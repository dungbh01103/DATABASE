CREATE DATABASE Iphone;
USE Iphone;
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,   
    SellingPrice DECIMAL(18, 2) NOT NULL,
    InventoryQuantity INT NOT NULL,       
    ImportedQuantity INT DEFAULT 0,       
    Cost DECIMAL(18, 2) NOT NULL,        
    Image VARBINARY(MAX),                 
    CreatedAt DATETIME DEFAULT GETDATE(), 
    UpdatedAt DATETIME DEFAULT GETDATE()  
);
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeName NVARCHAR(100) NOT NULL,  
    Position NVARCHAR(50) NOT NULL,       
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,     
    Authority NVARCHAR(50) NOT NULL,     
    CreatedAt DATETIME DEFAULT GETDATE(), 
    UpdatedAt DATETIME DEFAULT GETDATE()  
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,  
    Gender VARCHAR(20),                
    Email VARCHAR(255),                 
    PhoneNumber NVARCHAR(15) NOT NULL,    
    Address NVARCHAR(255) NULL,           
    CreatedAt DATETIME DEFAULT GETDATE(), 
    UpdatedAt DATETIME DEFAULT GETDATE()  
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,             
    CustomerName NVARCHAR(100) NOT NULL, 
    EmployeeID INT,                       
    EmployeeName NVARCHAR(100) NOT NULL,  
    OrderDate DATETIME DEFAULT GETDATE(), 
    TotalAmount DECIMAL(18, 2) NOT NULL,  
    Profit DECIMAL(18, 2) DEFAULT 0,      
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT ,                 
    ProductID INT ,               
    Quantity INT NOT NULL,                
    Price DECIMAL(18, 2) NOT NULL,       
    Profit DECIMAL(18, 2) DEFAULT 0,      
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT ,         
    EmployeeID INT NOT NULL,             
    EmployeeName NVARCHAR(100) NOT NULL, 
    InvoiceDate DATETIME DEFAULT GETDATE(), 
    Amount DECIMAL(18, 2) NOT NULL,      
    PaymentMethod NVARCHAR(50) NOT NULL, 
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100) NOT NULL,  
    ContactName NVARCHAR(100) NOT NULL,   
    Email VARCHAR(255)UNIQUE,
	Phone NVARCHAR(15) UNIQUE,         
    Address NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()  
);

