using Examen18022021Naza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Repositories
{
    public interface IRepository
    {
        List<Genero> GetGeneros();
        List<Libro> GetLibrosGenero(int idgenero);
        List<Libro> GetLibros();
        Libro GetLibroById(int idlibro);
        Usuario Validar(String email, String pass);
        Usuario GetUsuario(int idusuario);
        int GetNumeroRegistrosLibros();
        int GetNumeroRegistrosLibrosGenero(int idgenero);
        List<Libro> GetGrupoLibros(int posicion);
        List<Libro> GetGrupoLibrosGenero(int posicion, int idgenero);
        List<Libro> GetCarritoLibros(List<int> idlibros);
        int GetMaxIdPedido();
        int GetMaxIdFactura();
        void GuardarPedido(Pedido pedido);
    }
}
