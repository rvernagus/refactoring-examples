var director = new Director();
ClientCode(director);


static void ClientCode(Director director)
{
    Console.WriteLine("Building a sports car:");
    var carBuilder = new CarBuilder();
    director.ConstructSportsCar(carBuilder);
    var car = carBuilder.GetProduct();

    Console.WriteLine("\nBuilding an SUV manual:");
    var manualBuilder = new CarManualBuilder();
    director.ConstructSuv(manualBuilder);
    var manual = manualBuilder.GetProduct();
}

public class Car
{
    
}

public class Manual
{
    
}

// Builder
public interface IBuilder
{
    IBuilder Reset();

    IBuilder SetSeats(int number);

    IBuilder SetEngine(string type);

    IBuilder SetTripComputer(bool has);

    IBuilder SetGPS(bool has);
}

public class CarBuilder : IBuilder
{
    private Car _car = new Car();

    public IBuilder Reset()
    {
        _car = new Car();
        return this;
    }

    public IBuilder SetSeats(int number)
    {
        Console.WriteLine($"  Setting {number} seats");
        return this;
    }

    public IBuilder SetEngine(string type)
    {
        Console.WriteLine($"  Setting engine: {type}");
        return this;
    }

    public IBuilder SetTripComputer(bool has)
    {
        Console.WriteLine($"  Setting trip computer: {(has ? "Yes" : "No")}");
        return this;
    }

    public IBuilder SetGPS(bool has)
    {
        Console.WriteLine($"  Setting GPS: {(has ? "Yes" : "No")}");
        return this;
    }

    public Car GetProduct() => _car;
}

public class CarManualBuilder : IBuilder
{
    private Manual _manual = new Manual();

    public IBuilder Reset()
    {
        _manual = new Manual();
        return this;
    }

    public IBuilder SetSeats(int number)
    {
        Console.WriteLine($"  Describing {number} seats in manual");
        return this;
    }

    public IBuilder SetEngine(string type)
    {
        Console.WriteLine($"  Describing engine: {type} in manual");
        return this;
    }

    public IBuilder SetTripComputer(bool has)
    {
        Console.WriteLine($"  Describing trip computer: {(has ? "Yes" : "No")} in manual");
        return this;
    }

    public IBuilder SetGPS(bool has)
    {
        Console.WriteLine($"  Describing GPS: {(has ? "Yes" : "No")} in manual");
        return this;
    }

    public Manual GetProduct() => _manual;
}

// Director
public class Director
{
    public void ConstructSportsCar(IBuilder builder)
    {
        builder
            .Reset()
            .SetSeats(2)
            .SetEngine("V8")
            .SetTripComputer(true)
            .SetGPS(true);
    }

    public void ConstructSuv(IBuilder builder)
    {
        builder
            .Reset()
            .SetSeats(5)
            .SetEngine("V6")
            .SetTripComputer(true)
            .SetGPS(true);
    }
}