CREATE SCHEMA IF NOT EXISTS production;

drop table if exists production.products;
CREATE TABLE production.products (
	product_id SERIAl PRIMARY KEY,
	product_name VARCHAR (255) NOT NULL,
	brand_id INT NOT NULL,
	category_id INT NOT NULL,
	model_year SMALLINT NOT NULL,
	list_price DECIMAL (10, 2) NOT NULL
);