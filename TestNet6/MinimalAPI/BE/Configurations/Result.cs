using Newtonsoft.Json;

namespace BE
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string ReturnMessage { get; set; }
        public T Data { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
