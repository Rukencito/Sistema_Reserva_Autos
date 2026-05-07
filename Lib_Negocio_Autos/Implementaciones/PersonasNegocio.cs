using Lib_Negocio_Autos.Interfaces;
using Lib_Negocio_Autos.modelo;
using Lib_Negocio_Autos.nucleo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Negocio_Autos.Implementaciones
{
    public class PersonasNegocio: IPersonasNegocio
    {
        private IConexion? iConexion;
        public List<Personas> Consultar()
        {
            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var lista = iConexion.Persona!.ToList();

            var list = new Auditorias();
            list.Descripcion = "Se realizo una consulta en Personas";
            list.FechaHora = DateTime.Now;
            this.iConexion.Auditoria!.Add(list);
            iConexion.SaveChanges();

            return lista;
        }

        public Personas Guardar(Personas entidad)
        {

            iConexion = new Conexion();
            iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            iConexion.Persona!.Add(entidad!);
            iConexion.SaveChanges();

            var list = new Auditorias();
            list.Descripcion = "Se realizo un guardado en Personas";
            list.FechaHora = DateTime.Now;
            this.iConexion.Auditoria!.Add(list);
            iConexion.SaveChanges();
            return entidad;
        }
    }
}
