
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Create a new database called 'DatabaseName'
-- CREATE DATABASE ResearcherProfilerRepository_DEV;
CREATE TABLE Person (
    mnumber VARCHAR(9) PRIMARY KEY CHECK (mnumber LIKE 'M%'),
    firstName VARCHAR NOT NULL,
    lastName VARCHAR NOT NULL,
    email VARCHAR UNIQUE NOT NULL CHECK (email LIKE '%@%.%'),
    department VARCHAR
);

CREATE TABLE Aggregation(
    id UUID PRIMARY KEY,
    "name" VARCHAR,
    "type" VARCHAR,
    parentName uuid REFERENCES Aggregation(id)
);

CREATE TABLE GlobalMeasure(
    id uuid NOT NULL PRIMARY KEY,
    dateMeasured DATE NOT NULL,
    aggregateId uuid NOT NULL REFERENCES Aggregation(id),
    mean FLOAT NOT NULL,
    median FLOAT NOT NULL,
    minimumValue FLOAT NOT NULL,
    maximumValue FLOAT NOT NULL,
    standardDeviation FLOAT NOT NULL,
    UNIQUE(dateMeasured, aggregateId)
);

CREATE TABLE Measure(
    id uuid NOT NULL PRIMARY KEY,
    dateMeasured DATE NOT NULL,
    personMeasured VARCHAR(9) NOT NULL REFERENCES Person(mnumber),
    aggregateMeasured uuid NOT NULL REFERENCES Aggregation(id),
    "value" FLOAT NOT NULL
);

CREATE TABLE Threshold (
    id uuid NOT NULL PRIMARY KEY,
    threshold_start FLOAT NOT NULL,
    threshold_end FLOAT NOT NULL,
    thresholdName VARCHAR NOT NULL,
    aggregation UUID NOT NULL REFERENCES Aggregation(id)
);


