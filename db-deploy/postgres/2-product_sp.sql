DROP PROCEDURE if exists production.sp_SelectProducts;

Create OR REPLACE PROCEDURE production.sp_SelectProducts (product varchar(30))

LANGUAGE plpgsql AS

$$ BEGIN

SELECT * FROM production.products WHERE product_id=product;

END $$;