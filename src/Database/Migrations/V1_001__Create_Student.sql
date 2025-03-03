CREATE TABLE student (
student_id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
student_name varchar(50) NOT NULL,
dob date NOT NULL,
email varchar(100) NOT NULL,
phone_number varchar(20) NOT NULL,
address varchar(200) NOT NULL,
isactive BOOLEAN DEFAULT TRUE
);
