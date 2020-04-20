import psycopg2
import statistics
import datetime
CONN = psycopg2.connect('dbname=ResearcherProfilerRepository_DEV user=postgres password=AwesomePwd')
TODAY = datetime.date.today().isoformat()
AGGREGATE_FUNCTIONS = {
    'SUM': sum,
    'AVG': statistics.mean
}

class Aggregation:
    def __init__(self, row):
        self.agg_id = row[0]
        self.name = row[1]
        try:
            self.calculate = AGGREGATE_FUNCTIONS[row[2]]
        except KeyError:
            raise ValueError(f'Invalid operation: {row[2]}')
        self.parent_id = row[3]
        self.csv_column = row[4]
        self.filter_def = None
        if row[5]:
            split_filter = row[5].split('=')
            self.filter_def = (split_filter[0], split_filter[1])
    def calculate_measure(self, rows, headers):
        data = [float(row[headers.index(self.csv_column)]) for row in rows]
        return self.calculate(data)
    def create_measure(self, mnumber, value):
        _insert_measures(TODAY, mnumber, self.agg_id, float(value))


    def get_filter(self, headers):
        filter_func = lambda x : True
        if self.filter_def:
            header_index = headers.index(self.filter_def[0])
            def func(row):
                left = str(row[header_index]) 
                right = self.filter_def[1].strip()
                return left == right
            filter_func = func
        return filter_func

def get_aggregates():
    sql_result = __query(SELECT_AGGREGATIONS)
    aggregations = []
    for row in sql_result:
        try:
            new_agg = Aggregation(row)
            aggregations.append(new_agg)
        except ValueError as e:
            print(f'ERROR:\t {e}')
    return aggregations

def insert_person(mnumber, first, last, email, department):
    params = {
        'mnumber':mnumber,
        'first_name':first,
        'last_name':last,
        'email': email,
        'department':department
    }
    __query(INSERT_PERSON, params, commit=True)

def _insert_measures(date_measured, person_measured, aggregate_measured, value):
    params = {
        'date_measured': date_measured,
        'person_measured': person_measured,
        'aggregate_measured': aggregate_measured,
        'value': value
    }
    __query(INSERT_MEASURE_QUERY, params, True)

def update_global_measure():
    __query(INSERT_GLOBAL_MEASURES_QUERY, commit=True)
def __query(query, args=None, commit=False):
    cursor = CONN.cursor()
    cursor.execute(query, args)
    if commit:
        CONN.commit()
    result = None
    try:
        result = cursor.fetchall()
    except:
        pass
    cursor.close()
    return result

INSERT_MEASURE_QUERY = '''
    INSERT INTO public.measure(id, date_measured, person_measured, aggregate_measured, value)
VALUES 
    (uuid_generate_v4(),%(date_measured)s,%(person_measured)s, %(aggregate_measured)s, %(value)s);
'''

INSERT_PERSON = '''
    INSERT INTO public.person(mnumber, first_name, last_name, email, department)
	VALUES (%(mnumber)s, %(first_name)s, %(last_name)s, %(email)s, %(department)s)
    ON CONFLICT DO NOTHING;
'''

INSERT_GLOBAL_MEASURES_QUERY = '''
INSERT INTO global_measure("id", aggregate_id, date_measured, minimum_value, maximum_value, mean, median, standard_deviation)
SELECT  uuid_generate_v4() AS "id", aggregate_measured, date_measured, MIN("value") AS "minimum_value", MAX("value") AS "maximum_value", 
	AVG("value") AS mean,  MEDIAN("value") AS median, STDDEV("value") AS standard_deviation FROM measure
WHERE date_measured = (SELECT MAX(date_measured) FROM measure)
GROUP BY aggregate_measured, date_measured;
'''

SELECT_AGGREGATIONS = '''
    SELECT id, name, type, parent_name, csv_column, filter
	FROM public.aggregation;
'''

if __name__ == '__main__':
    result = __query(SELECT_AGGREGATIONS)
    aggregates = get_aggregates()