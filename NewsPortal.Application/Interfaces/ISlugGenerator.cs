namespace NewsPortal.Application.Interfaces
{
    public interface ISlugGenerator
    {
        public Task<string> GenerateUniqueSlugAsync(string title);
    }
}
