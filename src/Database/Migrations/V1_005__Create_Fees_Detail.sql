CREATE TABLE fees_detail (
    fees_detail_id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    enrollment_id integer NOT NULL,
    amount_paid integer NOT NULL,
    payment_status varchar(20) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (enrollment_id) REFERENCES course_detail (enrollment_id)
);
