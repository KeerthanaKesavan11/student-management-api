CREATE TABLE fees_detail (
    fees_detail_id uuid NOT NULL,
    enrollment_id integer NOT NULL,
    amount_paid integer NOT NULL,
    payment_status varchar(20) NOT NULL,
    isactive BOOLEAN DEFAULT TRUE,
    CONSTRAINT pk_fees_detail PRIMARY KEY (fees_detail_id),
    CONSTRAINT fk_fees_detail_enrollment_id FOREIGN KEY (enrollment_id) REFERENCES course_detail (enrollment_id)
);
