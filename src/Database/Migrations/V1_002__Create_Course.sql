CREATE TABLE course (
    course_id uuid NOT NULL,
    course_name varchar(20) NOT NULL,
    duration varchar(20) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    fees numeric NOT NULL,
    CONSTRAINT pk_course PRIMARY KEY (course_id)
);
