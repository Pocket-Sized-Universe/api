namespace PocketSizedUniverse.API.Dto.Files;

public interface IFileDto
{
    string ForbiddenBy { get; set; }
    string Hash { get; set; }
    bool IsForbidden { get; set; }
}