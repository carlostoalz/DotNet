namespace BE.Interfaces
{
    using BE.Resultado;
    using System.Collections.Generic;

    public interface IController<T>
    {
        public RSV_Global<List<T>> Obtener();
        public RSV_Global<T> Seleccionar(decimal id);
        public RSV_Global<T> Actualizar(T entity);
        public RSV_Global<bool> Eliminar(decimal id);
    }
}
