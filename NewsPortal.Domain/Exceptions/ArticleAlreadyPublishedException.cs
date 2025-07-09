[Serializable]
public class ArticleAlreadyPublishedException : Exception
{
	public ArticleAlreadyPublishedException() { }
    public ArticleAlreadyPublishedException(Guid id) : base($"Article with id {id} is already published") { }
    public ArticleAlreadyPublishedException(string message) : base(message) { }
	public ArticleAlreadyPublishedException(string message, Exception inner) : base(message, inner) { }
	protected ArticleAlreadyPublishedException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}