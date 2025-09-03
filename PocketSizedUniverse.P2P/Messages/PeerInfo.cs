using System.Net;
using MessagePack;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Information about a peer
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record PeerInfo
{
    /// <summary>Peer's public key</summary>
    public byte[] PublicKey { get; set; } = [];

    /// <summary>IP address</summary>
    public string IPAddress { get; set; } = string.Empty;

    /// <summary>Port number</summary>
    public int Port { get; set; } = 0;

    /// <summary>Last seen timestamp</summary>
    public DateTimeOffset LastSeen { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>Services this peer offers</summary>
    public List<string> Services { get; set; } = [];

    public IPEndPoint ToEndPoint() => new(System.Net.IPAddress.Parse(IPAddress), Port);
}