using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.WebAPI.Api.Modelos
{
    //github api-guidelines
    public class ErrorResponse
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public ErrorResponse InnerError { get; set; }
        public string[] Detalhes { get; set; }

        public static ErrorResponse From(Exception exception)
        {
            return exception == null
                ? null
                : new ErrorResponse
                {
                    Codigo = exception.HResult,
                    Mensagem = exception.Message,
                    InnerError = ErrorResponse.From(exception.InnerException)
                };
        }

        public static ErrorResponse FromModelState(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(m => m.Errors);
            return new ErrorResponse
            {
                Codigo = 100,
                Mensagem = "Houve erro(s) no envio da requisicao",
                Detalhes = erros.Select(e => e.ErrorMessage).ToArray()
            };
        }
    }
}
