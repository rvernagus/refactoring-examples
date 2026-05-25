var converter = new VideoConverter();
var oggFile = converter.Convert("example.avi", "ogg");
oggFile.Save();

public class VideoFile(string filePath)
{
    public void Save()
    {
        Console.WriteLine($"Saving video file: {this}");
    }

    override public string ToString() => $"VideoFile({filePath})";
}

// Subsystem 1
public class OggCompressionCodec
{
    public void Compress(VideoFile file)
    {
        Console.WriteLine($"Compressing video file using OGG format: {file}");
    }
}

// Subsystem 2
public class MPEGCompressionCodec
{    public void Compress(VideoFile file)
    {
        Console.WriteLine($"Compressing video file using MPEG format: {file}");
    }
}

// Facade
public class VideoConverter
{
    public VideoFile Convert(string filePath, string format)
    {
        var file = new VideoFile(filePath);
        if (format == "ogg")
        {
            var oggCodec = new OggCompressionCodec();
            oggCodec.Compress(file);
        }
        else if (format == "mpeg")
        {
            var mpegCodec = new MPEGCompressionCodec();
            mpegCodec.Compress(file);
        }
        return file;
    }
}