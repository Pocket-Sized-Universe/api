using MessagePack;
using System.Buffers.Text;
using System.Text;

namespace PocketSizedUniverse.API.Dto.Files;

[MessagePackObject(keyAsPropertyName: true)]
public record TorrentFileDto : IFileDto
{
    public string ForbiddenBy { get; set; }
    public byte[] Hash { get; set; }
    public bool IsForbidden { get; set; }
    public string Extension { get; set; }
    public string Filename => Hash.ShortHash() + Extension;
    public string TorrentName => Hash.ShortHash() + ".torrent";
    public byte[] Data { get; set; }
    public string GamePath { get; set; }
}
public static class TorrentFileDtoExtensions
{
    public static string ShortHash(this byte[] hash)
    {
        return Convert.ToBase64String(hash).Replace("/", "_").Replace("+", "-");
    }
}