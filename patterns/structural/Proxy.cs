var youtubeService = new YoutubeProxy(new ThirdPartyYoutubeClass());
var manager = new YoutubeManager(youtubeService);
manager.ReactOnUserInput();

// Service Interface
public interface IThirdPartyYoutubeLib
{
    IEnumerable<string> ListVideos();

    string GetVideoInfo(int videoId);

    void DownloadVideo(int videoId);
}

// Service Implementation (Expensive to create)
public class ThirdPartyYoutubeClass : IThirdPartyYoutubeLib
{
    public ThirdPartyYoutubeClass()
    {        // Simulate expensive object creation
        Console.WriteLine("Initializing ThirdPartyYoutubeClass...");
        Thread.Sleep(2000); // Simulate delay
    }

    public IEnumerable<string> ListVideos()
    {
        return new List<string> { "Video1", "Video2", "Video3" };
    }

    public string GetVideoInfo(int videoId)
    {
        return $"Info for Video {videoId}";
    }

    public void DownloadVideo(int videoId)
    {
        Console.WriteLine($"Downloading Video {videoId}...");
    }
}

// Proxy
public class YoutubeProxy(ThirdPartyYoutubeClass service) : IThirdPartyYoutubeLib
{
    private IList<string>? cachedVideoList = null;

    public IEnumerable<string> ListVideos()
    {
        if (cachedVideoList == null)
        {
            cachedVideoList = service.ListVideos().ToList();
        }
        return cachedVideoList;
    }

    public string GetVideoInfo(int videoId)
    {
        return service.GetVideoInfo(videoId);
    }

    public void DownloadVideo(int videoId)
    {
        service.DownloadVideo(videoId);
    }
}

// Client
public class YoutubeManager(IThirdPartyYoutubeLib youtubeService)
{
    public void RenderVideoPage()
    {
        var videos = youtubeService.ListVideos();
        Console.WriteLine("Available Videos:");
        foreach (var video in videos)
        {
            Console.WriteLine(video);
        }
    }

    public void RenderListPanel()
    {
        var videos = youtubeService.ListVideos();
        Console.WriteLine("Video List Panel:");
        foreach (var video in videos)
        {
            Console.WriteLine(video);
        }
    }

    public void ReactOnUserInput()
    {
        RenderVideoPage();
        RenderListPanel();
    }
}