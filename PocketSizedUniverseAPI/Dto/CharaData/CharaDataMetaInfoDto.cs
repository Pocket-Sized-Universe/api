using PocketSizedUniverse.API.Data;
using MessagePack;

namespace PocketSizedUniverse.API.Dto.CharaData;

[MessagePackObject(keyAsPropertyName: true)]
public record CharaDataMetaInfoDto(string Id, UserData Uploader) : CharaDataDto(Id, Uploader)
{
    public List<PoseEntry> PoseData { get; set; } = [];
}
