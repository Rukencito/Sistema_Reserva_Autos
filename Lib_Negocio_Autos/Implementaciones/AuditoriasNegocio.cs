using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        public string UsuarioSesion { get; set; } = "";

        private IConexion? iConexion;

        private void AbrirConexion()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
        }

        public List<Auditorias> Consultar()
        {
            AbrirConexion();
            return iConexion!.Auditorias!.ToList();
        }
    }
}