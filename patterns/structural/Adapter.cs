var hole = new RoundHole(5);
var rpeg = new RoundPeg(5);
Console.WriteLine(hole.Fits(rpeg)); // True

var smallSqPeg = new SquarePeg(5);
var largeSqPeg = new SquarePeg(10);
// hole.Fits(smallSqPeg); // Compilation error

var smallSqPegAdapter = new SquarePegAdapter(smallSqPeg);
var largeSqPegAdapter = new SquarePegAdapter(largeSqPeg);
Console.WriteLine(hole.Fits(smallSqPegAdapter)); // True
Console.WriteLine(hole.Fits(largeSqPegAdapter)); // False

// Target
public class RoundHole(double radius)
{
    public double Radius => radius;

    public bool Fits(RoundPeg peg) => radius >= peg.Radius;
}

// Target
public class RoundPeg(double radius)
{
    public double Radius => radius;
}

// Adaptee
public class SquarePeg(double width)
{
    public double Width => width;
}

// Adapter
public class SquarePegAdapter(SquarePeg peg) : RoundPeg(peg.Width * Math.Sqrt(2) / 2)
{
}
