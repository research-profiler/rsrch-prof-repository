import os
import aggregate

TEST_FILE_PATH = '../data/testdata.csv'
USE_TEST_FILE = True

def main():
    csv_file_path = TEST_FILE_PATH
    if not USE_TEST_FILE:
        csv_file_path = _get_csv_path()
    
    rows_by_researcher, headers = _parse_csv(csv_file_path)

    aggregates = aggregate.get_aggregates()
    measures = calc_measures(rows_by_researcher, headers, aggregates)

def __insert_person(person_rows, headers):
    row = person_rows[0]
    mnumber = row[_get_column_index('PERSON_ID', headers)]
    name = row[_get_column_index('NAME', headers)]
    last, first = tuple(name.split(',')[0:2])
    email = 'NotAvailable@nomail.com'
    department = row[_get_column_index('DEPARTMENT', headers)]
    aggregate.insert_person(mnumber, first, last, email, department)

def calc_measures(rows_by_researcher, headers, aggregates):
    '''
        Generates a function which calculates the measures based on 
    '''
    print('INFO:\tProcessing Data...')
    for mnumber in rows_by_researcher:
        researcher_rows = rows_by_researcher[mnumber]
        __insert_person(researcher_rows, headers)
        for agg in aggregates:
            value_column = _get_column_index(agg.csv_column, headers)
            agg_filter = agg.get_filter(headers)

            filter_func = agg.get_filter(headers)
            filtered_researcher_rows = filter(filter_func, researcher_rows)
            measure_value = agg.calculate_measure(filtered_researcher_rows, headers)
            if measure_value != 0:
                agg.create_measure(mnumber, measure_value)
    print('INFO:\tFinished processing. now updating global measures')
    aggregate.update_global_measure()
    print('INFO:\tFinished updating global measures')


def _parse_csv(csv_path):
    print(f'INFO:\tParsing {csv_path}')
    
    line_table = {}
    with open(csv_path, 'r') as csv:
        headers_line = csv.readline()
        headers = _clean_csv_row(headers_line)
        assert len(headers) == 36 # make sure we have the right number of columns
        mnumber_index = _get_column_index('PERSON_ID', headers)

        for line in csv:
            mnumber, row = _parse_csv_row(line, mnumber_index)
            assert len(row) == 36 # make sure we have the right number of columns

            if mnumber in line_table:
                line_table[mnumber].append(row)
            else:
                line_table[mnumber] = [row]
    print(f'INFO:\tFinished parsing the csv file. Found {len(line_table)} unique researchers')
    return line_table, headers
            

def _parse_csv_row(line, mnumber_index):
    cleaned_line = _clean_csv_row(line)
    mnumber = cleaned_line[mnumber_index]
    return mnumber, cleaned_line

def _get_column_index(column_name, headers):
    col_index = headers.index(column_name)
    return col_index

def _clean_csv_row(row):
    strip_end_line = row.replace('\n', '')

    split_row = strip_end_line.replace('""','"NULL"').split('","')
    return split_row


def _get_csv_path():
    csv_file_path = input('CSV File Path: ')
    while not csv_file_path or not os.path.isfile(csv_file_path) or not csv_file_path.endswith('.csv'):
        print(f'I\'m sorry, "{csv_file_path}" is not a valid path for a csv. Ensure that the path exists and has the extension ".csv"')
        csv_file_path = input('CSV File Path: ')
    return csv_file_path

if __name__ == '__main__':
    main()