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
    public string ShortHash => Convert.ToBase64String(Hash);
    public string Filename => ShortHash + Extension;
    public string TorrentName => Hash + ".torrent";
    public byte[] Data { get; set; }
    public string GamePath { get; set; }
}