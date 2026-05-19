using Lib_Negocio_Autos.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Presentacion_Autos.Interfaces
{
    public interface IReseñasPresentacion
    {
        List<Reseñas> Consultar();
        Reseñas Guardar(Reseñas entidad);
        Reseñas Modificar(Reseñas entidad);
        Reseñas Eliminar(Reseñas entidad);
    }
}
