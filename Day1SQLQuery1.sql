create database ecommerce_assignment_db;

use ecommerce_assignment_db;

create table Customer
(CustomerId int identity(1,1) primary key,
CustomerName varchar(100) not null,
Email varchar(100) not null unique,
MobileNo varchar(50) null,
City varchar(100) not null,
Address varchar(100) not null,
IsActive bit not null default 1,
CreatedDate datetime not null default getdate()
);

create table Seller
(
SellerId int identity(1,1) primary key,
SellerName varchar(100) not null,
Email varchar(100) not null unique,
MobileNo varchar(15) not null,
City varchar(50) not null,
Rating decimal(10,2),
IsActive bit not null default 1
);

create table Product
(
ProductId int identity(1,1) primary key,
ProductName varchar(100) not null,
Category varchar(100) not null,
Price decimal(10,2) not null check (Price > 0),
StockQuantity int not null check (StockQuantity >= 0),
SellerId int not null,
CreatedDate datetime not null default getdate(),
constraint fk_product_seller foreign key (SellerId) references Seller(SellerId)
);

create table Orders
(
OrderId int identity(1,1) primary key,
CustomerId int not null,
OrderDate datetime not null default getdate(),
OrderStatus varchar(100) not null,
PaymentMode varchar(100) not null,
DeliveryCity varchar(100) not null,
constraint fk_orders_customer foreign key(CustomerId) references Customer(CustomerId)
);

create table OrderItem
(
OrderItemId int identity(1,1) primary key,
OrderId int not null,
ProductId int not null,
Quantity int not null check (Quantity > 0),
UnitPrice decimal(10,2) not null,
constraint fk_orderitem_orders foreign key(OrderId) references Orders(OrderId),
constraint fk_orderitem_product foreign key (ProductId) references Product(ProductId)
);

INSERT INTO Customer (CustomerName, Email, MobileNo, City, Address) VALUES
('Amit Sharma', 'amit@gmail.com', '9876543210', 'Chennai', '12 Pallavaram'),
('Bala Kumar', 'bala@gmail.com', '8765432109', 'Bangalore', '45 MG Road'),
('Chitra Ram', 'chitra@yahoo.com', '7654321098', 'Chennai', '78 Velachery'),
('Deepak Raj', 'deepak@gmail.com', NULL, 'Hyderabad', '90 Gachibowli'),
('Eshwar Rao', 'eshwar@outlook.com', '6543210987', 'Mumbai', '34 Bandra');

INSERT INTO Product (ProductName, Category, Price, StockQuantity, SellerId) VALUES
('iPhone 15', 'Mobile', 79999.00, 15, 1),
('Galaxy S24', 'Mobile', 74999.00, 8, 1),
('MacBook Air', 'Laptop', 99999.00, 5, 2),
('Dell Inspiron', 'Laptop', 55000.00, 12, 3),
('Sony Headphones', 'Electronics', 9999.00, 25, 2),
('BoAt Earbuds', 'Electronics', 1999.00, 50, 4),
('OnePlus 12R', 'Mobile', 39999.00, 20, 1),
('Unused Dummy Phone', 'Mobile', 500.00, 2, 4);

INSERT INTO Orders (CustomerId, OrderStatus, PaymentMode, DeliveryCity) VALUES
(1, 'Delivered', 'UPI', 'Chennai'),
(2, 'Pending', 'Credit Card', 'Bangalore'),
(1, 'Shipped', 'COD', 'Chennai'),
(3, 'Delivered', 'Net Banking', 'Chennai'),
(4, 'Cancelled', 'UPI', 'Hyderabad');


INSERT INTO Seller (SellerName, Email, MobileNo, City, Rating) VALUES
('Appario Retail', 'appario@seller.com', '9988776655', 'Bangalore', 4.5),
('Cloudtail India', 'cloudtail@seller.com', '8877665544', 'Chennai', 4.2),
('SuperCom Net', 'supercom@seller.com', '7766554433', 'Mumbai', 3.9),
('RetailNet', 'retailnet@seller.com', '6655443322', 'Delhi', 4.0);


INSERT INTO OrderItem (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1, 4, 1, 79999.00),
(1, 8, 2, 9999.00), 
(2, 6, 1, 99999.00),
(2, 9, 1, 1999.00), 
(3, 5, 1, 74999.00),
(4, 7, 1, 55000.00),
(4, 8, 1, 9999.00), 
(5, 10, 1, 39999.00),
(3, 9, 3, 1999.00),
(1, 9, 2, 1999.00);

select * from OrderItem;

select * from Customer where City = 'Chennai';

select * from Customer where City <> 'Chennai';

select * from Product where Price > 50000;

select * from Product where Price between 10000 and 60000;

select * from Product where Category in ('Mobile','Laptop');

select * from Customer where CustomerName like 'A%';

SELECT * FROM Customer WHERE Email LIKE '%gmail%';

select * from Product where ProductName like '%Phone%';

select * from Orders where OrderStatus = 'Delivered';

select * from Product where StockQuantity < 10;

