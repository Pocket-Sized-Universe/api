using MessagePack;
using System.Net;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for relay network operations
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public abstract class RelayMessage : P2PMessage
{
    /// <summary>Relay node ID</summary>
    public byte[] RelayNodeId { get; set; } = [];
}

/// <summary>
/// Join the relay network
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelayJoinMessage : RelayMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.RelayJoin;
    
    /// <summary>External IP address</summary>
    public string ExternalIP { get; set; } = string.Empty;
    
    /// <summary>Port number</summary>
    public int Port { get; set; } = 0;
    
    /// <summary>Services offered by this relay</summary>
    public List<string> Services { get; set; } = [];
    
    /// <summary>Protocol version supported</summary>
    public byte ProtocolVersion { get; set; } = 1;
}

/// <summary>
/// Response to relay join request
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelayJoinResponseMessage : RelayMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.RelayJoinResponse;
    
    /// <summary>Whether join was successful</summary>
    public bool Success { get; set; } = false;
    
    /// <summary>Known relay nodes</summary>
    public List<RelayNodeInfo> KnownRelays { get; set; } = [];
    
    /// <summary>Error message if join failed</summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Periodic heartbeat to maintain relay status
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelayHeartbeatMessage : RelayMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.RelayHeartbeat;
    
    /// <summary>Current load (0.0 to 1.0)</summary>
    public float Load { get; set; } = 0.0f;
    
    /// <summary>Number of connected peers</summary>
    public int ConnectedPeers { get; set; } = 0;
    
    /// <summary>Uptime in seconds</summary>
    public long Uptime { get; set; } = 0;
}

/// <summary>
/// Check relay health
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelayHealthCheckMessage : RelayMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.RelayHealthCheck;
}

/// <summary>
/// Response to health check
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelayHealthResponseMessage : RelayMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.RelayHealthResponse;
    
    /// <summary>Health status (OK, WARNING, ERROR)</summary>
    public string Status { get; set; } = "OK";
    
    /// <summary>Response time in milliseconds</summary>
    public int ResponseTime { get; set; } = 0;
    
    /// <summary>Additional health metrics</summary>
    public Dictionary<string, object> Metrics { get; set; } = [];
}

/// <summary>
/// Simple ping message
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PingMessage : P2PMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.Ping;
}

/// <summary>
/// Pong response to ping
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PongMessage : P2PMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.Pong;
}

/// <summary>
/// Information about a relay node
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record RelayNodeInfo
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
    public long LastSeen { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    
    /// <summary>Reputation score (0.0 to 1.0)</summary>
    public float Reputation { get; set; } = 1.0f;
    
    public IPEndPoint ToEndPoint() => new(System.Net.IPAddress.Parse(IPAddress), Port);
}
