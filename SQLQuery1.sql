﻿
CREATE TABLE Skole(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Lokation varchar(30)
);

CREATE TABLE BrugerRolle(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RolleNavn varchar(30),
	DagesVarselIndenOverskrivelse int NULL,
);

CREATE TABLE Bruger(
	Name varchar(20) NOT NULL,
	Email varchar(30) NOT NULL PRIMARY KEY,
	Kode varchar(32) NOT NULL,
	Rolle int NOT NULL FOREIGN KEY REFERENCES BrugerRolle(Id),
	SkoleId int NOT NULL FOREIGN KEY REFERENCES Skole(Id),
	SletningsDato date NULL
);

CREATE TABLE Lokale(
	Id int NOT NULL,
	SkoleId int NOT NULL FOREIGN KEY REFERENCES Skole(Id),
	HarSmartboard tinyint NOT NULL,
	PRIMARY KEY (Id, SkoleId)
);

CREATE TABLE BookingType(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TypeNavn varchar(10) NOT NULL
);

CREATE TABLE Booking(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Dato date NOT NULL,
	TidFra int NOT NULL,
	TidTil int NOT NULL,
	BrugerEmail varchar(30) NOT NULL FOREIGN KEY REFERENCES Bruger(Email),
	LokaleId int NOT NULL,
	SkoleId int NOT NULL,
	Type int NOT NULL FOREIGN KEY REFERENCES BookingType(Id),
	FOREIGN KEY (LokaleId, SkoleId) REFERENCES Lokale(Id, SkoleId)
);


INSERT INTO Skole (Lokation)
VALUES ('JORNKOB'),
('Steffen');