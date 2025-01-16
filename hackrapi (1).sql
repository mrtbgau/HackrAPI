-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jan 16, 2025 at 11:38 PM
-- Server version: 8.2.0
-- PHP Version: 8.2.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hackrapi`
--

-- --------------------------------------------------------

--
-- Table structure for table `logs`
--

DROP TABLE IF EXISTS `logs`;
CREATE TABLE IF NOT EXISTS `logs` (
  `LogID` int NOT NULL AUTO_INCREMENT,
  `Date` datetime(6) NOT NULL,
  `UserId` int NOT NULL,
  `Details` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LogID`),
  KEY `IX_Logs_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `logs`
--

INSERT INTO `logs` (`LogID`, `Date`, `UserId`, `Details`) VALUES
(1, '2024-10-31 11:14:02.835865', 4, 'string s\'est connecté'),
(2, '2024-11-12 20:08:14.598439', 4, 'string s\'est connecté'),
(3, '2024-11-13 19:45:21.594532', 4, 'string s\'est connecté'),
(4, '2024-11-13 20:00:31.425952', 4, 'string s\'est connecté'),
(5, '2024-11-13 20:01:15.778627', 4, 'string s\'est connecté'),
(6, '2024-11-13 20:07:53.356068', 4, 'string s\'est connecté'),
(7, '2024-11-13 20:08:15.894914', 4, 'string s\'est connecté'),
(8, '2024-11-13 20:45:15.135876', 4, 'string s\'est connecté'),
(9, '2025-01-13 12:06:23.810452', 4, 'string s\'est connecté'),
(10, '2025-01-16 23:35:01.391425', 4, 'string s\'est connecté');

-- --------------------------------------------------------

--
-- Table structure for table `permission`
--

DROP TABLE IF EXISTS `permission`;
CREATE TABLE IF NOT EXISTS `permission` (
  `PermissionId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`PermissionId`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `permission`
--

INSERT INTO `permission` (`PermissionId`, `Name`, `Description`) VALUES
(1, 'ViewLogs', 'Allows viewing of log routes'),
(2, 'ManagePermission', 'Allows managing permissions');

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `RoleId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`RoleId`, `Name`, `Description`) VALUES
(1, 'Admin', 'Administrator with full access');

-- --------------------------------------------------------

--
-- Table structure for table `rolepermissions`
--

DROP TABLE IF EXISTS `rolepermissions`;
CREATE TABLE IF NOT EXISTS `rolepermissions` (
  `RoleId` int NOT NULL,
  `PermissionId` int NOT NULL,
  PRIMARY KEY (`RoleId`,`PermissionId`),
  KEY `IX_RolePermissions_PermissionId` (`PermissionId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `rolepermissions`
--

INSERT INTO `rolepermissions` (`RoleId`, `PermissionId`) VALUES
(1, 1),
(1, 2);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserPWD` longblob NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL DEFAULT '0',
  `mail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PasswordSalt` longblob NOT NULL,
  `RoleId` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`UserID`),
  KEY `IX_Users_RoleId` (`RoleId`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `UserName`, `UserPWD`, `IsAdmin`, `mail`, `PasswordSalt`, `RoleId`) VALUES
(4, 'string', 0x61f74e3f045e1072d552e4296f86d756655d884c4d465186da229b4df1c98029c42c5c8dcb6fc873aa65cb135cc955904d5a34abe4ae1f064ca842894f8bafcc, 1, 'user@example.com', 0x059f96c102faf4684edfb5159e2c7fbf228cf90f2c0d5657f21fb43fb4729e05ae79dab182d7f67f4d6589bc1ca49c0ab91a769384db3ca30ee78175792d0084fbdb2843b8587f91169d005013ec8fd3858de26b8f28ad72e2372f3eae9e9702f2c0e3e1462b2567f8dc53b23884225d5d7ec07cc0afa44c866ec2c76abe437f, 1);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20241011143653_UserMigration', '8.0.10'),
('20241016194525_UserMigrationV2', '8.0.10'),
('20241017162328_UserMigrationV3', '8.0.10'),
('20241017193115_UserPermissionSystem', '8.0.10'),
('20241030144339_LogMigration', '8.0.10'),
('20241030202514_LogMigration', '8.0.10'),
('20241030203859_LogMigrationV2', '8.0.10'),
('20241112000243_UpdateUserRoleRelationship', '8.0.10');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
