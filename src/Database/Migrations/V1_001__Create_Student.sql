CREATE TABLE student (
    student_id uuid NOT NULL,
    student_name varchar(50) NOT NULL,
    dob date NOT NULL,
    email varchar(100) NOT NULL,
    phone_number varchar(20) NOT NULL,
    address varchar(200) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    CONSTRAINT pk_student PRIMARY KEY (student_id)
);

