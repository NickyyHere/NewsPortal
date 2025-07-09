namespace NewsPortal.Application.DTO.Update
{
    public record UpdateArticleDTO(
        string title,
        string content,
        string author,
        Guid categoryId
        );
}
