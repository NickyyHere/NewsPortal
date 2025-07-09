[Serializable]
public class UniqueConstraintViolationException : Exception
{
	public UniqueConstraintViolationException() { }
	public UniqueConstraintViolationException(Type type, string uniqueFieldName, string value) : base($"{type.Name} already has value {value} in {uniqueFieldName} column") { }
	public UniqueConstraintViolationException(string message) : base(message) { }
	public UniqueConstraintViolationException(string message, Exception inner) : base(message, inner) { }
	protected UniqueConstraintViolationException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}