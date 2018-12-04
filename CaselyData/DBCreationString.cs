﻿namespace CaselyData {
	public class DBCreationString {
        public static string sqlCreateDBString = @"CREATE TABLE `path_case` (
	`case_number`	TEXT NOT NULL UNIQUE PRIMARY KEY ON CONFLICT IGNORE,
	`service`	TEXT,
	`evaluation` TEXT,
    `date_of_service` TEXT
);

CREATE TABLE IF NOT EXISTS `casely_data` (
`database_version` TEXT
);

INSERT INTO `casely_data` (database_version) VALUES (0.24);

CREATE TABLE IF NOT EXISTS `case_entry` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,	
	`author_id`	TEXT,
	`case_number`	TEXT NOT NULL,
	`date_modified`	TEXT,
	`time_modified`	TEXT,
	`tumor_synoptic`	TEXT,
	`comment`	TEXT,
	`result` TEXT,
	`material` TEXT,
	`history` TEXT,
	`interpretation` TEXT,
	`gross` TEXT,
	`microscopic`	TEXT

);

CREATE TABLE IF NOT EXISTS `part_diagnosis` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,	
	`case_number`	TEXT NOT NULL,
	`part` TEXT NOT NULL,
	`date_modified` TEXT NOT NULL,
	`time_modified` TEXT NOT NULL,
	`organ_system` TEXT,
	`organ` TEXT,
	`category` TEXT,	
	`diagnosis`	TEXT,
	`diagnosis_detailed` TEXT
);

CREATE TABLE IF NOT EXISTS `part_entry` (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`author_id`	TEXT,
	`part`	TEXT,
	`procedure`	TEXT,
	`specimen`	TEXT,
	`organ_system` TEXT,
	`date_modified`	TEXT,
	`time_modified`	TEXT,
	`case_number` TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS `staff` (
	`last_first_name`	TEXT,
	`author_id`TEXT UNIQUE NOT NULL PRIMARY KEY ON CONFLICT IGNORE,
	`role`	TEXT
);

CREATE TABLE IF NOT EXISTS `evaluation` (
	`evaluation`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TABLE IF NOT EXISTS `service` (
	`service`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insert_part_entry_author AFTER INSERT  ON part_entry
BEGIN
INSERT INTO staff (author_id) VALUES (new.author_id);
END;

CREATE TRIGGER insert_case_entry AFTER INSERT  ON case_entry
BEGIN
INSERT INTO staff (author_id) VALUES (new.author_id);
INSERT INTO fts5_case_entry_result (author_id, case_number, date_modified, time_modified, result) VALUES (new.author_id, new.case_number, new.date_modified, new.time_modified, new.result);
INSERT INTO fts5_case_entry_interpretation (author_id, case_number, date_modified, time_modified,interpretation) VALUES (new.author_id, new.case_number, new.date_modified, new.time_modified,new.interpretation);
INSERT INTO  fts5_case_entry_comment (author_id, case_number, date_modified, time_modified, comment)  VALUES (new.author_id, new.case_number,new.date_modified, new.time_modified, new.comment);
INSERT INTO  fts5_case_entry_tumor_synoptic (author_id, case_number, date_modified, time_modified, tumor_synoptic)  VALUES (new.author_id, new.case_number, new.date_modified, new.time_modified,new.tumor_synoptic);
END;

CREATE TABLE IF NOT EXISTS `specimen` (
	`specimen`	TEXT UNIQUE NOT NULL PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insrt_part_entry_specimen AFTER  INSERT ON part_entry
BEGIN
INSERT INTO specimen (specimen) VALUES (new.specimen);
END;

CREATE TABLE IF NOT EXISTS `procedure` (
	`procedure`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);


CREATE TRIGGER insert_part_entry_procedure AFTER INSERT  ON part_entry
BEGIN
INSERT INTO procedure(procedure) VALUES (new.procedure);
END;


CREATE TABLE IF NOT EXISTS `diagnosis` (
	`diagnosis`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insert_part_diagnosis_diagnosis AFTER INSERT  ON part_diagnosis
BEGIN
INSERT INTO diagnosis (diagnosis) VALUES (new.diagnosis);
END;


CREATE TABLE IF NOT EXISTS `organ` (
	`organ`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insert_part_diganosis_organ AFTER INSERT  ON part_diagnosis
BEGIN
INSERT INTO organ (organ) VALUES (new.organ);
END;


CREATE TABLE IF NOT EXISTS `organ_system` (
	`organ_system`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insert_part_diganosis_organ_system AFTER INSERT  ON part_diagnosis
BEGIN
INSERT INTO organ_system (organ_system) VALUES (new.organ_system);
END;


CREATE TABLE IF NOT EXISTS `diagnosis_category` (
	`category`	TEXT UNIQUE PRIMARY KEY ON CONFLICT IGNORE
);

CREATE TRIGGER insert_part_diganosis_category AFTER INSERT  ON part_diagnosis
BEGIN
INSERT INTO diagnosis_category (category) VALUES (new.category);
END;

CREATE TRIGGER insert_case_entry_case_number AFTER INSERT ON case_entry
BEGIN
INSERT INTO path_case (case_number) VALUES (new.case_number);
END;

CREATE TRIGGER insert_part_entry_case_number AFTER INSERT ON part_entry
BEGIN
INSERT INTO path_case (case_number) VALUES (new.case_number);
END;

CREATE TRIGGER insert_path_case_case_number AFTER UPDATE ON path_case
BEGIN
INSERT INTO evaluation (evaluation) VALUES (new.evaluation);
END;

CREATE TRIGGER insert_service_path_case AFTER UPDATE ON path_case
BEGIN
INSERT INTO service (service) VALUES (new.service);
END;

CREATE VIRTUAL TABLE fts5_case_entry_result USING fts5(author_id, case_number, date_modified, time_modified, result);
CREATE VIRTUAL TABLE fts5_case_entry_interpretation USING fts5(author_id, case_number, date_modified, time_modified, interpretation);
CREATE VIRTUAL TABLE fts5_case_entry_comment USING fts5(author_id, case_number, date_modified, time_modified, comment);
CREATE VIRTUAL TABLE fts5_case_entry_tumor_synoptic USING fts5(author_id, case_number, date_modified, time_modified, tumor_synoptic);

CREATE INDEX casenum_case_entry ON case_entry (case_number);
CREATE INDEX casenum_part_entry ON part_entry (case_number);
CREATE INDEX casenum_path_case ON path_case (case_number);
CREATE INDEX casenum_part_diagnosis ON part_diagnosis (case_number);
	";

    }
}