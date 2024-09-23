--Database creation
CREATE DATABASE MailingList;
USE MailingList;

--Table creation
CREATE TABLE Contact
(
Contact_ID int identity(1,1) Primary Key,
FirstName varchar(50),
LastName varchar(50),
Email varchar(50)
);

--Starter Data. Use these queries to have some initial data to test the application.
INSERT INTO Contact values ('Frederick', 'Hamlin', 'fhamlin@example.com');
INSERT INTO Contact values ('Derik', 'Hamlin', 'dhamlin@example.com');
INSERT INTO Contact values ('Jaya', 'Newton', 'jnewton@yahoo.com');
INSERT INTO Contact values ('Khenan', 'Newton', 'knewton@yahoo.com');
INSERT INTO Contact values ('Jibble', 'Bibbles', 'jbibbles@gmail.com');
INSERT INTO Contact values ('Goat', 'Ofalltime', 'gofalltime@hotmail.com');
