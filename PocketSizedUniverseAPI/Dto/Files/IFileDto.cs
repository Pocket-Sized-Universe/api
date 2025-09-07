namespace PocketSizedUniverse.API.Dto.Files;

public interface IFileDto
{
    string ForbiddenBy { get; set; }
    byte[] Hash { get; set; }
    bool IsForbidden { get; set; }
}