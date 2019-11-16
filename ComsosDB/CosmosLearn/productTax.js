function producttax(price) {
    if (price == undefined)
        throw 'no input';

    var amount = parseFloat(price);

    if (amount < 1000)
        return amount * 0.1;
    else if (amount < 10000)
        return amount * 0.2;
    else
        return amount * 0.4;
}

//to execute
//SELECT c.id, c.productId, c.price, udf.producttax(c.price) AS producttax FROM c