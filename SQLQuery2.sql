select c.Name Customers from customers c left outer join orders o on c.Id = o.CustomerId where o.CustomerId is NULL
/*select * from orders o left outer join customers c on c.Id = o.CustomerId where o.CustomerId=null*/
