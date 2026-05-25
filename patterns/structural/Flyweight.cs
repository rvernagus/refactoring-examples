var forest = new Forest();
forest.PlantTree(10, 20, "Oak", "Green", "Rough");
forest.PlantTree(15, 25, "Pine", "Dark Green", "Smooth");
forest.PlantTree(20, 30, "Oak", "Green", "Rough"); // Reuses the same TreeType instance for Oak
forest.Draw();


// Flyweight
public class TreeType(string name, string color, string texture)
{
    public void Draw(int x, int y)
    {
        Console.WriteLine($"Displaying tree '{name}' of color '{color}' and texture '{texture}' at ({x}, {y})");
    }
}

// Flyweight Factory
public static class TreeFactory
{
    private static Dictionary<string, TreeType> treeTypes = new Dictionary<string, TreeType>();

    public static TreeType GetTreeType(string name, string color, string texture)
    {
        string key = $"{name}_{color}_{texture}";
        if (!treeTypes.ContainsKey(key))
        {
            treeTypes[key] = new TreeType(name, color, texture);
        }
        return treeTypes[key];
    }
}

// Context
public class Tree(int x, int y, TreeType type)
{
    public void Draw()
    {
        type.Draw(x, y);
    }
}

// Client
public class Forest
{
    private List<Tree> trees = new List<Tree>();

    public void PlantTree(int x, int y, string name, string color, string texture)
    {
        var type = TreeFactory.GetTreeType(name, color, texture);
        var tree = new Tree(x, y, type);
        trees.Add(tree);
    }

    public void Draw()
    {
        foreach (var tree in trees)
        {
            tree.Draw();
        }
    }
}