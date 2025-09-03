using System.Net;
using MessagePack;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Information about a relay node
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record NodeInfo
{
    /// <summary>Node ID</summary>
    public byte[] NodeId { get; set; } = [];

    /// <summary>Public key</summary>
    public byte[] PublicKey { get; set; } = [];

    /// <summary>External IP address</summary>
    public string IPAddress { get; set; } = string.Empty;

    /// <summary>Port number</summary>
    public int Port { get; set; } = 0;

    /// <summary>Services offered</summary>
    public List<string> Services { get; set; } = [];

    /// <summary>Last seen timestamp</summary>
    public DateTimeOffset LastSeen { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>Reputation score (0.0 to 1.0)</summary>
    public float Reputation { get; set; } = 1.0f;

    public IPEndPoint ToEndPoint() => new(System.Net.IPAddress.Parse(IPAddress), Port);
}