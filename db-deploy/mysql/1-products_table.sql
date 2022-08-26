
-- create schemas
if schema_id('production') is null begin
    execute('create schema production');
end;
go

if not exists(select * from sys.sequences where [name] = 'Ids')
begin
    create sequence dbo.Ids
    as int
    start with 1;
end
go
/* if schema_id('sales') is null begin
    execute('create schema sales');
end;
go */

-- create tables
/* CREATE TABLE production.categories (
	category_id INT IDENTITY (1, 1) PRIMARY KEY,
	category_name VARCHAR (255) NOT NULL
);

CREATE TABLE production.brands (
	brand_id INT IDENTITY (1, 1) PRIMARY KEY,
	brand_name VARCHAR (255) NOT NULL
); */
drop table if exists production.products;
CREATE TABLE production.products (
	product_id INT IDENTITY (1, 1) PRIMARY KEY,
	product_name VARCHAR (255) NOT NULL,
	brand_id INT NOT NULL,
	category_id INT NOT NULL,
	model_year SMALLINT NOT NULL,
	list_price DECIMAL (10, 2) NOT NULL
);
go

if not exists(select * from sys.change_tracking_databases where database_id = db_id())
begin
    alter database current 
    set change_tracking = on
    (change_retention = 30 days, auto_cleanup = on)
end
go

if not exists(select * from sys.change_tracking_tables where [object_id]=object_id('production.products'))
begin
    alter table production.products
    enable change_tracking
end
go
