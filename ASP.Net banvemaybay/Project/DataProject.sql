CREATE DATABASE AirTicketAgent
GO

USE AirTicketAgent
GO

CREATE TABLE Roles
(
	RoleID int identity(1,1) primary key,
	RoleName varchar(20)  ,
	Status	 varchar(20)  -- Active or Non Actice

)
GO

CREATE TABLE Account(
	
	AccountID int identity(101,1) primary key,
	Username varchar(50),
	Password varchar(50),
	Status	 varchar(20),  -- Active or Non Actice
	AccountRole	 int foreign key references dbo.Roles,
	DisplayName nvarchar(100)
)
GO

CREATE TABLE Employee(

	EmployeeID  int identity,
	Surname nvarchar(50),
	Name  nvarchar(15),
	Phone int,
	Email varchar(50),
	Sex varchar(5),   -- Nam or Nữ
	Age int,
	AccountID int foreign key references dbo.Account

)

Go


CREATE TABLE Customer(

	CustomerID  int identity,
	FullName nvarchar(100),
	Phone int,
	Email varchar(50),
	Sex varchar(5),   -- Nam or Nữ
	Age int,
	AccountID int foreign key references dbo.Account

)

Go




CREATE TABLE AirLine(
	AirLineID int identity primary key,
	AirLineName nvarchar(100),
	AirLineStatus varchar(20)


)
GO

CREATE TABLE Flight(

	FlightID int identity(101,1)  primary key,
	FlightDate varchar(50),			--  dd-MM-yyyy HH:mm
	ReturnDate nvarchar(50) null default 'Đây là vé một chiều',
	StartingPoint nvarchar(100),
	FinalDestination nvarchar(100),
	CategoryFlight nvarchar(10),
	StatusFlight varchar(20) null default 'active',						-- active va non active ( dung cho muc dich xoa )
	AirLineID int foreign key references dbo.AirLine
)
GO




CREATE TABLE AirTicket(

	AirTicketID int identity primary key,
	Price float,
	Discount int,
	StatusTicket nvarchar(15),		-- Chua ban hoac da ban
	ChairID varchar(5),
	TicketCateogry nvarchar(15),
	FlightID int foreign key references dbo.Flight
)
GO


CREATE TABLE Bill(

		ID int identity(201,1) primary key,
		TotalPirce int,
		TotalCount int,
		Oderer int foreign key references dbo.Account,
		CreateBillDate datetime not null default GetDate()



)
go

CREATE TABLE BillDeTail(

		ID int identity primary key,
		FlightID int foreign key references dbo.Flight,
		AirlineID int foreign key references dbo.AirLine,
		FlightDate varchar(50),	
		Price int,
		Status nvarchar(20),
		Discount int


)

go


Select * from Listoforders

CREATE TABLE Transactions(

		ID int identity primary key,
		Username varchar(50),
		TicketCount int,
		TicketCateogry nvarchar(15),
		AirLine nvarchar(50),
		Flight int ,
		StatusTrans nvarchar(15),   -- Chua xac nhan or Da xac nhan or Da huy
		TimeOrder datetime null default GetDate()

)

go



CREATE TABLE Listoforders
(
	ID int identity primary key,
	AirTicketID int,	-- Ma ve	
	Price float,
	ChairID varchar(10),	-- Ma ghe
	TicketCategory nvarchar(15),   -- Loại vé: Thường, Cao Cấp, Thương Gia  
	FlightID int,  -- Ma chuyen bay
	
	FlightDate varchar(50)  ,			-- Ngay bay--
	ReturnDate varchar(50) null default N'Đây là vé một chiều', -- 	  Ngay ve (neu co )		
	FlightCategory nvarchar(15),			-- Khu hoi hay mot chieu
	EmployUser  varchar(50),
	StatusOrder nvarchar(20),
	StartingPoint nvarchar(50),
	FinalDestination nvarchar(50),
	AirLineName nvarchar(50),
	Discount int,
	-- Phan code nhan vien nhap
	CustomerName nvarchar(50),		-- Ten nguoi mua	
	TransactionID int,
		
	TimeOrder datetime null default GetDate(),
)	

go


-- Phần code tạo dữ liệu

	-- Insert Roles
INSERT dbo.Roles Values ('admin','active')
go
INSERT dbo.Roles Values ('employee','active')
go
INSERT dbo.Roles Values ('customer','active')
go



-- Add tai khoan admin
INSERT dbo.Account Values ('admin1','202cb962ac59075b964b07152d234b70','active',1,N'Admin Minh')
go
-- Add tai khoan nhan vien
INSERT dbo.Account Values ('nhanvien1','202cb962ac59075b964b07152d234b70','active',2,N'Nhân viên My')
go
-- Add tai khoan khach hang
INSERT dbo.Account Values ('khachhang1','202cb962ac59075b964b07152d234b70','active',3,N'Khách Hàng Duyên')
go



Insert dbo.AirLine Values(N'Vietnam Airlines', 'active')
GO
Insert dbo.AirLine Values(N'Vietjet Air', 'active')
GO
Insert dbo.AirLine Values(N'Jetstar Pacific', 'active')
GO
Insert dbo.AirLine Values(N'Bamboo Airways', 'active')
GO


-- Insert Flight
	-- Vietnam Airlines
Insert dbo.Flight Values('26-12-2021 11:34','29-12-2021 11:34', N'TP. Hồ Chí Minh', N'Hà Nội',N'Khứ hồi','active',1)
GO

-- Vietjet Air
Insert dbo.Flight Values('14-06-2021 11:00','29-12-2021 11:34', N'TP. Hồ Chí Minh', N'Hà Nội',N'Khư hồi','active',2)
GO

-- Jetstar Pacific
Insert dbo.Flight Values('01-08-2021 08:02', '29-12-2021 11:34', N'TP. Hồ Chí Minh', N'Hà Nội',N'Khư hồi','active',3)
GO

-- Bamboo Airways'
Insert dbo.Flight Values('15-05-2021 06:17','', N'TP. Hồ Chí Minh', N'Hà Nội',N'Một chiều','active',4)
GO

Insert dbo.Flight Values('19-08-2021 06:17','', N'TP. Cần Thơ', N'TP.HCM',N'Một chiều','active',4)
GO


-- Insert AirTicket

--AirTicketID int identity primary key,
--	Price float,
--	Discount int,
--	StatusTicket nvarchar(15),											-- Chua ban hoac da ban
--	FlightID int foreign key references dbo.Flight


Select * from dbo.AirTicket

Insert dbo.AirTicket values(95000,0,N'Chưa bán','A1',N'Thường',101)
Insert dbo.AirTicket values(75000,0,N'Chưa bán','A1',N'Cao Cấp',102)
Insert dbo.AirTicket values(85000,0,N'Chưa bán','A1',N'Thương Gia',103)
Insert dbo.AirTicket values(90000,0,N'Chưa bán','A1',N'Thường',101)
Insert dbo.AirTicket values(65000,0,N'Chưa bán','A1',N'Thường',102)



select a.AirLineName,b.FlightID , c.AirTicketID, c.Price, c.Discount,c.ChairID, c.TicketCateogry,b.StartingPoint, b.FinalDestination, b.FlightDate, b.ReturnDate, b.CategoryFlight from dbo.AirLine as a, dbo.Flight as b , dbo.AirTicket as c where a.AirLineID = b.AirLineID and b.FlightID = 101



-- Những vé đã bán được

Select * from dbo.Listoforders as a where a.StatusOrder = N'Đã Thanh Toán'

