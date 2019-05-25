-- mysql
-- crete table
create database Book;
use Book;

--
-- User表
--
create table `User`
(
	`Id`			int				not null	auto_increment,
	`OpenId`		varchar(132)	not null,
	`NickName`		varchar(36)		not null,
	`ExpPoints`		int				not null	default 0,
	`ImageURL`		varchar(300),
	`Telephone`		char(11),
	`Gender`		int,
	`Birthday`		date,
	`Country`		varchar(104),
	`Province`		varchar(104),
	`City`			varchar(104),
	
	primary key (`Id`)
);


--
-- Book表
--
create table `Book`
(
	`Id`			int				not null	auto_increment,
	`SellerId`		int				not null,
	`Name`			varchar(104)	not null,
	`Remaining`		int				not null,
	`ISBN`			char(13)		not null,
	`Price`			decimal(11,2)	not null,
	`ImageURL`		varchar(300)	not null,
	`Category`		int				not null,
	`Press`			varchar(104)	not null,
	`PublishedDate`	date			not null,
	`Author`		varchar(64)		not null,
	`Depreciation`	int				not null,
	`Description`	varchar(1004)	not null,
	
	primary key (`Id`),
	foreign key (`SellerId`) references `User`(`Id`)
);


--
-- Reward表
--
create table `Reward`
(
	`Id`			int				not null	auto_increment,
	`UserId`		int				not null,
	`BookName`		varchar(104)	not null,
	`ISBN`			char(13)		not null,
	`Price`			decimal(11,2)	not null,
	`ImageURL`		varchar(300)	not null,
	`Category`		int				not null,
	`Press`			varchar(104)	not null,
	`PublishedDate`	date			not null,
	`Author`		varchar(64)		not null,
	`Depreciation`	int				not null,
	`Description`	varchar(1004)	not null,
	
	primary key (`Id`),
	foreign key (`UserId`) references `User`(`Id`)
);


--
-- Order表
--
create table `Order`
(
	`Id`			int				not null	auto_increment,
	`BuyerId`		int				not null,
	`BookId`		int				not null,
	`TimeStamp`		datetime		not null,
	`BuyerName`		varchar(34)		not null,
	`PhoneNumber`	varchar(11)		not null,
	`Address`		varchar(254)	not null,
	`Cost`			decimal(11,2)	not null,
	
	primary key (`Id`),
	foreign key (`BuyerId`) references `User`(`Id`),
	foreign key (`BookId`) references `Book`(`Id`)
);


--
-- ShoppingCart表
--
create table `ShoppingCart`
(
	`UserId`		int				not null,
	`BookId`		int				not null,
	`Number`		int				not null,
	
	primary key (`UserId`,`BookId`),
	foreign key (`UserId`) references `User`(`Id`),
	foreign key (`BookId`) references `Book`(`Id`)
);


--
-- Post表
--
create table `Post`
(
	`Id`			int				not null	auto_increment,
	`AuthorId`		int				not null,
	`TimeStamp`		datetime		not null,
	`Title`			varchar(44)		not null,
	`Content`		varchar(1004)	not null,
	
	primary key (`Id`),
	foreign key (`AuthorId`) references `User`(`Id`)
);


--
-- Comment表
--
create table `Comment`
(
	`Id`			int				not null	auto_increment,
	`AuthorId`		int				not null,
	`PostId`		int				not null,
	`TimeStamp`		datetime		not null,
	`Content`		varchar(1004)	not null,
	
	primary key (`Id`),
	foreign key (`AuthorId`) references `User`(`Id`),
	foreign key (`PostId`) references `Post`(`Id`)
);

