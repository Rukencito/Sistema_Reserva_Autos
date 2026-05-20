using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class AutosNegocio : IAutosNegocio
    {
        private IConexion? iConexion;
        public List<Autos> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Autos!.ToList();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo una consulta en Autos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();

            return lista;
        }

        public Autos Guardar(Autos entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);

            iConexion.Autos!.Add(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se realizo un guardado en Autos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Guardado";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Autos Eliminar(Autos entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (ValidarPlaca(entidad.Placa!))
            {
                throw new Exception("El auto con la placa " + entidad.Placa + " no existe en el sistema");
            }

            iConexion.Autos!.Remove(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se elimino un registro en Autos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Eliminacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public Autos Modificar(Autos entidad)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            ValidarDatos(entidad);

            if (!ValidarPlaca(entidad.Placa!))
            {
                throw new Exception("El auto con la placa " + entidad.Placa + " no existe en el sistema");
            }

            iConexion.Autos!.Update(entidad!);
            iConexion.SaveChanges();

            var Auditorias = new Auditorias();
            Auditorias.Descripcion = "Se modifico un registro en Autos";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Modificacion";
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return entidad;
        }

        public bool ValidarPlaca(string placa)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            var auto = iConexion.Autos!.FirstOrDefault(a => a.Placa == placa);
            return auto != null;
        }

        public Autos ConsultarPorPlaca(string placa)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var auto = iConexion.Autos!.FirstOrDefault(a => a.Placa == placa);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta en Autos por placa: " + placa;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por Placa";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return auto!;
        }

        public List<Autos> ConsultarPorMarca(string marca)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var autos = iConexion.Autos!.Where(a => a.Marca == marca).ToList();
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta en Autos por marca: " + marca;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por Marca";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return autos;
        }

        public List<Autos> ConsultarPorModelo(string modelo)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            
            var autos = iConexion.Autos!.Where(a => a.Modelo == modelo).ToList();
            var Auditorias = new Auditorias();
            
            Auditorias.Descripcion = "Se realizo una consulta en Autos por modelo: " + modelo;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta por Modelo";
            
            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return autos;
        }

        public bool VerificarDisponibilidad(string placa)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var auto = ConsultarPorPlaca(placa);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se verifico la disponibilidad del auto con placa: " + placa;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Verificacion de Disponibilidad";
            this.iConexion.Auditorias!.Add(Auditorias);

            this.iConexion.Auditorias!.Add(Auditorias); 
            iConexion.SaveChanges();

            if (auto == null)
            {
                throw new Exception("El auto con la placa " + placa + " no existe.");
            }
            else
            {
                return auto.Estado;
            }
        }

        public List<Autos> ConsultarDisponibles()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var autosDisponibles = iConexion.Autos!.Where(a => a.Estado == true).ToList();
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se realizo una consulta de autos disponibles";
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Consulta de Autos Disponibles";

            this.iConexion.Auditorias!.Add(Auditorias);
            iConexion.SaveChanges();
            return autosDisponibles;
        }

        public bool CambiarEstado(string placa, bool nuevoEstado)
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var auto = ConsultarPorPlaca(placa);
            var Auditorias = new Auditorias();

            Auditorias.Descripcion = "Se cambio el estado del auto con placa: " + placa + " a " + nuevoEstado;
            Auditorias.FechaHora = DateTime.Now;
            Auditorias.Usuario = "UsuarioActual"; // Reemplaza con el usuario actual
            Auditorias.Accion = "Cambio de Estado";

            this.iConexion.Auditorias!.Add(Auditorias);
            if (auto == null)
            {
                throw new Exception("El auto con la placa " + placa + " no existe.");
            }
            else
            {
                auto.Estado = nuevoEstado;
                iConexion.Autos!.Update(auto);
                iConexion.SaveChanges();
                return auto.Estado;
            }
        }
        public void ValidarDatos(Autos entidad) 
        { 
            if (entidad == null)
            {
                throw new Exception("La información del auto es obligatoria");
            }

                if (string.IsNullOrEmpty(entidad.Placa))
                {
                    throw new Exception("La placa del auto es obligatoria");
                }
    
                if (string.IsNullOrEmpty(entidad.Marca))
                {
                    throw new Exception("La marca del auto es obligatoria");
                }
    
                if (entidad.Año >= 0)
                {
                    throw new Exception("El año del auto debe ser un número positivo");
                }
    
                if (string.IsNullOrEmpty(entidad.Modelo))
                {
                    throw new Exception("El modelo del auto es obligatorio");
                }
    
                if (string.IsNullOrEmpty(entidad.Color))
                {
                    throw new Exception("El color del auto es obligatorio");
                }
        }
    }
}
