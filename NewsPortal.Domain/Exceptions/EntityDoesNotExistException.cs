[Serializable]
public class EntityDoesNotExistException : Exception
{
	public EntityDoesNotExistException() { }
    public EntityDoesNotExistException(Type type) : base($"{type.Name} does not exist") { }
    public EntityDoesNotExistException(Type type, Guid id) : base($"{type.Name} with id {id} does not exist") { }
    public EntityDoesNotExistException(string message) : base(message) { }
	public EntityDoesNotExistException(string message, Exception inner) : base(message, inner) { }
	protected EntityDoesNotExistException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}