
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";



-- Create a new database called 'DatabaseName'
-- CREATE DATABASE ResearcherProfilerRepository_DEV;



CREATE OR REPLACE FUNCTION _final_median(NUMERIC[])
   RETURNS NUMERIC AS
$$
   SELECT AVG(val)
   FROM (
     SELECT val
     FROM unnest($1) val
     ORDER BY 1
     LIMIT  2 - MOD(array_upper($1, 1), 2)
     OFFSET CEIL(array_upper($1, 1) / 2.0) - 1
   ) sub;
$$
LANGUAGE 'sql' IMMUTABLE;
 
CREATE AGGREGATE median(NUMERIC) (
  SFUNC=array_append,
  STYPE=NUMERIC[],
  FINALFUNC=_final_median,
  INITCOND='{}'
);

CREATE TABLE Person (
    mnumber VARCHAR(9) PRIMARY KEY CHECK (mnumber LIKE 'M%'),
    first_name VARCHAR NOT NULL,
    last_name VARCHAR NOT NULL,
    email VARCHAR UNIQUE NOT NULL CHECK (email LIKE '%@%.%'),
    department VARCHAR
);

CREATE TABLE Aggregation(
    id UUID PRIMARY KEY,
    "name" VARCHAR,
    "type" VARCHAR,
    csv_column VARCHAR NOT NULL,
    "filter" VARCHAR,
    parent_name uuid REFERENCES Aggregation(id)
);

CREATE TABLE Global_Measure(
    id uuid NOT NULL PRIMARY KEY,
    date_measured DATE NOT NULL,
    aggregate_id uuid NOT NULL REFERENCES Aggregation(id),
    mean FLOAT NOT NULL,
    median FLOAT NOT NULL,
    minimum_value FLOAT NOT NULL,
    maximum_value FLOAT NOT NULL,
    standard_deviation FLOAT NOT NULL,
    UNIQUE(date_measured, aggregate_id)
);

CREATE TABLE Measure(
    id uuid NOT NULL PRIMARY KEY,
    date_measured DATE NOT NULL,
    person_measured VARCHAR(9) NOT NULL REFERENCES Person(mnumber),
    aggregate_measured uuid NOT NULL REFERENCES Aggregation(id),
    "value" FLOAT NOT NULL
);

CREATE TABLE Threshold (
    id uuid NOT NULL PRIMARY KEY,
    threshold_start FLOAT NOT NULL,
    threshold_end FLOAT NOT NULL,
    threshold_name VARCHAR NOT NULL,
    aggregation UUID NOT NULL REFERENCES Aggregation(id)
);