CREATE TABLE ProductSupplier (
    ProductID INT NOT NULL,   
    SupplierID INT NOT NULL,  
    CreatedBy INT,           
    PRIMARY KEY (ProductID, SupplierID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (CreatedBy) REFERENCES Employees(EmployeeID)
);

INSERT INTO Products (ProductName, SellingPrice, InventoryQuantity, ImportedQuantity, Cost, Image)
VALUES
('Iphone 14', 25000.00, 100, 50, 18000.00,  (SELECT * FROM OPENROWSET(BULK N'D:\Pictured\anh1.jpg', SINGLE_BLOB) AS Image)),  -- Iphone 14
('Iphone 13', 22000.00, 120, 40, 17000.00, (SELECT * FROM OPENROWSET(BULK N'D:\Pictured\anh2.jpg', SINGLE_BLOB) AS Image)),  -- Iphone 13
('Iphone 12', 20000.00, 150, 60, 15000.00, (SELECT * FROM OPENROWSET(BULK N'D:\Pictured\anh3.jpg', SINGLE_BLOB) AS Image)),  -- Iphone 12
('Iphone SE', 15000.00, 200, 80, 12000.00, (SELECT * FROM OPENROWSET(BULK N'D:\Pictured\anh4.jpg', SINGLE_BLOB) AS Image)),  -- Iphone SE
('Iphone 15', 28000.00, 80, 30, 20000.00, (SELECT * FROM OPENROWSET(BULK N'D:\Pictured\anh5.jpg', SINGLE_BLOB) AS Image));   -- Iphone 15

Select * from Products
INSERT INTO Employees (EmployeeName, Position, Username, Password, Authority)
VALUES
('Nguyen Van A', 'Manager', 'dung', '123', 'Admin'),
('Tran Thi B', 'Sales', 'tranthib', 'password456', 'Sales'),
('Le Thi C', 'Cashier', 'lethic', 'password789', 'Cashier'),
('Pham Quoc D', 'Support', 'phamquocd', 'password012', 'Support'),
('Nguyen Thi E', 'Sales', 'nguyenthie', 'password345', 'Sales');
Select * from Employees

INSERT INTO Customers (CustomerName, Gender, Email, PhoneNumber, Address)
VALUES
('Le Minh T', 'Male', 'leminht@example.com', '0987654321', '123 Nguyen Trai, HCM'),
('Nguyen Thi M', 'Female', 'nguyenthiM@example.com', '0123456789', '456 Le Loi, HN'),
('Tran Minh L', 'Male', 'tranminhl@example.com', '0912345678', '789 Phan Chu Trinh, HCM'),
('Pham Thi A', 'Female', 'phamthia@example.com', '0945678910', '321 Ba Trieu, HN'),
('Nguyen Minh P', 'Male', 'nguyenminhp@example.com', '0981234567', '654 Hoang Hoa Tham, HCM');

Select * from Customers
INSERT INTO Orders (CustomerID, CustomerName, EmployeeID, EmployeeName, TotalAmount, Profit)
VALUES
(1, 'Le Minh T', 2, 'Tran Thi B', 25000.00, 5000.00),
(2, 'Nguyen Thi M', 1, 'Nguyen Van A', 22000.00, 4000.00),
(3, 'Tran Minh L', 3, 'Le Thi C', 20000.00, 3000.00),
(4, 'Pham Thi A', 4, 'Pham Quoc D', 18000.00, 2000.00),
(5, 'Nguyen Minh P', 5, 'Nguyen Thi E', 28000.00, 6000.00);

Select * from Orders
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price, Profit)
VALUES
(1, 1, 1, 25000.00, 5000.00),
(2, 2, 1, 22000.00, 4000.00),
(3, 3, 1, 20000.00, 3000.00),
(4, 4, 1, 15000.00, 2000.00),
(5, 5, 1, 35000.00, 4000.00);
Select * from OrderDetails

INSERT INTO Suppliers (SupplierName, ContactName, Email, Phone, Address) 
VALUES 
('TechSupply Co.', 'John Doe', 'johndoe@techsupply.com', '0987654321', '123 Tech Street, Hanoi'),
('Global Traders', 'Jane Smith', 'janesmith@globaltraders.com', '0981234567', '45 Trade Avenue, HCM City'),
('EcoMaterials Ltd.', 'Michael Nguyen', 'michael@ecomaterials.vn', '0912345678', '89 Green Road, Da Nang'),
('Prime Supplies', 'Emily Tran', 'emily@primesupplies.vn', '0923456789', '56 Main Street, Can Tho'),
('MegaTools', 'Kevin Pham', 'kevin@megatools.com', '0909876543', '78 Tool Lane, Hue');
Select * from Suppliers

INSERT INTO Invoices (OrderID, EmployeeID, EmployeeName, InvoiceDate, Amount, PaymentMethod)
VALUES
(1, 1, 'Nguyen Van A', '2024-1-21 10:30', 1500.00, 'Credit Card'),
(2, 2, 'Tran Thi B', '2024-2-22 11:45:00', 2500.00, 'Cash'),
(3, 3, 'Le Van C', '2024-3-13 09:15:00', 3200.50, 'Bank Transfer'),
(4, 1, 'Nguyen Van A', '2024-4-14 14:20:00', 4200.00, 'PayPal'),
(5, 4, 'Pham Thi D', '2024-5-25 17:00:00', 2800.00, 'Cash');
select * from Invoices



SELECT o.OrderID, o.CustomerName, p.ProductName, od.Quantity, e.EmployeeName
FROM Orders o
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
INNER JOIN Products p ON od.ProductID = p.ProductID
INNER JOIN Employees e ON o.EmployeeID = e.EmployeeID;

SELECT c.CustomerName, COUNT(o.OrderID) AS NumberOfOrders
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerName;

SELECT o.OrderID, COUNT(od.ProductID) AS ProductCount
FROM Orders o
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID
GROUP BY o.OrderID;


SELECT ProductName, SellingPrice FROM Products WHERE ProductID = 1;

