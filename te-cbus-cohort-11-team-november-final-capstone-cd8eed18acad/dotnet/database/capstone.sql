USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE devices (
	device_id int IDENTITY(1,1) NOT NULL,
	device_type varchar(50) NOT NULL,
	firmware_version varchar(100) NOT NULL,
	facility varchar(50) NOT NULL,
	city varchar(50) NOT NULL,
	state varchar(10) NOT NULL,
	device_in_use bit NOT NULL,
	battery_status decimal NOT NULL
	) 


--populate default data: 'password'
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Strength','xyzzy3000','NationWide Childrens Hospital','Columbus','OH', 'false', 25) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Connect','ABC123','St. Marys','Columbus','OH', 'false', 42) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Connect','ABC123','St. Annes','Columbus','OH', 'false', 88) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Strength','xyzzy4200','Riverside','Columbus','OH', 'false', 5) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Strength','xyzzy5000','Mt. Caramel','Columbus','OH', 'false', 10) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Connect','ABC12345','Ohio State Wexner Medical Center','Columbus','OH', 'false', 100) 
INSERT INTO devices (device_type, firmware_version, facility, city, state, device_in_use, battery_status) 
VALUES ('Strength','xyzzy9000','NationWide Childrens Hospital','Columbus','OH', 'false', 50) 


GO