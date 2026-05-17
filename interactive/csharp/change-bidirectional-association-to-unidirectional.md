change-bidirectional-association-to-unidirectional:csharp

###

1.en. Make sure that one of the following is true for your classes:<ul><li>Association is not used at all,</li><li>Another way of getting the associated object is available (such as by querying a database), or</li><li>The association object can be passed as an argument to the methods that use it.</li></ul>

2.en. Depending on your situation, instead of using a field containing an association with the relevant object, you may want to use a parameter, property, or method call for obtaining the associated object in a different way.

3.en. Delete the code that assigns the associated object to the field.

4.en. Delete the now-unused field.

###

```
public class Order
{
  // ...
  private Customer customer;

  public Customer Customer
  {
    get {
      return customer;
    }
    set {
      // Remove order from old customer.
      if (customer != null)
      {
        customer.Orders.Remove(this);
      }
      customer = value;
      // Add order to new customer.
      if (customer != null)
      {
        customer.Orders.Add(this);
      }
    }
  }

  public double GetDiscountedPrice()
  {
    return GetGrossPrice() * (1 - this.Customer.GetDiscount());
  }
}

public class Customer
{
  // ...
  private HashSet<Order> orders = new HashSet<Order>();

  // Should be used in Order class only.
  public HashSet<Order> Orders
  {
    get {
      return orders;
    }
  }

  public void AddOrder(Order order)
  {
    order.Customer = this;
  }

  public double GetPriceFor(Order order)
  {
     Assert.IsTrue(orders.Contains(order));
     return order.GetDiscountedPrice();
  }
}
```

###

```
public class Order
{
  // ...
  public Customer Customer
  {
    get {
      foreach (Customer customer in Customer.GetInstances())
      {
        if (customer.ContainsOrder(this))
          return customer;
      }
      return null;
    }
  }

  public double GetDiscountedPrice()
  {
    return GetGrossPrice() * (1 - this.Customer.GetDiscount());
  }
}

public class Customer
{
  // ...
  private HashSet<Order> orders = new HashSet<Order>();

  public void AddOrder(Order order)
  {
    orders.Add(order);
  }

  public double GetPriceFor(Order order)
  {
     Assert.IsTrue(orders.Contains(order));
     return order.GetDiscountedPrice();
  }
}
```

###

Set step 1

#|en| We will start <i>Change Bidirectional Association to Unidirectional</i> from the place where we have stopped in the inverse refactoring.

Select name of "Order"
+ Select name of "Customer"

#|en| In other words, we have <code>Customer</code> and <code>Order</code> classes with a bidirectional association.

#|en| Two new methods have been added to the code since completion of the previous refactoring.

Select name of "GetPriceFor"

#|en|V First, the method for getting order price in the customer object.

+ Select name of "GetDiscountedPrice"

#|en| Then the method for getting price with discount in the order class.

Select "private |||Customer||| customer;"

#|en| Few days ago a new requirement was received, which says that orders must only be created only for existing customers. This lets us eliminate the bidirectional association between orders and customer, and only keeping customers aware of their orders.

Select "|||this.Customer|||.GetDiscount()"

#|en| The hardest part of this refactoring technique is making sure that it is possible. Refactoring itself is easy, but we must make sure that it is safe. The problem comes down to whether any of order class' code needs a customer field. If that is the case, removing the field requires you to provide an alternative method for getting the customer object.

Set step 2

#|en|^= First, we review all usages of the customer field and it's getter. Is there another way to provide the customer object or it's data? Often thу best solution means passing the customer as an argument to the methods, which use the field.

Go to parameters of "GetDiscountedPrice"

Print "Customer customer"

Wait 500ms

Select "|||this.Customer|||.GetDiscount()"

Replace "customer"


Select:
```
     return order.GetDiscountedPrice();
```

#|en| This works particularly well for methods that called by client code already containing a customer object. In this case, you just pass it as a method's argument.

Go to:
```
     return order.GetDiscountedPrice(|||);
```

Print "this"

Wait 1000ms

Select "    |||return customer;|||"

#|en| Another alternative to consider is changing the getter of the property that allows it to get the customer without using the field. Then you can apply <a href="/substitute-algorithm">Replace Algorithm</a> to the body of <code>Order.Customer.get</code> and do something similar to the actions outlined below.

Print:
```
foreach (Customer customer in Customer.GetInstances())
      {
        if (customer.ContainsOrder(this))
          return customer;
      }
      return null;
```

Select parameters of "GetDiscountedPrice"

#|en| The previous insertion of the parameter in the method can now be removed, since the getter of the <code>this.Customer</code> property will return the correct object.

Remove selected

Select "|||customer|||.GetDiscount()"

Replace "this.Customer"


Select "GetDiscountedPrice(|||this|||);"
Remove selected

Select:
```
    |||get||| {
      foreach (Customer customer in Customer.GetInstances())
```

#|en| Slow… But it works. In the context of a database, things may even become a little faster if a database query is used.

Set step 3

Select:
```
  // Should be used in Order class only.
  public HashSet<Order> Orders
  {
    get {
      return orders;
    }
  }

```

+ Select body of "AddOrder"

#|en| Now you can prepare to remove use of the <code>order.Customer</code> property, replacing its assignment in the code of the customer class with direct addition of order objects to the collection.

Select:
```

  // Should be used in Order class only.
  public HashSet<Order> Orders
  {
    get {
      return orders;
    }
  }

```

Remove selected

Select body of "AddOrder"

Type:
```
    orders.Add(order);
```

Select:
```
    set {
      // Remove order from old customer.
      if (customer != null)
      {
        customer.Orders.Remove(this);
      }
      customer = value;
      // Add order to new customer.
      if (customer != null)
      {
        customer.Orders.Add(this);
      }
    }

```

#|en| Then remove the property setter in the order class where assignment of the new customer value had taken place.

Remove selected

Set step 4

Select:
```
  private Customer customer;


```

#|en| At last, we can remove the field itself, fully eliminating the bidirectional association between the classes.

Remove selected

#C|ru| Запускаем финальную компиляцию и тестирование.
#S Отлично, все работает!

#C|en| Let's perform the final compilation and testing.
#S Wonderful, it's all working!

#C|uk| Запускаємо фінальну компіляцію і тестування.
#S Супер, все працює.

Set final step

#|en|Q The refactoring is complete! You can compare the old and new code if you like.