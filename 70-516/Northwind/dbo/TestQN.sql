USE [Northwind]
GO

update products
  set UnitPrice = 19.00
  where ProductID = 1

update products
  set UnitPrice = 18.00
  where ProductID = 1

select * from Products