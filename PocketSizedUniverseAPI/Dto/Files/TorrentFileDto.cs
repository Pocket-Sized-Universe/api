using MessagePack;

namespace PocketSizedUniverse.API.Dto.Files;

[MessagePackObject(keyAsPropertyName: true)]
public record TorrentFileDto : ITransferFileDto
{
    public bool FileExists { get; set; } = true;
    public string Hash { get; set; } = string.Empty;
    public bool IsForbidden { get; set; } = false;
    public string ForbiddenBy { get; set; } = string.Empty;
    public string MagnetLink { get; set; } = string.Empty;
}