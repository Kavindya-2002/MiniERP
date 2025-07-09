CREATE DATABASE MiniERP;
GO

USE MiniERP;
GO

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Price DECIMAL(10, 2),
    Quantity INT
);

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100)
);

CREATE TABLE Sales (
    SaleId INT PRIMARY KEY IDENTITY,
    ProductId INT FOREIGN KEY REFERENCES Products(ProductId),
    CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId),
    Quantity INT,
    Total DECIMAL(10, 2),
    SaleDate DATETIME DEFAULT GETDATE()
);

SELECT * FROM Products;
