using Alura.ListaLeitura.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.Api.Modelos
{
    public static class LivroFiltroExtensions
    {
        public static IQueryable<Livro> AplicaFiltro(this IQueryable<Livro> query, LivroFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrEmpty(filtro.Titulo))
                {
                    query = query.Where(x => x.Titulo.Contains(filtro.Titulo));
                }

                if (!string.IsNullOrEmpty(filtro.Subtitulo))
                {
                    query = query.Where(x => x.Subtitulo.Contains(filtro.Subtitulo));
                }

                if (!string.IsNullOrEmpty(filtro.Lista))
                {
                    query = query.Where(x => x.Lista== filtro.Lista.ParaTipo());
                }

                if (!string.IsNullOrEmpty(filtro.Autor))
                {
                    query = query.Where(x => x.Autor.Contains(filtro.Autor));
                }
            }

            return query;
        }
    }

    public class LivroFiltro
    {
        public string Autor { get; set; }

        public string Subtitulo { get; set; }

        public string Titulo { get; set; }

        public string Lista { get; set; }
    }
}
