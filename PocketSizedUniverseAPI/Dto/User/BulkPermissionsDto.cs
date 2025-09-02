using PocketSizedUniverse.API.Data.Enum;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.User;

[MessagePackObject(keyAsPropertyName: true)]
public record BulkPermissionsDto(Dictionary<string, UserPermissions> AffectedUsers, Dictionary<string, GroupUserPreferredPermissions> AffectedGroups);
