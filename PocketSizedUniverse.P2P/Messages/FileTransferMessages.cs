using MessagePack;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for file transfer operations
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public abstract class FileTransferMessage : P2PMessage
{
    /// <summary>SHA-256 hash of the file being transferred</summary>
    public string FileHash { get; set; } = string.Empty;
}

/// <summary>
/// Request a file from a peer
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class FileRequestMessage : FileTransferMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.FileRequest;
}

/// <summary>
/// Response to file request with file data or error
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class FileRequestResponseMessage : FileTransferMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.FileRequestResponse;
    
    /// <summary>Whether the file is available</summary>
    public bool Available { get; set; } = false;
    
    /// <summary>File data if available</summary>
    public byte[]? FileData { get; set; }
    
    /// <summary>Error message if unavailable</summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>File metadata (name, size, etc.)</summary>
    public Dictionary<string, string> Metadata { get; set; } = [];
}

/// <summary>
/// Query which files a peer has available
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class FileAvailabilityQueryMessage : P2PMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.FileAvailabilityQuery;
    
    /// <summary>List of file hashes to check</summary>
    public List<string> FileHashes { get; set; } = [];
}

/// <summary>
/// Response with file availability information
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class FileAvailabilityResponseMessage : P2PMessage
{
    public override MessageTypeV2 Type => MessageTypeV2.FileAvailabilityResponse;
    
    /// <summary>List of available file hashes</summary>
    public List<string> AvailableFiles { get; set; } = [];
}
