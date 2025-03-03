CREATE TABLE course_detail (
    enrollment_id uuid NOT NULL,
    student_id integer NOT NULL,
    course_id integer NOT NULL,
    enrollment_date Date NOT NULL,
    grade CHAR(1) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    CONSTRAINT pk_course_detail PRIMARY KEY (enrollment_id),
    CONSTRAINT fk_course_detail_student_id FOREIGN KEY (student_id) REFERENCES student (student_id),
    CONSTRAINT fk_course_detail_course_id FOREIGN KEY (course_id)  REFERENCES course (course_id),
    CONSTRAINT fk_course_detail_grade FOREIGN KEY (grade)  REFERENCES grade (grade)
);
