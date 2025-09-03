using MessagePack;
using System.Security.Cryptography;
using NSec.Cryptography;

namespace PocketSizedUniverse.P2P;

/// <summary>
/// Base class for all P2P messages with MessagePack serialization,
/// Ed25519 signatures, and protocol versioning.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
[Union(0, typeof(DhtMessage))]
[Union(1, typeof(RelayMessage))]
[Union(2, typeof(PeerMessage))]
[Union(3, typeof(FileTransferMessage))]
[Union(4, typeof(UserMessage))]
[Union(5, typeof(CharacterDataMessage))]
public abstract class P2PMessage
{
    /// <summary>Protocol version for compatibility checking</summary>
    public byte Version { get; set; } = 1;
    
    /// <summary>Message type identifier</summary>
    public abstract MessageTypeV2 Type { get; }
    
    /// <summary>Unique message ID for correlation</summary>
    public Guid MessageId { get; set; } = Guid.NewGuid();
    
    /// <summary>UTC timestamp when message was created</summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    /// <summary>Sender's Ed25519 public key (32 bytes)</summary>
    public byte[]? SenderPublicKey { get; set; }
    
    /// <summary>Ed25519 signature of message content (64 bytes)</summary>
    public byte[]? Signature { get; set; }
    
    /// <summary>Optional correlation ID for request/response pairs</summary>
    public Guid? CorrelationId { get; set; }
    
    /// <summary>
    /// Serialize message to MessagePack binary format
    /// </summary>
    public virtual byte[] Serialize()
    {
        return MessagePackSerializer.Serialize(this);
    }
    
    /// <summary>
    /// Deserialize MessagePack binary data to P2PMessage
    /// </summary>
    public static T Deserialize<T>(byte[] data) where T : P2PMessage
    {
        return MessagePackSerializer.Deserialize<T>(data);
    }
    
    /// <summary>
    /// Sign this message with the provided Ed25519 private key
    /// </summary>
    public void Sign(byte[] privateKey, byte[] publicKey)
    {
        SenderPublicKey = publicKey;
        
        // Serialize message content without signature
        var originalSignature = Signature;
        Signature = null;
        var messageBytes = Serialize();
        Signature = originalSignature;
        
        // Sign the serialized content using NSec Ed25519
        var algorithm = SignatureAlgorithm.Ed25519;
        using var key = Key.Import(algorithm, privateKey, KeyBlobFormat.RawPrivateKey);
        Signature = algorithm.Sign(key, messageBytes);
    }
    
    /// <summary>
    /// Verify the Ed25519 signature on this message
    /// </summary>
    public bool VerifySignature()
    {
        if (SenderPublicKey == null || Signature == null)
            return false;
        
        // Serialize message content without signature
        var originalSignature = Signature;
        Signature = null;
        var messageBytes = Serialize();
        Signature = originalSignature;
        
        // Verify signature using NSec Ed25519
        try
        {
            var algorithm = SignatureAlgorithm.Ed25519;
            var publicKey = PublicKey.Import(algorithm, SenderPublicKey, KeyBlobFormat.RawPublicKey);
            return algorithm.Verify(publicKey, messageBytes, Signature);
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Create a response message with correlation ID set
    /// </summary>
    public T CreateResponse<T>() where T : P2PMessage, new()
    {
        return new T
        {
            CorrelationId = MessageId,
            Version = Version
        };
    }
    
    /// <summary>
    /// Check if message has expired based on timestamp and TTL
    /// </summary>
    public bool IsExpired(TimeSpan ttl)
    {
        var messageTime = DateTimeOffset.FromUnixTimeMilliseconds(Timestamp);
        return DateTimeOffset.UtcNow > messageTime.Add(ttl);
    }
}

/// <summary>
/// Message envelope with length prefix for network transport
/// Format: [4-byte length][MessagePack serialized P2PMessage]
/// </summary>
public static class MessageEnvelope
{
    public const int MaxMessageSize = 16 * 1024 * 1024; // 16MB max message size
    
    /// <summary>
    /// Wrap message with 4-byte length prefix for network transmission
    /// </summary>
    public static byte[] Wrap(P2PMessage message)
    {
        var messageBytes = message.Serialize();
        var lengthBytes = BitConverter.GetBytes(messageBytes.Length);
        
        var envelope = new byte[4 + messageBytes.Length];
        Array.Copy(lengthBytes, 0, envelope, 0, 4);
        Array.Copy(messageBytes, 0, envelope, 4, messageBytes.Length);
        
        return envelope;
    }
    
    /// <summary>
    /// Read length-prefixed message from stream
    /// </summary>
    public static async Task<byte[]> ReadFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        // Read 4-byte length prefix
        var lengthBytes = new byte[4];
        var totalRead = 0;
        
        while (totalRead < 4)
        {
            var bytesRead = await stream.ReadAsync(lengthBytes.AsMemory(totalRead, 4 - totalRead), cancellationToken);
            if (bytesRead == 0)
                throw new EndOfStreamException("Connection closed while reading message length");
            totalRead += bytesRead;
        }
        
        var messageLength = BitConverter.ToInt32(lengthBytes, 0);
        if (messageLength <= 0 || messageLength > MaxMessageSize)
            throw new InvalidDataException($"Invalid message length: {messageLength}");
        
        // Read message data
        var messageBytes = new byte[messageLength];
        totalRead = 0;
        
        while (totalRead < messageLength)
        {
            var bytesRead = await stream.ReadAsync(messageBytes.AsMemory(totalRead, messageLength - totalRead), cancellationToken);
            if (bytesRead == 0)
                throw new EndOfStreamException("Connection closed while reading message data");
            totalRead += bytesRead;
        }
        
        return messageBytes;
    }
}
