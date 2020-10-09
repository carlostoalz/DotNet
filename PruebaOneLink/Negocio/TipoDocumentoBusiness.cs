namespace Negocio
{
    using BE.Bases;
    using Datos.Context;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class TipoDocumentoBusiness : BaseBusiness<TipoDocumentoContext>
    {
        #region Constructor
        public TipoDocumentoBusiness(IConfiguration configuration) : base(configuration)
        {
            this.Context = new TipoDocumentoContext(configuration);
        }
        public TipoDocumentoBusiness(string connectionString) : base(connectionString)
        {
            this.Context = new TipoDocumentoContext(connectionString);
        }
        #endregion

        #region Métodos Públicos
        public List<TipoDocumento> ObtenerTiposDocumento()
        {
            return this.Context.usp_TiposDocumento_Obtener();
        }

        public TipoDocumento SeleccionarTipoDocumento(decimal pIdTipoDocumento)
        {
            return this.Context.usp_TiposDocumento_Seleccionar(pIdTipoDocumento);
        }

        public TipoDocumento ActualizarTipoDocumento(TipoDocumento pTipoDocumento)
        {
            return this.Context.usp_TiposDocumento_Actualizar(pTipoDocumento);
        }

        public bool EliminarTipoDocumento(decimal pIdTipoDocumento)
        {
            return this.Context.usp_TiposDocumento_Eliminar(pIdTipoDocumento);
        }
        #endregion
    }
}
