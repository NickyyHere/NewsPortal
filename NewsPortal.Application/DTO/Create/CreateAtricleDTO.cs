namespace NewsPortal.Application.DTO.Create
{
    public record CreateAtricleDTO(
        string title,
        string content,
        string author,
        Guid categoryId
        );
}
