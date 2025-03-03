CREATE TABLE course_detail (
    enrollment_id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    student_id integer NOT NULL,
    course_id integer NOT NULL,
    enrollment_date Date NOT NULL,
    grade CHAR(1) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (student_id) REFERENCES student (student_id),
    FOREIGN KEY (course_id) REFERENCES course (course_id),
    FOREIGN KEY (grade) REFERENCES grade (grade)
);
