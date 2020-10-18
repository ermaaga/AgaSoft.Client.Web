  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '
'__EFMigrationsHistory'' AND table_schema = DATABASE()) 
BEGIN
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE TABLE `UserRoles` (
    `Id` int NOT NULL,
    `Role` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` int NOT NULL,
    `Username` text NULL,
    `Password` text NULL,
    `Email` text NULL,
    `IdRoles` int NOT NULL,
    `RolesId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Users_UserRoles_RolesId` FOREIGN KEY (`RolesId`) REFERENCES `UserRoles` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Users_RolesId` ON `Users` (`RolesId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201018153245_FirstMigration', '3.1.9');

ALTER TABLE `Users` DROP COLUMN `IdRoles`;

CREATE TABLE `Roles` (
    `Id` int NOT NULL,
    `Name` text NULL,
    `UserRolesId` int NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Roles_UserRoles_UserRolesId` FOREIGN KEY (`UserRolesId`) REFERENCES `UserRoles` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Roles_UserRolesId` ON `Roles` (`UserRolesId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201018205700_AddRolesTale', '3.1.9');

