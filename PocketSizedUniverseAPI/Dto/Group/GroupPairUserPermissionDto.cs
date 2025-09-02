using PocketSizedUniverse.API.Data;
using PocketSizedUniverse.API.Data.Enum;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.Group;

[MessagePackObject(keyAsPropertyName: true)]
public record GroupPairUserPermissionDto(GroupData Group, UserData User, GroupUserPreferredPermissions GroupPairPermissions) : GroupPairDto(Group, User);