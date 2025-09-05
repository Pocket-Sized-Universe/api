using MessagePack;

namespace PocketSizedUniverse.API.Dto.Files;

[MessagePackObject(keyAsPropertyName: true)]
public record TorrentFileDto : IFileDto
{
    public string ForbiddenBy { get; set; }
    public string Hash { get; set; }
    public bool IsForbidden { get; set; }
    public string Extension { get; set; }
    public string Filename => Hash + Extension;
    public string TorrentName => Hash + ".torrent";
    public byte[] Data { get; set; }
}