CREATE TABLE grade (
    grade_id uuid NOT NULL,
    grade CHAR(1) NOT NULL,
    score_range VARCHAR(20),
    CONSTRAINT pk_grade PRIMARY KEY (grade_id)
);
