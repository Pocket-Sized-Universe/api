namespace PocketSizedUniverse.API.Routes;

public class MareFiles
{
    public const string Torrents = "/torrents";
    public const string Torrents_GetSuperSeederPackage = "getSuperSeederPackage";

    public static Uri GetSuperSeederPackage(Uri baseUri, int batchSize) =>
        new Uri(baseUri, Torrents + "/" + Torrents_GetSuperSeederPackage + "?batchSize=" + batchSize);
}