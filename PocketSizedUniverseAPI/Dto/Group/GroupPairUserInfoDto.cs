using PocketSizedUniverse.API.Data;
using PocketSizedUniverse.API.Data.Enum;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.Group;

[MessagePackObject(keyAsPropertyName: true)]
public record GroupPairUserInfoDto(GroupData Group, UserData User, GroupPairUserInfo GroupUserInfo) : GroupPairDto(Group, User);