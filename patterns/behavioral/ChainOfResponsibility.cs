
var monkey = new MonkeyHandler();
var squirrel = new SquirrelHandler();
var dog = new DogHandler();

monkey.SetNext(squirrel).SetNext(dog);

// The client should be able to send a request to any handler, not
// just the first one in the chain.
Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
ClientCode(monkey);
Console.WriteLine();

Console.WriteLine("Subchain: Squirrel > Dog\n");
ClientCode(squirrel);

static void ClientCode(AbstractHandler handler)
{
    foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
    {
        Console.WriteLine($"Client: Who wants a {food}?");

        var result = handler.Handle(food);

        if (result != null)
        {
            Console.Write($"   {result}");
        }
        else
        {
            Console.WriteLine($"   {food} was left untouched.");
        }
    }
}

// The Handler interface declares a method for building the chain of
// handlers. It also declares a method for executing a request.
public interface IHandler
{
    IHandler SetNext(IHandler handler);

    object Handle(object request);
}

// The default chaining behavior can be implemented inside a base handler
// class.
abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        _nextHandler = handler;

        // Returning a handler from here will let us link handlers in a
        // convenient way like this:
        // monkey.SetNext(squirrel).SetNext(dog);
        return handler;
    }

    public virtual object Handle(object request)
    {
        if (_nextHandler != null)
        {
            return _nextHandler.Handle(request);
        }
        else
        {
            return null;
        }
    }
}

class MonkeyHandler : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string) == "Banana")
        {
            return $"Monkey: I'll eat the {request.ToString()}.\n";
        }
        else
        {
            return base.Handle(request);
        }
    }
}

class SquirrelHandler : AbstractHandler
{
    public override object Handle(object request)
    {
        if (request.ToString() == "Nut")
        {
            return $"Squirrel: I'll eat the {request.ToString()}.\n";
        }
        else
        {
            return base.Handle(request);
        }
    }
}

class DogHandler : AbstractHandler
{
    public override object Handle(object request)
    {
        if (request.ToString() == "MeatBall")
        {
            return $"Dog: I'll eat the {request.ToString()}.\n";
        }
        else
        {
            return base.Handle(request);
        }
    }
}
