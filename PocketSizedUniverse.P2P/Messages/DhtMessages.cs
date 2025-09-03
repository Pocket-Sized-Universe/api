using MessagePack;
using System.Net;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for DHT operations in the Kademlia distributed hash table
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
[Union(0, typeof(DhtFindNodeMessage))]
[Union(1, typeof(DhtFindNodeResponseMessage))]
[Union(2, typeof(DhtStoreMessage))]
[Union(3, typeof(DhtStoreResponseMessage))]
[Union(4, typeof(DhtFindValueMessage))]
[Union(5, typeof(DhtFindValueResponseMessage))]
[Union(6, typeof(DhtPingMessage))]
[Union(7, typeof(DhtPingResponseMessage))]
public abstract class DhtMessage : P2PMessage
{
    /// <summary>160-bit node ID for DHT operations</summary>
    public NodeInfo NodeInfo { get; set; } = new NodeInfo();
}

/// <summary>
/// Find nodes closest to a target ID in the DHT
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtFindNodeMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtFindNode;
    
    /// <summary>Target node ID to find closest nodes for</summary>
    public byte[] TargetId { get; set; } = [];
    
    /// <summary>Maximum number of nodes to return (K parameter)</summary>
    public int MaxNodes { get; set; } = 20;
}

/// <summary>
/// Response with closest known nodes to target
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtFindNodeResponseMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtFindNodeResponse;
    
    /// <summary>List of closest nodes to the target</summary>
    public List<NodeInfo> Nodes { get; set; } = [];
}

/// <summary>
/// Store a key-value pair in the DHT
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtStoreMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtStore;
    
    /// <summary>DHT key (e.g., "peer:12345", "group:abc123")</summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>Encrypted value to store</summary>
    public byte[] Value { get; set; } = [];
    
    /// <summary>Time-to-live in seconds</summary>
    public int TTL { get; set; } = 86400; // 24 hours
    
    /// <summary>Replication factor (how many nodes should store this)</summary>
    public int ReplicationFactor { get; set; } = 3;
}

/// <summary>
/// Confirmation of DHT store operation
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtStoreResponseMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtStoreResponse;
    
    /// <summary>Whether the store operation succeeded</summary>
    public bool Success { get; set; } = true;
    
    /// <summary>Error message if store failed</summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>Number of nodes that confirmed storage</summary>
    public int ReplicationCount { get; set; } = 1;
}

/// <summary>
/// Find value for a specific key in the DHT
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtFindValueMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtFindValue;
    
    /// <summary>DHT key to look up</summary>
    public string Key { get; set; } = string.Empty;
}

/// <summary>
/// Response with either the value or closest nodes
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtFindValueResponseMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtFindValueResponse;
    
    /// <summary>The requested value if found</summary>
    public byte[]? Value { get; set; }
    
    /// <summary>Closest nodes if value not found</summary>
    public List<NodeInfo> Nodes { get; set; } = [];
    
    /// <summary>When the value expires (Unix timestamp)</summary>
    public long? ExpirationTime { get; set; }
}

/// <summary>
/// Ping a node to check if it's alive
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtPingMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtPing;
}

/// <summary>
/// Response to ping confirming node is alive
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class DhtPingResponseMessage : DhtMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.DhtPingResponse;
    
    /// <summary>Node's current load (0.0 to 1.0)</summary>
    public float LoadFactor { get; set; } = 0.0f;
    
    /// <summary>Number of keys stored by this node</summary>
    public int StoredKeys { get; set; } = 0;
}
