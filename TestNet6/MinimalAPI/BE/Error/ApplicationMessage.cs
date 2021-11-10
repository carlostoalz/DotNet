namespace BE
{
    public class ApplicationMessage
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public override string ToString() => string.Format("[{0}]- {1}", Code, Message);
    }
}
