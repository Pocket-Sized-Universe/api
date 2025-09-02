using MareSynchronos.API.Data;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.User;

[MessagePackObject(keyAsPropertyName: true)]
public record OnlineUserCharaDataDto(UserData User, CharacterData CharaData) : UserDto(User);