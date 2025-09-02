using MessagePack;
using PocketSizedUniverse.API.Data;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for user/peer operations
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public abstract class UserMessage : P2PMessage
{
    /// <summary>User identity (Ed25519 public key as base58)</summary>
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Base class for peer discovery and presence
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public abstract class PeerMessage : P2PMessage
{
    /// <summary>Peer's Ed25519 public key</summary>
    public byte[] PeerPublicKey { get; set; } = [];
}

/// <summary>
/// Request to pair with another user
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPairRequestMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPairRequest;
    
    /// <summary>Target user to pair with</summary>
    public string TargetUserId { get; set; } = string.Empty;
    
    /// <summary>Optional pairing message</summary>
    public string? Message { get; set; }
}

/// <summary>
/// Response to pairing request
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPairResponseMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPairResponse;
    
    /// <summary>Whether the pairing was accepted</summary>
    public bool Accepted { get; set; } = false;
    
    /// <summary>User's display name/alias</summary>
    public string? DisplayName { get; set; }
    
    /// <summary>Response message</summary>
    public string? Message { get; set; }
}

/// <summary>
/// Accept a pairing request
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPairAcceptMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPairAccept;
    
    /// <summary>User being accepted</summary>
    public string AcceptedUserId { get; set; } = string.Empty;
}

/// <summary>
/// Reject a pairing request
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPairRejectMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPairReject;
    
    /// <summary>User being rejected</summary>
    public string RejectedUserId { get; set; } = string.Empty;
    
    /// <summary>Optional rejection reason</summary>
    public string? Reason { get; set; }
}

/// <summary>
/// Remove/unpair a user
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPairRemoveMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPairRemove;
    
    /// <summary>User being removed</summary>
    public string RemovedUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update user presence/online status
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class UserPresenceUpdateMessage : UserMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.UserPresenceUpdate;
    
    /// <summary>Whether user is online</summary>
    public bool IsOnline { get; set; } = true;
    
    /// <summary>Character name in game</summary>
    public string? CharacterName { get; set; }
    
    /// <summary>Current world/server</summary>
    public string? WorldName { get; set; }
    
    /// <summary>Display status message</summary>
    public string? StatusMessage { get; set; }
}

/// <summary>
/// Announce peer availability to DHT
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerAnnounceMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerAnnounce;
    
    /// <summary>Peer's IP address</summary>
    public string IPAddress { get; set; } = string.Empty;
    
    /// <summary>Peer's port</summary>
    public int Port { get; set; } = 0;
    
    /// <summary>Available files count</summary>
    public int FileCount { get; set; } = 0;
    
    /// <summary>Services offered by this peer</summary>
    public List<string> Services { get; set; } = [];
}

/// <summary>
/// Response to peer announcement
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerAnnounceResponseMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerAnnounceResponse;
    
    /// <summary>Whether announcement was accepted</summary>
    public bool Accepted { get; set; } = true;
    
    /// <summary>TTL for this peer info in seconds</summary>
    public long TTL { get; set; } = 3600; // 1 hour
}

/// <summary>
/// Look up peers for a specific user
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerLookupMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerLookup;
    
    /// <summary>User ID to find peers for</summary>
    public string TargetUserId { get; set; } = string.Empty;
}

/// <summary>
/// Response with peer information
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerLookupResponseMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerLookupResponse;
    
    /// <summary>Found peers for the user</summary>
    public List<PeerInfo> Peers { get; set; } = [];
}

/// <summary>
/// Update peer presence information
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerPresenceMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerPresence;
    
    /// <summary>Whether peer is online</summary>
    public bool IsOnline { get; set; } = true;
    
    /// <summary>Last activity timestamp</summary>
    public long LastActivity { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}

/// <summary>
/// Notify that peer is going offline
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class PeerOfflineMessage : PeerMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.PeerOffline;
    
    /// <summary>Reason for going offline</summary>
    public string Reason { get; set; } = "shutdown";
}

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
    public long LastSeen { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    
    /// <summary>Services this peer offers</summary>
    public List<string> Services { get; set; } = [];
}
