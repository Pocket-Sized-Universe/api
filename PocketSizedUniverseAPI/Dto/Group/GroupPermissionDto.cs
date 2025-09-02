using MareSynchronos.API.Data;
using PocketSizedUniverse.API.Data.Enum;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.Group;

[MessagePackObject(keyAsPropertyName: true)]
public record GroupPermissionDto(GroupData Group, GroupPermissions Permissions) : GroupDto(Group);