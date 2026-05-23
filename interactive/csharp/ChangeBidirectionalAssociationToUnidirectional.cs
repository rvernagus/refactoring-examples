using System.Diagnostics;

Console.WriteLine("Hello World!");

public class Order
{
    // ...
    private Customer? customer;

    public Customer Customer
    {
        get
        {
            return customer;
        }
        set
        {
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

    private double GetGrossPrice()
    {
        // ...
        return 0;
    }
}

public class Customer
{
    // ...
    private HashSet<Order> orders = new HashSet<Order>();

    // Should be used in Order class only.
    public HashSet<Order> Orders
    {
        get
        {
            return orders;
        }
    }

    public void AddOrder(Order order)
    {
        order.Customer = this;
    }

    public double GetPriceFor(Order order)
    {
        Debug.Assert(orders.Contains(order));
        return order.GetDiscountedPrice();
    }

    public double GetDiscount()
    {
        // ...
        return 0;
    }
}
