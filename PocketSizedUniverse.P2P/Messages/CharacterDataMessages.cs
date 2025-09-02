using MessagePack;
using PocketSizedUniverse.API.Data;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for character data operations
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public abstract class CharacterDataMessage : P2PMessage
{
    /// <summary>Character data hash for identification</summary>
    public string DataHash { get; set; } = string.Empty;
}

/// <summary>
/// Push character data to paired users
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class CharacterDataPushMessage : CharacterDataMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.CharacterDataPush;
    
    /// <summary>Complete character data</summary>
    public CharacterData? Data { get; set; }
    
    /// <summary>List of users this data should be sent to</summary>
    public List<string> TargetUsers { get; set; } = [];
}

/// <summary>
/// Request character data from a peer
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class CharacterDataRequestMessage : CharacterDataMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.CharacterDataRequest;
    
    /// <summary>User whose character data is requested</summary>
    public string RequestedUserId { get; set; } = string.Empty;
}

/// <summary>
/// Response with character data
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class CharacterDataResponseMessage : CharacterDataMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.CharacterDataResponse;
    
    /// <summary>Character data if available</summary>
    public CharacterData? Data { get; set; }
    
    /// <summary>Whether data was found</summary>
    public bool Found { get; set; } = false;
    
    /// <summary>Error message if data unavailable</summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Notify of character data updates
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class CharacterDataUpdateMessage : CharacterDataMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.CharacterDataUpdate;
    
    /// <summary>Updated character data</summary>
    public CharacterData? Data { get; set; }
    
    /// <summary>What changed (appearance, mods, etc.)</summary>
    public List<string> ChangedComponents { get; set; } = [];
}

/// <summary>
/// Notify paired users of character data changes
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class CharacterDataNotifyMessage : CharacterDataMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.CharacterDataNotify;
    
    /// <summary>User whose data changed</summary>
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>New data hash</summary>
    public string NewDataHash { get; set; } = string.Empty;
    
    /// <summary>Brief description of changes</summary>
    public string? ChangeDescription { get; set; }
}
