ALTER TABLE IF EXISTS administrators 
    DROP CONSTRAINT IF EXISTS fk_administrator_user
    DROP CONSTRAINT IF EXISTS fk_administrator_organizator

ALTER TABLE IF EXISTS places 
    DROP CONSTRAINT IF EXISTS fk_place_organizator
    DROP CONSTRAINT IF EXISTS fk_place_creator

ALTER TABLE IF EXISTS seats 
    DROP CONSTRAINT IF EXISTS fk_seat_place
    DROP CONSTRAINT IF EXISTS fk_seat_section

ALTER TABLE IF EXISTS events 
    DROP CONSTRAINT IF EXISTS fk_event_organizator
    DROP CONSTRAINT IF EXISTS fk_event_administrator
    DROP CONSTRAINT IF EXISTS fk_event_place

ALTER TABLE IF EXISTS tickets 
    DROP CONSTRAINT IF EXISTS fk_ticket_event
    DROP CONSTRAINT IF EXISTS fk_ticket_user
    DROP CONSTRAINT IF EXISTS fk_ticket_seat

ALTER TABLE IF EXISTS sections 
    DROP CONSTRAINT IF EXISTS fk_section_place

DROP TABLE IF EXISTS users;

/*
1 - superadmin
2 - admin
3 - editor
4 - user
*/

CREATE TABLE users (
    user_id serial PRIMARY KEY,
    login varchar(32) NOT NULL UNIQUE,
    name varchar(32) NOT NULL,
    surname varchar(32) NOT NULL,
    password varchar(64) NOT NULL,
    email varchar(64) NOT NULL UNIQUE,
    role smallint NOT NULL DEFAULT 4,
    is_active boolean NOT NULL,
    insert_date TIMESTAMP WITH TIME ZONE NOT NULL
); 

DROP TABLE IF EXISTS organizators; 

CREATE TABLE organizators (
    organizator_id serial PRIMARY KEY,
    name varchar(64) NOT NULL,
    insert_date TIMESTAMP WITH TIME ZONE NOT NULL
);

DROP TABLE IF EXISTS administrators; 

CREATE TABLE administrators (
    administrator_id serial PRIMARY KEY,
    user_id int NOT NULL, --FK to users
    organizator_id int NOT NULL, --FK to organizators
    insert_date TIMESTAMP WITH TIME ZONE NOT NULL
);

DROP TABLE IF EXISTS places;

CREATE TABLE places (
    place_id serial PRIMARY KEY,
    name varchar(128) NOT NULL,
    organizator_id int NOT NULL, -- FK to organizators
    creator_id int NOT NULL, -- FK to users
    insert_date TIMESTAMP WITH TIME ZONE,
    UNIQUE(name, organizator_id)
);


DROP TABLE IF EXISTS seats;

CREATE TABLE seats (
    seat_id serial PRIMARY KEY,
    section_id int NOT NULL, --FK to sections
    seat_row smallint NOT NULL,
    seat_number smallint NOT NULL,
    UNIQUE(section_id, seat_row, seat_number)
);

DROP TABLE IF EXISTS events;

CREATE TABLE events (
    event_id serial PRIMARY KEY,
    organizator_id int NOT NULL, --FK to organizators
    administrator_id int NOT NULL, --FK to administrators
    name varchar(128) NOT NULL,
    begin_date TIMESTAMP WITH TIME ZONE NOT NULL,
    finish_date TIMESTAMP WITH TIME ZONE NOT NULL,
    place_id int NOT NULL, -- FK to places
    insert_date TIMESTAMP WITH TIME ZONE NOT NULL,
    update_date TIMESTAMP WITH TIME ZONE NOT NULL
);

DROP TABLE IF EXISTS tickets;

CREATE TABLE tickets (
    ticket_id serial PRIMARY KEY,
    event_id int NOT NULL, --FK to events
    user_id int NOT NULL, --FK to users
    seat_id int NOT NULL, -- FK to seats
    insert_date TIMESTAMP WITH TIME ZONE NOT NULL,
    UNIQUE (event_id, seat_id)
);

DROP TABLE IF EXISTS sections;

CREATE TABLE sections (
    section_id serial PRIMARY KEY,
    place_id int NOT NULL, -- FK to organizator
    name varchar(128) NOT NULL,
    UNIQUE (place_id, name)
);


-- FOREIGN KEYS
ALTER TABLE administrators
    ADD CONSTRAINT fk_administrator_user FOREIGN KEY (user_id) REFERENCES users(user_id),
    ADD CONSTRAINT fk_administrator_organizator FOREIGN KEY (organizator_id) REFERENCES organizators(organizator_id);

ALTER TABLE places
    ADD CONSTRAINT fk_place_organizator FOREIGN KEY (organizator_id) REFERENCES organizators(organizator_id),
    ADD CONSTRAINT fk_place_creator FOREIGN KEY (creator_id) REFERENCES users(user_id);

ALTER TABLE seats
    ADD CONSTRAINT fk_seat_place FOREIGN KEY (place_id) REFERENCES places(place_id),
    ADD CONSTRAINT fk_seat_section FOREIGN KEY (section_id) REFERENCES sections(section_id);

ALTER TABLE events
    ADD CONSTRAINT fk_event_organizator FOREIGN KEY (organizator_id) REFERENCES organizators(organizator_id),
    ADD CONSTRAINT fk_event_administrator FOREIGN KEY (administrator_id) REFERENCES users(user_id),
    ADD CONSTRAINT fk_event_place FOREIGN KEY (place_id) REFERENCES places(place_id);

ALTER TABLE tickets
    ADD CONSTRAINT fk_ticket_event FOREIGN KEY (event_id) REFERENCES events(event_id),
    ADD CONSTRAINT fk_ticket_user FOREIGN KEY (user_id) REFERENCES users(user_id),
    ADD CONSTRAINT fk_ticket_seat FOREIGN KEY (seat_id) REFERENCES seats(seat_id);

ALTER TABLE sections
    ADD CONSTRAINT fk_section_place FOREIGN KEY (place_id) REFERENCES places(place_id);

-- FUNCTIONS
CREATE OR REPLACE FUNCTION insert_update_timestamp_function() RETURNS trigger AS $BODY$
BEGIN
  IF TG_OP = 'INSERT' THEN
    NEW.insert_date := current_timestamp;
  ELSE
    NEW.update_date := current_timestamp;
  END IF;
  RETURN NEW;
END; $BODY$ LANGUAGE plpgsql VOLATILE;


-- TRIGGERS
CREATE TRIGGER trigger_insert_user
BEFORE INSERT ON users
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();

CREATE TRIGGER trigger_insert_organizator
BEFORE INSERT ON organizators
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();

CREATE TRIGGER trigger_insert_administrator
BEFORE INSERT ON administrators
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();

CREATE TRIGGER trigger_insert_place
BEFORE INSERT ON places
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();

CREATE TRIGGER trigger_insert_ticket
BEFORE INSERT ON tickets
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();

CREATE TRIGGER trigger_insert_update_event
BEFORE INSERT OR UPDATE ON events
FOR EACH ROW EXECUTE PROCEDURE insert_update_timestamp_function();


-- INSERT SUPERADMIN

INSERT INTO users (login, name, surname, password, email, role, is_active) VALUES ('admin', 'Administrator', 'Administrator', '$2a$11$KQSNECjJliC3Y78o3reyI.15s1psYlh4LyyiiK8icfKUULMP9aQ8K', 'admin@tickets.com', 1, true); -- Password: qwerty
