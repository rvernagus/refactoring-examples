ClientCode();

static void ClientCode()
{
    var shapes = new List<Shape>
    {
        new Rectangle(10, 20, "red", 30, 40),
        new Circle(15, 25, "blue", 10)
    };

    var copiedShapes = shapes.Select(shape => shape.Copy()).ToList();

    foreach (var shape in copiedShapes)
    {
        Console.WriteLine(shape);
    }
}

// Prototype
public abstract record Shape(double X, double Y, string Color)
{
    public abstract Shape Copy();
}

// Concrete Prototype A
public record Rectangle(double X, double Y, string Color, double Width, double Height) : Shape(X, Y, Color)
{
    public override Shape Copy() => this with { };
}

// Concrete Prototype B
public record Circle(double X, double Y, string Color, double Radius) : Shape(X, Y, Color)
{
    public override Shape Copy() => this with { };
}
