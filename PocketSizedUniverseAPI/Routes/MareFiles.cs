namespace PocketSizedUniverse.API.Routes;

public class MareFiles
{
    public const string Magnets = "/magnets";

    public Uri GetMagnetsUri(string baseUrl, string hash) => new Uri($"{baseUrl}{Magnets}/{hash}");
    public Uri PostMagnetsUri(string baseUrl, string hash) => new Uri($"{baseUrl}{Magnets}/{hash}");
}