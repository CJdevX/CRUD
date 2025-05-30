-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.32-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for wc_db_project
DROP DATABASE IF EXISTS `wc_db_project`;
CREATE DATABASE IF NOT EXISTS `wc_db_project` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci */;
USE `wc_db_project`;

-- Dumping structure for table wc_db_project.inventoryitems
DROP TABLE IF EXISTS `inventoryitems`;
CREATE TABLE IF NOT EXISTS `inventoryitems` (
  `ItemID` int(11) NOT NULL AUTO_INCREMENT,
  `ItemName` varchar(50) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(20,2) NOT NULL,
  PRIMARY KEY (`ItemID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- Dumping data for table wc_db_project.inventoryitems: ~10 rows (approximately)
INSERT INTO `inventoryitems` (`ItemID`, `ItemName`, `Quantity`, `Price`) VALUES
	(1, 'Bread', 500, 50000.00),
	(3, 'Egg Bun', 150, 1200.00),
	(4, 'Fish Bun', 150, 10500.00),
	(5, 'Cream Bun', 100, 3500.00),
	(6, 'Pestry', 100, 18000.00),
	(7, 'Pizza', 50, 6000.00),
	(8, 'Rosted Bread', 500, 20000.00),
	(9, 'Hot Dogs', 50, 7500.00),
	(11, 'Tea Bun', 200, 1000.00),
	(12, 'Burger', 30, 3000.00);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
