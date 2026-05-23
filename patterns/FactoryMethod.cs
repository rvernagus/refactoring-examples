
Console.WriteLine("App: Launched with Windows Dialog.");
ClientCode(new WindowsDialog());
Console.WriteLine();
Console.WriteLine("App: Launched with Web Dialog.");
ClientCode(new WebDialog());

static void ClientCode(Dialog dialog)
{
    dialog.Render();
}

// Creator
public abstract class Dialog
{
    public abstract IButton CreateButton();

    public void Render()
    {
        var okButton = CreateButton();
        okButton.OnClick();
        okButton.Render();
    }
}

// Concrete Creator A
public class WindowsDialog : Dialog
{
    public override IButton CreateButton() => new WindowsButton();
}

// Concrete Creator B
public class WebDialog : Dialog
{
    public override IButton CreateButton() => new WebButton();
}

// Product
public interface IButton
{
    void Render();

    void OnClick();
}

// Concrete Product A
public class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("  Rendering Windows Button");
    }

    public void OnClick()
    {
        Console.WriteLine("  Windows Button Clicked");
    }
}

// Concrete Product B
public class WebButton : IButton
{
    public void Render()
    {
        Console.WriteLine("  Rendering Web Button");
    }

    public void OnClick()
    {
        Console.WriteLine("  Web Button Clicked");
    }
}