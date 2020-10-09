namespace BE.Resultado
{
    public class Error
    {
        public string Mensaje { get; set; }
        public string Pila { get; set; }

        public Error(string mensaje, string pila)
        {
            this.Mensaje = mensaje;
            this.Pila = pila;
        }
    }
}
