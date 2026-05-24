var tv = new Tv();
var remote = new RemoteControl(tv);
Console.WriteLine("Tests with basic remote control:");
remote.TogglePower();

var radio = new Radio();
remote = new AdvancedRemoteControl(radio);
Console.WriteLine("\nTests with advanced remote control:");
remote.TogglePower();

public class RemoteControl(IDevice device)
{
    protected IDevice Device => device;

    public void TogglePower()
    {
        if (Device.IsEnabled())
        {
            Console.WriteLine("Remote: toggling power off...");
            Device.Disable();
        }
        else
        {
            Console.WriteLine("Remote: toggling power on...");
            Device.Enable();
        }
    }

    public void VolumeDown()
    {
        Device.SetVolume(Device.GetVolume() - 10);
    }

    public void VolumeUp()
    {
        Device.SetVolume(Device.GetVolume() + 10);
    }

    public void ChannelDown()
    {
        Device.SetChannel(Device.GetChannel() - 1);
    }
    public void ChannelUp()
    {
        Device.SetChannel(Device.GetChannel() + 1);
    }
}

public class AdvancedRemoteControl(IDevice device) : RemoteControl(device)
{
    public void Mute()
    {
        Device.SetVolume(0);
    }
}

public interface IDevice
{
    bool IsEnabled();

    void Enable();

    void Disable();

    int GetVolume();

    void SetVolume(int percent);

    int GetChannel();

    void SetChannel(int channel);
}

public class Tv : IDevice
{
    private bool _on = false;
    private int _volume = 30;
    private int _channel = 1;

    public bool IsEnabled() => _on;

    public void Enable() => _on = true;

    public void Disable() => _on = false;

    public int GetVolume() => _volume;

    public void SetVolume(int percent) => _volume = percent;

    public int GetChannel() => _channel;

    public void SetChannel(int channel) => this._channel = channel;
}

public class Radio : IDevice
{
    private bool _on = false;
    private int _volume = 30;
    private int _channel = 1;

    public bool IsEnabled() => _on;

    public void Enable() => _on = true;

    public void Disable() => _on = false;

    public int GetVolume() => _volume;

    public void SetVolume(int percent) => _volume = percent;

    public int GetChannel() => _channel;

    public void SetChannel(int channel) => this._channel = channel;
}
