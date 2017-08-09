create table Staffs(
	StaffID int IDENTITY(1,1),
	FirstName varchar(255),
	LastName varchar(255),
	ContactNo varchar(255),
	PrivilegeLevel varchar(255),
	DOB date,
	uname varchar(255) UNIQUE,
	pwd varchar(255)

	PRIMARY KEY (StaffID),
	CONSTRAINT PL check (PrivilegeLevel='Admin' OR PrivilegeLevel='Assistant')
);

create table Customers(
	CustomerID int IDENTITY(1,1),
	FirstName varchar(255),
	LastName varchar(255),
	ContactNo varchar(255),
	NICno varchar(255) UNIQUE,
	DOB date,
	RegDate date,
	picture varchar(255)

	PRIMARY KEY(CustomerID),
);
create Table Categories(
	CatID int IDENTITY(1,1),
	CatName varchar(255)

	PRIMARY KEY (CatID)
);
create table Items(
	ItemID int IDENTITY(1,1),
	ItemName varchar(255)
);
create Table CategoriesItems(
	CatItemID int IDENTITY(1,1),
	CatID int,
	ItemID int,
	UnitPrice float

	PRIMARY KEY(CatItemID)
);

create Table Orders(
	OrderID int IDENTITY(1,1),
	CustID int,
	StaffID int,
	OrderDate date

	PRIMARY KEY (OrderID),
	FOREIGN KEY (CustID) references Customers(CustomerID),
	FOREIGN KEY (StaffID) references Staffs(StaffID)
);

create Table OrderedItems(
	OrderID int,
	CatItemID int,
	Quantity int

	PRIMARY KEY (OrderID, CatItemID),
	FOREIGN KEY (CatItemID) references CategoriesItems(CatItemID)


);

insert into Staffs (FirstName, LastName, ContactNo, PrivilegeLevel, DOB, uname, pwd) values('Default', 'Admin', '000-0000000', 'Admin', '1995-01-01', 'admin', 'pass')