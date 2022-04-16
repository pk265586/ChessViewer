CREATE TABLE IF NOT EXISTS Games (
    Id integer Primary Key Autoincrement NOT NULL,
    Name Varchar(100) Unique NOT NULL
);

CREATE TABLE IF NOT EXISTS GameMoves (
    Id integer Primary Key Autoincrement NOT NULL,
    GameId integer NOT NULL,
	MoveNumber integer NOT NULL,
	WhiteFrom varchar(2) NULL,
	WhiteTo varchar(2) NULL,
	BlackFrom varchar(2) NULL,
	BlackTo varchar(2) NULL
);
