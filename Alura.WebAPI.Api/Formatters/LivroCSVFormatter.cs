using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.Api.Formatters
{
    public class LivroCSVFormatter : TextOutputFormatter
    {
        public LivroCSVFormatter()
        {
            var textcsvMediaType = MediaTypeHeaderValue.Parse("text/csv");
            var appcsvMediaType = MediaTypeHeaderValue.Parse("application/csv");
            SupportedMediaTypes.Add(textcsvMediaType);
            SupportedMediaTypes.Add(appcsvMediaType);
            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LivroApi);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var lLivroEmVcs = "";

            if (context.Object is LivroApi)
            {
                var livro = context.Object as LivroApi;

                lLivroEmVcs = $"{livro.Titulo};{livro.Subtitulo};{livro.Autor};{livro.Lista}";
            }

            using (var escritor = context.WriterFactory(
            context.HttpContext.Request.Body, selectedEncoding))
            {
                return escritor.WriteAsync(lLivroEmVcs);
            }

        }
    }
}