select * from Customer where MobileNo is not null;

select * from Product where Price not between 10000 and 50000;

select * from Customer where City in ('Chennai','Bangalore');

select * from Customer where City = 'Chennai' and IsActive = 1;

select * from Customer where City <> 'Hyderabad';

select City,count(*) as TotalCustomers from Customer group by City;

select Category, count(*) as TotalProducts from Product group by Category;

select Category, sum(StockQuantity) as TotalStock from Product group by Category;

select Category, max(Price) as MaxPrice from Product group by Category;

select Category, min(Price) as MinPrice from Product group by Category;

select Category, avg(Price) as AvgPrice from Product group by Category;

select o.CustomerId , sum(oi.Quantity * oi.UnitPrice) as TotalOrderAmount from Orders o join OrderItem oi on o.OrderId = oi.OrderId group by o.CustomerId;

select ProductId, sum(Quantity * UnitPrice) as TotalSales from OrderItem group by ProductId;

select ProductId, sum(Quantity) as TotalQuantitySold from OrderItem group by ProductId;
select Category, count(*) as ProductCount from Product group by Category having count(*) > 1;

select o.CustomerId, sum(oi.Quantity * oi.UnitPrice) as TotalAmount from Orders o join OrderItem oi on o.OrderId = oi.OrderId group by o.CustomerId having sum(oi.Quantity * oi.UnitPrice) > 50000;

select SellerId, count(*) as TotalProducts from Product group by SellerId;

select p.SellerId, sum(oi.Quantity * oi.UnitPrice) as TotalSales from OrderItem oi join Product p on oi.ProductId = p.ProductId group by p.SellerId;

select OrderStatus, count(*) as OrderCount from Orders group by OrderStatus;


select * from Product order by Price asc;

select * from Product order by Price desc;

select * from Customer order by City asc, CustomerName asc;

select * from Orders order by OrderDate desc;

select * from Product order by Category asc, Price desc;

select top 3 from Product order by Price desc;

select top 5 from Orders order by OrderDate desc;

select * from Customer order by IsActive asc, CustomerName asc;


SELECT o.OrderId, o.OrderDate, o.OrderStatus, c.CustomerName, c.Email 
FROM Orders o
INNER JOIN Customer c ON o.CustomerId = c.CustomerId;

SELECT p.ProductId, p.ProductName, p.Price, s.SellerName, s.City 
FROM Product p
INNER JOIN Seller s ON p.SellerId = s.SellerId;

SELECT oi.OrderItemId, oi.OrderId, p.ProductName, oi.Quantity, oi.UnitPrice 
FROM OrderItem oi
INNER JOIN Product p ON oi.ProductId = p.ProductId;

SELECT c.CustomerName, o.OrderId, o.OrderDate, p.ProductName, oi.Quantity, oi.UnitPrice, s.SellerName
FROM Orders o
INNER JOIN Customer c ON o.CustomerId = c.CustomerId
INNER JOIN OrderItem oi ON o.OrderId = oi.OrderId
INNER JOIN Product p ON oi.ProductId = p.ProductId
INNER JOIN Seller s ON p.SellerId = s.SellerId;

SELECT c.CustomerId, c.CustomerName, o.OrderId, o.OrderStatus 
FROM Customer c
LEFT JOIN Orders o ON c.CustomerId = o.CustomerId;

SELECT o.OrderId, o.OrderStatus, c.CustomerId, c.CustomerName 
FROM Orders o
RIGHT JOIN Customer c ON o.CustomerId = c.CustomerId;

SELECT c.CustomerId, c.CustomerName, o.OrderId, o.OrderStatus 
FROM Customer c
FULL OUTER JOIN Orders o ON c.CustomerId = o.CustomerId;

SELECT c.CustomerName, p.ProductName 
FROM Customer c
CROSS JOIN Product p;

SELECT c.* FROM Customer c
LEFT JOIN Orders o ON c.CustomerId = o.CustomerId
WHERE o.OrderId IS NULL;

SELECT p.* FROM Product p
LEFT JOIN OrderItem oi ON p.ProductId = oi.ProductId
WHERE oi.OrderItemId IS NULL;

SELECT s.SellerName, p.ProductName, p.Category, p.Price 
FROM Seller s
INNER JOIN Product p ON s.SellerId = p.SellerId
ORDER BY s.SellerName;

SELECT DISTINCT c.CustomerName, p.ProductName 
FROM Customer c
INNER JOIN Orders o ON c.CustomerId = o.CustomerId
INNER JOIN OrderItem oi ON o.OrderId = oi.OrderId
INNER JOIN Product p ON oi.ProductId = p.ProductId;

SELECT OrderId, SUM(Quantity * UnitPrice) AS TotalOrderAmount 
FROM OrderItem 
GROUP BY OrderId;

SELECT s.SellerName, SUM(oi.Quantity * oi.UnitPrice) AS TotalSalesAmount
FROM Seller s
INNER JOIN Product p ON s.SellerId = p.SellerId
INNER JOIN OrderItem oi ON p.ProductId = oi.ProductId
GROUP BY s.SellerName;

