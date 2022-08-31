
DROP FUNCTION production.v_SelectProducts;

CREATE FUNCTION production.v_SelectProducts(in product integer, OUT product_name varchar(50), OUT product_id int, OUT list_price numeric)
RETURNS SETOF record
AS $$
BEGIN
    RETURN QUERY SELECT products.product_name, products.product_id, products.list_price FROM production.products WHERE products.product_id=product;
END;
$$
LANGUAGE plpgsql;
