var compoundGraphic = new CompoundGraphic();
compoundGraphic.Add(new Dot(1, 2));
compoundGraphic.Add(new Circle(5, 7, 10));
compoundGraphic.Move(10, 10);
compoundGraphic.Draw();


// Component
public interface IGraphic
{
    void Move(int x, int y);

    void Draw();
}

// Leaf
public class Dot(int x, int y) : IGraphic
{
    public void Move(int dx, int dy)
    {
        x += dx;
        y += dy;
    }

    public void Draw()
    {
        Console.WriteLine($"Dot: {x}, {y}");
    }
}

// Component
public class Circle(int x, int y, int radius) : IGraphic
{
    public void Move(int dx, int dy)
    {
        x += dx;
        y += dy;
    }

    public void Draw()
    {
        Console.WriteLine($"Circle: {x}, {y}, radius: {radius}");
    }
}

// Composite
public class CompoundGraphic : IGraphic
{
    private List<IGraphic> _children = new List<IGraphic>();

    public void Add(IGraphic child)
    {
        _children.Add(child);
    }

    public void Remove(IGraphic child)
    {
        _children.Remove(child);
    }

    public void Move(int x, int y)
    {
        foreach (var child in _children)
        {
            child.Move(x, y);
        }
    }

    public void Draw()
    {
        foreach (var child in _children)
        {
            child.Draw();
        }
    }
}
