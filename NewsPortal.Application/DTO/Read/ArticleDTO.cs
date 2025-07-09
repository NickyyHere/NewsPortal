namespace NewsPortal.Application.DTO.Read
{
    public record ArticleDTO(Guid id, string title, string content, string author, string slug, string status, DateTime createdAt, Guid categoryId);
}
