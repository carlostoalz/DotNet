using System.Runtime.Serialization;

namespace BE
{
    public class ModelBaseException : Exception
    {
        public ApplicationMessage ApplicationMessage { get; set; }

        public ModelBaseException() : base() { }
        public ModelBaseException(string message) : base(message) { }
        public ModelBaseException(string message, Exception innerException)
            : base(message, innerException) { }

        public ModelBaseException(ApplicationMessage message, Exception innerException = null) : base(message.Message, innerException) => this.ApplicationMessage = message;

        // The special constructor is used to deserialize values.
        public ModelBaseException(SerializationInfo info, StreamingContext context) => ApplicationMessage.Message = (string)info.GetValue("ApplicationMessage", typeof(string));

        // Implement this method to serialize data. The method is called 
        // on serialization.
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            base.GetObjectData(info, context);
            info.AddValue("ApplicationMessage", ApplicationMessage, typeof(string));
        }
    }
}
