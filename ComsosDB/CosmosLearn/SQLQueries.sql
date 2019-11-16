
--cosdb sql format
SELECT <select_list>
    [FROM <optional_from_specification>]
    [WHERE <optional_filter_condition>]
    [ORDER BY <optional_sort_specification>]
    [JOIN <optional_join_specification>]

--SELECT 
SELECT *
  FROM Products p
  WHERE p.id ="1"

--
SELECT
     p.id,
     p.manufacturer,
     p.description
  FROM Products p
  WHERE p.id ="1"

  --ALIAS
  SELECT p.id FROM Products AS p

  --QUERY INNER ARRAY

  SELECT *
  FROM Products.shipping

  --QUERY SPECIFIC PROPERTY
  SELECT *
  FROM Products.shipping.weight

  --WHERE CLAUSE
  SELECT p.description
  FROM Products p
  WHERE p.id = "1"#

  --ORDER BY
  SELECT p.price, p.description, p.productId
  FROM Products p
  ORDER BY p.price ASC

  --JOIN this is an inner join where product documents have an inner shipping array
  SELECT p.productId
  FROM Products p
  JOIN p.shipping