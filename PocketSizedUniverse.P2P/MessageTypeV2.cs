using MessagePack;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// P2P Message types for the distributed PocketSizedUniverse protocol.
/// Based on ranges to avoid conflicts and support protocol versioning.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public enum MessageTypeV2 : byte
{
    // === DHT Operations (0-19) ===
    DhtFindNode = 0,
    DhtFindNodeResponse = 1,
    DhtStore = 2,
    DhtStoreResponse = 3,
    DhtFindValue = 4,
    DhtFindValueResponse = 5,
    DhtPing = 6,
    DhtPingResponse = 7,
    
    // === Relay Network Management (20-39) ===
    RelayJoin = 20,
    RelayJoinResponse = 21,
    RelayAnnounce = 22,
    RelayHeartbeat = 23,
    RelayHealthCheck = 24,
    RelayHealthResponse = 25,
    RelayNodeList = 26,
    RelayNodeListResponse = 27,
    
    // === Peer Discovery & Registration (40-59) ===
    PeerAnnounce = 40,
    PeerAnnounceResponse = 41,
    PeerLookup = 42,
    PeerLookupResponse = 43,
    PeerPresence = 44,
    PeerPresenceResponse = 45,
    PeerOffline = 46,
    
    // === NAT Traversal & Connection (60-79) ===
    NatProbe = 60,
    NatProbeResponse = 61,
    NatPunchRequest = 62,
    NatPunchCoordinate = 63,
    ConnectionRequest = 64,
    ConnectionResponse = 65,
    
    // === P2P Handshake & Security (80-99) ===
    P2PHandshake = 80,
    P2PHandshakeResponse = 81,
    P2PKeyExchange = 82,
    P2PKeyExchangeResponse = 83,
    P2PAuthChallenge = 84,
    P2PAuthChallengeResponse = 85,
    
    // === File Transfer Protocol (100-119) ===
    FileRequest = 100,
    FileRequestResponse = 101,
    FileChunkStart = 102,
    FileChunk = 103,
    FileChunkAck = 104,
    FileComplete = 105,
    FileError = 106,
    FileCancel = 107,
    FileAvailabilityQuery = 108,
    FileAvailabilityResponse = 109,
    
    // === User Management & Pairing (120-139) ===
    UserPairRequest = 120,
    UserPairResponse = 121,
    UserPairAccept = 122,
    UserPairReject = 123,
    UserPairRemove = 124,
    UserPresenceUpdate = 125,
    UserPermissionUpdate = 126,
    UserProfileUpdate = 127,
    
    // === Group Management (140-159) ===
    GroupCreate = 140,
    GroupCreateResponse = 141,
    GroupJoin = 142,
    GroupJoinResponse = 143,
    GroupLeave = 144,
    GroupInvite = 145,
    GroupInviteResponse = 146,
    GroupMemberUpdate = 147,
    GroupPermissionUpdate = 148,
    GroupDelete = 149,
    
    // === Character Data Transfer (160-179) ===
    CharacterDataPush = 160,
    CharacterDataRequest = 161,
    CharacterDataResponse = 162,
    CharacterDataUpdate = 163,
    CharacterDataNotify = 164,
    CharacterDataDelete = 165,
    
    // === Error Handling & Status (240-255) ===
    Error = 240,
    StatusUpdate = 241,
    Ping = 254,
    Pong = 255
}
