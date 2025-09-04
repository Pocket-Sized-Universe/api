using PocketSizedUniverse.API.Data;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.CharaData;

[MessagePackObject(keyAsPropertyName: true)]
public record CharaDataDownloadDto(string Id, UserData Uploader) : CharaDataDto(Id, Uploader)
{
    public string GlamourerData { get; init; } = string.Empty;
    public string CustomizeData { get; init; } = string.Empty;
    public string ManipulationData { get; set; } = string.Empty;
    public List<FileRedirectEntry> FileRedirects { get; init; } = [];
    public List<TorrentFileEntry> FileSwaps { get; init; } = [];
}