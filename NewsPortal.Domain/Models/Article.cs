using NewsPortal.Domain.Enums;

namespace NewsPortal.Domain.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public ArticleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
