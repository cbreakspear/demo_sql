DROP PROCEDURE IF EXISTS production.sp_SelectProducts;

DELIMITER //
CREATE PROCEDURE production.sp_SelectProducts(IN product int)
BEGIN
SELECT * FROM production.products WHERE product_id=product;
END//
DELIMITER;





