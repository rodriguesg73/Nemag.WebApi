using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Nemag.WebApi.Filters
{
    public class AsyncActionFilter : IAsyncActionFilter
    {
        #region Métodos Publicos

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requisicaoItem = await ProcessarRequisicaoItem(context);

            var actionExecutedContext = null as ActionExecutedContext;

            using (TransactionScope transactionScope = new(TransactionScopeOption.RequiresNew, TimeSpan.MaxValue, TransactionScopeAsyncFlowOption.Enabled))
            {
                actionExecutedContext = await next();

                var actionResult = actionExecutedContext.Result;

                var httpStatusCode = 0;

                if (actionResult.GetType() == typeof(ContentResult))
                {
                    httpStatusCode = ((ContentResult)actionResult).StatusCode.Value;
                }
                else if (actionResult.GetType() == typeof(FileStreamResult))
                {
                    httpStatusCode = 200;
                }

                if (httpStatusCode.Equals(200))
                    transactionScope.Complete();
            }

            await ProcessarRequisicaoResultadoItem(requisicaoItem, actionExecutedContext);
        }

        #endregion

        #region Métodos Privados

        private async Task<Core.Entidade.Requisicao.RequisicaoItem> ProcessarRequisicaoItem(ActionExecutingContext context)
        {
            var headerReferer = ObterRequestHeader(context, "Referer");

            var headerOrigin = ObterRequestHeader(context, "Origin");

            var requestPath = ObterRequestPath(context);

            var ip = ObterConnectionRemoteIpAddress(context);

            var requisicaoArgumentoLista = IdentificarRequisicaoArgumentoLista(context);

            var requisicaoItem = new Core.Entidade.Requisicao.RequisicaoItem()
            {
                UrlReferencia = headerReferer,
                UrlOrigem = headerOrigin,
                UrlDestino = requestPath,
                Ip = ip
            };

            requisicaoItem = await SalvarRequisicaoItem(requisicaoItem);

            await SalvarRequisicaoArgumentoLista(requisicaoItem, requisicaoArgumentoLista);

            return requisicaoItem;
        }

        private async Task<Core.Entidade.Requisicao.RequisicaoItem> SalvarRequisicaoItem(Core.Entidade.Requisicao.RequisicaoItem requisicaoItem)
        {
            return await Task.Run(() => new Core.Negocio.Requisicao.RequisicaoItem().SalvarItem(requisicaoItem));
        }

        private async Task<List<Core.Entidade.Requisicao.Argumento.ArgumentoItem>> SalvarRequisicaoArgumentoLista(Core.Entidade.Requisicao.RequisicaoItem requisicaoItem, List<Core.Entidade.Requisicao.Argumento.ArgumentoItem> requisicaoArgumentoLista)
        {
            var requisicaoArgumentoNegocio = new Core.Negocio.Requisicao.Argumento.ArgumentoItem();

            requisicaoArgumentoLista
                .ForEach(x =>
               {
                   x.RequisicaoId = requisicaoItem.Id;
               });

            for (int i = 0; i < requisicaoArgumentoLista.Count; i++)
                requisicaoArgumentoLista[i] = await Task.Run(() => requisicaoArgumentoNegocio.SalvarItem(requisicaoArgumentoLista[i]));

            return requisicaoArgumentoLista;
        }

        private async Task<Core.Entidade.Requisicao.Resultado.ResultadoItem> ProcessarRequisicaoResultadoItem(Core.Entidade.Requisicao.RequisicaoItem requisicaoItem, ActionExecutedContext actionExecutedContext)
        {
            var actionResult = actionExecutedContext.Result;

            if (actionResult == null)
                return null;

            var conteudo = string.Empty;

            var httpStatusCode = 0;

            var contentType = string.Empty;

            if (actionResult.GetType() == typeof(ContentResult))
            {
                httpStatusCode = ((ContentResult)actionResult).StatusCode.Value;

                contentType = ((ContentResult)actionResult).ContentType;

                conteudo = ((ContentResult)actionResult).Content;
            }
            else if (actionResult.GetType() == typeof(FileStreamResult))
            {
                contentType = ((FileStreamResult)actionResult).ContentType;

                conteudo = ((FileStreamResult)actionResult).FileDownloadName;
            }

            var requisicaoResultadoItem = new Core.Entidade.Requisicao.Resultado.ResultadoItem()
            {
                RequisicaoId = requisicaoItem.Id,

                Codigo = httpStatusCode,

                Tipo = contentType,

                Conteudo = conteudo
            };

            requisicaoResultadoItem = await SalvarRequisicaoResultadoItem(requisicaoResultadoItem);

            return requisicaoResultadoItem;
        }

        private async Task<Core.Entidade.Requisicao.Resultado.ResultadoItem> SalvarRequisicaoResultadoItem(Core.Entidade.Requisicao.Resultado.ResultadoItem requisicaoResultadoItem)
        {
            return await Task.Run(() => new Core.Negocio.Requisicao.Resultado.ResultadoItem().SalvarItem(requisicaoResultadoItem));
        }

        private string ObterRequestHeader(ActionExecutingContext context, string key)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(key))
                return context.HttpContext.Request.Headers[key].FirstOrDefault();

            return null;
        }

        private List<Core.Entidade.Requisicao.Argumento.ArgumentoItem> IdentificarRequisicaoArgumentoLista(ActionExecutingContext context)
        {
            return context.ActionArguments
                .ToList()
                .Select(x =>
                {
                    var requisicaoArgumentoItem = new Core.Entidade.Requisicao.Argumento.ArgumentoItem()
                    {
                        Nome = x.Key,
                        Valor = x.Value?.ToString()
                    };

                    return requisicaoArgumentoItem;
                })
                .ToList();
        }

        private string ObterRequestPath(ActionExecutingContext context)
        {
            return context.HttpContext.Request.Path;
        }

        private string ObterConnectionRemoteIpAddress(ActionExecutingContext context)
        {
            return context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        #endregion
    }
}
