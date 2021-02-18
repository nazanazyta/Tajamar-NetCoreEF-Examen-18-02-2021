using Examen18022021Naza.Data;
using Examen18022021Naza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen18022021Naza.Repositories
{
    public class RepositorySQL: IRepository
    {
        LibrosContext context;

        public RepositorySQL(LibrosContext context)
        {
            this.context = context;
        }

        public List<Genero> GetGeneros()
        {
            return this.context.Generos.ToList();
        }

        public List<Libro> GetLibrosGenero(int idgenero)
        {
            return this.context.Libros.Where(x => x.IdGenero == idgenero).ToList();
        }

        public List<Libro> GetLibros()
        {
            return this.context.Libros.ToList();
        }

        public Libro GetLibroById(int idlibro)
        {
            return this.context.Libros.Where(x => x.IdLibro == idlibro).SingleOrDefault();
        }

        public Usuario Validar(String email, String pass)
        {
            return (this.context.Usuarios.Where(x => x.Email == email && x.Pass == pass).FirstOrDefault());
        }

        public Usuario GetUsuario(int idusuario)
        {
            return this.context.Usuarios.Where(x => x.IdUsuario == idusuario).SingleOrDefault();
        }

        public int GetNumeroRegistrosLibros()
        {
            return this.context.Libros.Count();
        }

        public int GetNumeroRegistrosLibrosGenero(int idgenero)
        {
            return this.context.Libros.Where(x => x.IdGenero == idgenero).Count();
        }

        public List<Libro> GetGrupoLibros(int posicion)
        {
            int totallibros = this.GetNumeroRegistrosLibros();
            int registros = 3;
            int inicio = posicion * registros;

            return this.context.Libros.OrderBy(z => z.Titulo).Skip(inicio).Take(registros).ToList();
        }

        public List<Libro> GetGrupoLibrosGenero(int posicion, int idgenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            int totallibros = this.GetNumeroRegistrosLibrosGenero(idgenero);
            int registros = 3;
            int inicio = posicion * registros;
            return this.context.Libros.Where(x => x.IdGenero == idgenero).OrderBy(z => z.Titulo).Skip(inicio).Take(registros).ToList();
        }

        public List<Libro> GetCarritoLibros(List<int> idlibros)
        {
            var consulta = from datos in this.context.Libros
                           where idlibros.Contains(datos.IdLibro)
                           select datos;
            return consulta.ToList();
        }

        public int GetMaxIdPedido()
        {
            int id = (from datos in this.context.Pedidos
                            select datos).Count();
            if (id == 0)
            {
                return 1;
            }
            return (from datos in this.context.Pedidos
                    select datos.IdPedido).Max() + 1;
        }

        public int GetMaxIdFactura()
        {
            int id = (from datos in this.context.Pedidos
                      select datos).Count();
            if (id == 0)
            {
                return 1;
            }
            return (from datos in this.context.Pedidos
                    select datos.IdFactura).Max() + 1;
        }

        public void GuardarPedido(Pedido pedido)
        {
            this.context.Pedidos.Add(pedido);
            this.context.SaveChanges();
        }
    }
}
