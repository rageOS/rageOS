SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";

CREATE TABLE `account` (
  `Id` int(11) NOT NULL,
  `EMail` varchar(100) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `CopLevel` int(11) NOT NULL,
  `JustizLevel` int(11) NOT NULL,
  `AdminLevel` int(11) NOT NULL,
  `MedicLevel` int(11) NOT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `IsBanned` tinyint(1) NOT NULL,
  `BanTime` datetime DEFAULT NULL,
  `BanReason` varchar(255) DEFAULT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `auctionhouse` (
  `Id` int(11) NOT NULL,
  `Item` int(11) NOT NULL,
  `Quantity` double NOT NULL,
  `characterId` int(11) NOT NULL,
  `BuyPrice` double NOT NULL,
  `isSold` tinyint(1) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `BuyDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `character` (
  `Id` int(11) NOT NULL,
  `AccountId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `IsCop` tinyint(1) NOT NULL,
  `IsMedic` tinyint(1) NOT NULL,
  `IsJustiz` tinyint(1) NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `HealthLevel` double NOT NULL,
  `FoodLevel` double NOT NULL,
  `DrinkLevel` double NOT NULL,
  `DrunkLevel` double DEFAULT NULL,
  `CashMoney` double NOT NULL,
  `BankMoney` double DEFAULT NULL,
  `Level` int(11) NOT NULL,
  `MinutesToNextLevel` int(11) NOT NULL,
  `MinutesInThisLevel` int(11) NOT NULL,
  `Jailed` tinyint(1) DEFAULT NULL,
  `JailTime` double NOT NULL,
  `Gender` int(11) NOT NULL,
  `SSN` varchar(20) DEFAULT NULL,
  `LocationX` double NOT NULL,
  `LocationY` double NOT NULL,
  `LocationZ` double NOT NULL,
  `Skin` int(11) DEFAULT NULL,
  `ClothesDataJSON` varchar(5000) DEFAULT NULL,
  `CharDataJSON` varchar(5000) DEFAULT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `clothes` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `Draw0` int(11) NOT NULL,
  `Tx0` int(11) NOT NULL,
  `Draw1` int(11) NOT NULL,
  `Tx1` int(11) NOT NULL,
  `Draw2` int(11) NOT NULL,
  `Tx2` int(11) NOT NULL,
  `Draw3` int(11) NOT NULL,
  `Tx3` int(11) NOT NULL,
  `Draw4` int(11) NOT NULL,
  `Tx4` int(11) NOT NULL,
  `Draw5` int(11) NOT NULL,
  `Tx5` int(11) NOT NULL,
  `Draw6` int(11) NOT NULL,
  `Tx6` int(11) NOT NULL,
  `Draw7` int(11) NOT NULL,
  `Tx7` int(11) NOT NULL,
  `Draw8` int(11) NOT NULL,
  `Tx8` int(11) NOT NULL,
  `Draw9` int(11) NOT NULL,
  `Tx9` int(11) NOT NULL,
  `Draw10` int(11) NOT NULL,
  `Tx10` int(11) NOT NULL,
  `Draw11` int(11) NOT NULL,
  `Tx11` int(11) NOT NULL,
  `Propdraw0` int(11) NOT NULL,
  `Proptx0` int(11) NOT NULL,
  `Propdraw1` int(11) NOT NULL,
  `Proptx1` int(11) NOT NULL,
  `Propdraw2` int(11) NOT NULL,
  `Proptx2` int(11) NOT NULL,
  `Propdraw3` int(11) NOT NULL,
  `Proptx3` int(11) NOT NULL,
  `Propdraw4` int(11) NOT NULL,
  `Proptx4` int(11) NOT NULL,
  `Propdraw5` int(11) NOT NULL,
  `Proptx5` int(11) NOT NULL,
  `Propdraw6` int(11) NOT NULL,
  `Proptx6` int(11) NOT NULL,
  `Propdraw7` int(11) NOT NULL,
  `Proptx7` int(11) NOT NULL,
  `Propdraw8` int(11) NOT NULL,
  `Proptx8` int(11) NOT NULL,
  `Propdraw9` int(11) NOT NULL,
  `Proptx9` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `communication` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `HasPhone` tinyint(1) DEFAULT NULL,
  `HasShortRadio` tinyint(1) DEFAULT NULL,
  `HasLongRadio` tinyint(1) DEFAULT NULL,
  `PhoneNumber` varchar(10) DEFAULT NULL,
  `ShortRadioFrq` double DEFAULT NULL,
  `LongRadioFreq` double DEFAULT NULL,
  `ShortRadioVolume` double DEFAULT NULL,
  `LongRadioVolume` double DEFAULT NULL,
  `ShortRadioSide` int(11) NOT NULL,
  `LongRadioSide` int(11) NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `company` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `IdentNumber` int(11) NOT NULL,
  `BankMoney` double NOT NULL,
  `Type` int(11) NOT NULL,
  `LocationX` double NOT NULL,
  `LocationY` double NOT NULL,
  `LocationZ` double NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `house` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) DEFAULT NULL,
  `LocationX` double NOT NULL,
  `LocationY` double NOT NULL,
  `LocationZ` double NOT NULL,
  `Price` double NOT NULL,
  `RentalObjects` int(11) NOT NULL,
  `RentPerObject` double NOT NULL,
  `ObjectInterior` varchar(100) DEFAULT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `housekey` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `HouseId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `inventory` (
  `Id` int(11) NOT NULL,
  `Type` int(11) DEFAULT NULL,
  `CharacterId` int(11) NOT NULL,
  `ObjectId` int(11) DEFAULT NULL,
  `Item` varchar(100) DEFAULT NULL,
  `Quantity` double NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `job` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `Job` int(11) DEFAULT NULL,
  `JobLevel` int(11) NOT NULL,
  `EXPToNextLevel` int(11) NOT NULL,
  `EXPInThisLevel` int(11) NOT NULL,
  `CompaniesId` int(11) NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `logs` (
  `id` int(11) NOT NULL,
  `text` varchar(1024) NOT NULL,
  `type` varchar(100) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `message` (
  `Id` int(11) NOT NULL,
  `SenderId` int(11) NOT NULL,
  `ReceiverId` longtext NOT NULL,
  `Type` int(11) NOT NULL,
  `Message` varchar(5000) DEFAULT NULL,
  `Created` datetime NOT NULL,
  `Answered` datetime DEFAULT NULL,
  `DeletedBySender` tinyint(1) NOT NULL,
  `DeletedByReceiver` tinyint(1) NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `quest` (
  `Id` int(11) NOT NULL,
  `MinLevel` int(11) NOT NULL,
  `MaxLevel` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Description` varchar(5000) NOT NULL,
  `RewardMoney` double NOT NULL,
  `RewardBankMoney` longtext NOT NULL,
  `RewardXP` double NOT NULL,
  `RewardItem` int(11) NOT NULL,
  `RewardSkill` longtext NOT NULL,
  `RewardJob` longtext NOT NULL,
  `RewardVehicle` int(11) NOT NULL,
  `NextQuest` int(11) NOT NULL,
  `PreviousQuest` int(11) NOT NULL,
  `RequiredGender` int(11) NOT NULL,
  `RequiredSkill` int(11) NOT NULL,
  `RequiredItem` int(11) NOT NULL,
  `RequiredJob` int(11) NOT NULL,
  `CollectItem` int(11) NOT NULL,
  `CollectAmount` double NOT NULL,
  `UseItem` int(11) NOT NULL,
  `UseAmount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `queststatus` (
  `Id` int(11) NOT NULL,
  `characterId` int(11) NOT NULL,
  `questId` int(11) NOT NULL,
  `isStarted` tinyint(1) NOT NULL,
  `isCompleted` tinyint(1) NOT NULL,
  `ItemsCollected` int(11) NOT NULL,
  `ItemsUsed` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `skill` (
  `Id` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `Skill` int(11) DEFAULT NULL,
  `SkillLearned` tinyint(1) DEFAULT NULL,
  `SkillLevel` int(11) NOT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `vehicle` (
  `Id` int(11) NOT NULL,
  `vehicleinfoId` int(11) NOT NULL,
  `CharacterId` int(11) NOT NULL,
  `Plate` varchar(10) DEFAULT NULL,
  `IsOnline` tinyint(1) NOT NULL,
  `HealthLevel` double NOT NULL,
  `FuelLevel` double NOT NULL,
  `IsLeased` tinyint(1) DEFAULT NULL,
  `IsFinanced` tinyint(1) DEFAULT NULL,
  `LeaseRate` double DEFAULT NULL,
  `FinanceRate` double DEFAULT NULL,
  `FinanceAmount` double DEFAULT NULL,
  `FinanceToPay` double DEFAULT NULL,
  `ModDataJSON` varchar(5000) DEFAULT NULL,
  `CustomDataJSON` varchar(5000) DEFAULT NULL,
  `GarageId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `vehicleinfo` (
  `Id` int(11) NOT NULL,
  `Hash` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Manufactor` longtext NOT NULL,
  `MinPrice` double NOT NULL,
  `Price` double NOT NULL,
  `MaxPrice` double NOT NULL,
  `Class` int(11) NOT NULL,
  `EngineType` int(11) NOT NULL,
  `Blacklist` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `account`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`);

ALTER TABLE `auctionhouse`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_characterauctionhouse` (`characterId`);

ALTER TABLE `character`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Characters_Accounts` (`AccountId`);

ALTER TABLE `clothes`
  ADD PRIMARY KEY (`Id`,`CharacterId`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Clothes_Characters` (`CharacterId`);

ALTER TABLE `communication`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Communications_Characters` (`CharacterId`);

ALTER TABLE `company`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Companies_Characters` (`CharacterId`);

ALTER TABLE `house`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Houses_Characters` (`CharacterId`);

ALTER TABLE `housekey`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`);

ALTER TABLE `inventory`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Inventories_Characters` (`CharacterId`);

ALTER TABLE `job`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Jobs_Characters` (`CharacterId`),
  ADD KEY `IX_FK_Jobs_Companies` (`CompaniesId`);

ALTER TABLE `logs`
  ADD PRIMARY KEY (`id`);

ALTER TABLE `message`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Messages_Characters` (`SenderId`);

ALTER TABLE `quest`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`);

ALTER TABLE `queststatus`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_characterqueststatus` (`characterId`),
  ADD KEY `IX_FK_questqueststatus` (`questId`);

ALTER TABLE `skill`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Skills_Characters` (`CharacterId`);

ALTER TABLE `vehicle`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `IX_FK_Vehicles_Characters` (`CharacterId`),
  ADD KEY `IX_FK_vehicleinfovehicle` (`vehicleinfoId`);

ALTER TABLE `vehicleinfo`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`);


ALTER TABLE `account`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

ALTER TABLE `auctionhouse`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `character`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

ALTER TABLE `clothes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `communication`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `company`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `house`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `housekey`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `inventory`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `job`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `logs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=213;

ALTER TABLE `message`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `quest`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `queststatus`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `skill`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `vehicle`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `vehicleinfo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;


ALTER TABLE `auctionhouse`
  ADD CONSTRAINT `FK_characterauctionhouse` FOREIGN KEY (`characterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `character`
  ADD CONSTRAINT `FK_Characters_Accounts` FOREIGN KEY (`AccountId`) REFERENCES `account` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `clothes`
  ADD CONSTRAINT `FK_Clothes_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `communication`
  ADD CONSTRAINT `FK_Communications_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `company`
  ADD CONSTRAINT `FK_Companies_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `house`
  ADD CONSTRAINT `FK_Houses_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `inventory`
  ADD CONSTRAINT `FK_Inventories_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `job`
  ADD CONSTRAINT `FK_Jobs_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_Jobs_Companies` FOREIGN KEY (`CompaniesId`) REFERENCES `company` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `message`
  ADD CONSTRAINT `FK_Messages_Characters` FOREIGN KEY (`SenderId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `queststatus`
  ADD CONSTRAINT `FK_characterqueststatus` FOREIGN KEY (`characterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_questqueststatus` FOREIGN KEY (`questId`) REFERENCES `quest` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `skill`
  ADD CONSTRAINT `FK_Skills_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE `vehicle`
  ADD CONSTRAINT `FK_Vehicles_Characters` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_vehicleinfovehicle` FOREIGN KEY (`vehicleinfoId`) REFERENCES `vehicleinfo` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