SELECT p.ProductName, SUM(oi.Quantity) AS TotalQuantitySold
FROM Product p
INNER JOIN OrderItem oi ON p.ProductId = oi.ProductId
GROUP BY p.ProductName;


select * from Product where Price > (select avg(Price) from Product );

select * from Product where StockQuantity < (select avg(StockQuantity) from Product);

select * from Customer where CustomerId not in (select distinct CustomerId from Orders);

select * from Customer where CustomerId in (select distinct CustomerId from Orders);

select * from Product where ProductId not in (select distinct ProductId from OrderItem);

select * from Product where ProductId in (select distinct ProductId from OrderItem);

select * from Seller where SellerId not in (select distinct SellerId from Product);

select * from Seller where SellerId in (select distinct SellerId from Product);

select * from Orders where CustomerId in (select CustomerId from Customer where City = 'Chennai');

select * from Orders where CustomerId in (select CustomerId from Customer where City = 'Bangalore');


select * from Customer where CustomerId in (select CustomerId from Orders);

select * from Customer where CustomerId not in (select CustomerId from Orders);

select * from Product where ProductId in (select ProductId from OrderItem);

select * from Product where ProductId not in (select ProductId from OrderItem);

select * from Seller where SellerId in (select SellerId from Product);

select * from Seller where SellerId not in (select SellerId from Product);

select * from Orders where OrderId in (select OrderId from OrderItem where ProductId in (select ProductId from Product where Category = 'Mobile'));

select * from Orders where OrderId in (select OrderId from OrderItem where ProductId in (select ProductId from Product where Category = 'Laptop'));


select * from Product where Price = (select max(Price) from Product);

select * from Product where Price = (select min(Price) from Product);

select * from Product where Price > (select avg(Price) from Product);

select * from Product where Price < (select avg(Price) from Product);

select * from Customer where CustomerId in (
select o.CustomerId from Orders o join OrderItem oi on o.OrderId = oi.OrderId group by o.CustomerId having sum(oi.Quantity * oi.UnitPrice) > 
( select avg(TotalAmount) from (select sum(Quantity * UnitPrice) as TotalAmount from OrderItem group by OrderId) as AvgOrders)
);

select * from product where ProductId in (
select ProductId from OrderItem group by ProductId having sum(Quantity) > (select avg(TotalQty) from (select sum(Quantity) as TotalQty from OrderItem group by ProductId) as QtyTable)
);

select top 1 * from Customer where CustomerId in (
select o.CustomerId from Orders o join OrderItem oi on o.OrderId = oi.OrderId group by o.CustomerId order by sum(oi.Quantity * oi.UnitPrice) desc
);


select * from Product p1 where Price > (select avg(Price) from Product p2 where p2.Category = p1.Category);

select * from Product p1 where Price < (select avg(Price) from Product p2 where p2.Category = p1.Category);

select * from Seller s where (select count(*) from Product p where p.SellerId = s.SellerId) > 2;

select * from Customer c where (select count(*) from Orders o where o.CustomerId = c.CustomerId) > 1;

select * from Product p1 where StockQuantity > (select avg(StockQuantity) from Product p2 where p2.Category = p1.Category);

select * from Seller s where (select avg(Price) from Product p where p.SellerId = s.SellerId) > (select avg(Price) from Product);



select * from Customer c where exists (select 1 from Orders o where o.CustomerId = c.CustomerId);

select * from Customer c where not exists (select 1 from Orders o where o.CustomerId = c.CustomerId);

select * from Product p where exists (select 1 from OrderItem oi where oi.ProductId=p.ProductId);

select * from Seller s where exists (select 1 from Product p where p.SellerId = s.SellerId);

select * from Seller s where not exists (select 1 from Product p where p.SellerId = s.SellerId);

select * from Customer c where exists (
select 1 from Orders o join OrderItem oi on o.OrderId = oi.OrderId join Product p on oi.ProductId = p.ProductId where o.CustomerId = c.CustomerId and p.Category = 'Mobile'
);

select * from Customer c where not exists (
select 1 from Orders o join OrderItem oi on o.OrderId = oi.OrderId join Product p on oi.ProductId = p.ProductId where o.CustomerId = c.CustomerId and p.Category = 'Laptop'
);;



create procedure sp_GetAllCustomer 
as 
begin 
select * from Customer;
end;

exec sp_GetAllCustomer;

create procedure sp_GetAllProducts 
as 
begin
select * from Product;
end;

exec sp_GetAllProducts;

create procedure sp_GetAllSellers
as
begin
select * from Seller;
end;

create procedure sp_GetAllOrders
as
begin
select * from Orders;
end;

create procedure sp_GetAllOrderItems
as
begin
select * from OrderItem;
end;


create procedure sp_GetCustomerById 
@CustomerId int 
as
begin
select * from Customer where CustomerId = @CustomerId;
end;

create procedure sp_GetProductById 
@ProductId int 
as
begin
select * from Product where ProductId = @ProductId;
end;





