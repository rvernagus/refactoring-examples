// The client code can parameterize an invoker with any commands.
var invoker = new Invoker();
invoker.SetOnStart(new SimpleCommand("Say Hi!"));
var receiver = new Receiver();
invoker.SetOnFinish(new ComplexCommand(receiver, "Send email", "Save report"));

invoker.DoSomethingImportant();

// The Command interface declares a method for executing a command.
public interface ICommand
{
    void Execute();
}

// Some commands can implement simple operations on their own.
class SimpleCommand(string payload) : ICommand
{
    public void Execute()
    {
        Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({payload})");
    }
}

// However, some commands can delegate more complex operations to other
// objects, called "receivers."
class ComplexCommand(Receiver receiver, string a, string b) : ICommand
{
    // Commands can delegate to any methods of a receiver.
    public void Execute()
    {
        Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
        receiver.DoSomething(a);
        receiver.DoSomethingElse(b);
    }
}

// The Receiver classes contain some important business logic. They know how
// to perform all kinds of operations, associated with carrying out a
// request. In fact, any class may serve as a Receiver.
class Receiver
{
    public void DoSomething(string a)
    {
        Console.WriteLine($"Receiver: Working on ({a}.)");
    }

    public void DoSomethingElse(string b)
    {
        Console.WriteLine($"Receiver: Also working on ({b}.)");
    }
}

// The Invoker is associated with one or several commands. It sends a
// request to the command.
class Invoker
{
    private ICommand? _onStart;

    private ICommand? _onFinish;

    // Initialize commands.
    public void SetOnStart(ICommand command)
    {
        this._onStart = command;
    }

    public void SetOnFinish(ICommand command)
    {
        this._onFinish = command;
    }

    // The Invoker does not depend on concrete command or receiver classes.
    // The Invoker passes a request to a receiver indirectly, by executing a
    // command.
    public void DoSomethingImportant()
    {
        Console.WriteLine("Invoker: Does anybody want something done before I begin?");
        if (this._onStart is ICommand)
        {
            this._onStart.Execute();
        }

        Console.WriteLine("Invoker: ...doing something really important...");

        Console.WriteLine("Invoker: Does anybody want something done after I finish?");
        if (this._onFinish is ICommand)
        {
            this._onFinish.Execute();
        }
    }
}
