using PocketSizedUniverse.API.Data;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.User;

[MessagePackObject(keyAsPropertyName: true)]
public record UserCharaDataMessageDto(List<UserData> Recipients, CharacterData CharaData, CensusDataDto? CensusDataDto);
