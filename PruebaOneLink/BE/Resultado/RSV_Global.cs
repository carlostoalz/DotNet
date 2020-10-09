namespace BE.Resultado
{
    public class RSV_Global<T>
    {
        public T Datos { get; set; }
        public bool Exitoso { get; set; }
        public int CatidadPaginas { get; set; }
        public Error Error { get; set; }
        public int CodigoRespuesta { get; set; }
    }
}
