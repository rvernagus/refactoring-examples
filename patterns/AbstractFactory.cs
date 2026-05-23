Console.WriteLine("App: Launched with the Windows Factory.");
ClientCode(new WinFactory());
Console.WriteLine();
Console.WriteLine("App: Launched with the Mac Factory.");
ClientCode(new MacFactory());

static void ClientCode(GuiFactory factory)
{
    var button = factory.CreateButton();
    var checkbox = factory.CreateCheckbox();
    button.Paint();
    checkbox.Paint();
}

// Abstract Factory
public interface GuiFactory
{
    IButton CreateButton();

    ICheckbox CreateCheckbox();
}

// Concrete Factory A
public class WinFactory : GuiFactory
{
    public IButton CreateButton() => new WinButton();

    public ICheckbox CreateCheckbox() => new WinCheckbox();
}

// Concrete Factory B
public class MacFactory : GuiFactory
{
    public IButton CreateButton() => new MacButton();

    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

// Abstract Product A
public interface IButton
{
    void Paint();
}

// Abstract Product B
public interface ICheckbox
{
    void Paint();
}

// Concrete Product A1
public class WinButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("  Painting Windows Button");
    }
}

// Concrete Product B1
public class WinCheckbox : ICheckbox
{
    public void Paint()
    {
        Console.WriteLine("  Rendering Windows Checkbox");
    }
}

// Concrete Product A2
public class MacButton : IButton
{
    public void Paint()
    {
        Console.WriteLine("  Painting Mac Button");
    }
}

// Concrete Product B2
public class MacCheckbox : ICheckbox
{
    public void Paint()
    {
        Console.WriteLine("  Rendering Mac Checkbox");
    }
}