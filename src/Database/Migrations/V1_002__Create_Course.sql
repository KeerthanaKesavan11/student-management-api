CREATE TABLE course (
course_id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
course_name varchar(20) NOT NULL,
duration  varchar(20) NOT NULL,
isactive BOOLEAN DEFAULT TRUE,
fees numeric NOT NULL
);