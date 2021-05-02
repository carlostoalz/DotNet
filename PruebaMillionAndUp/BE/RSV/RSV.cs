namespace BE.RSV
{
    public class RSV<T>
    {
        public T Datos { get; set; }
        public bool Exitoso { get; set; }
        public int StatusCode { get; set; }
        public Error Error { get; set; }
    }
}
