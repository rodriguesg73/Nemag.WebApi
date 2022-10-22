using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Nemag.WebApi.Controllers
{
    [ApiController]
    public class _BaseController : ControllerBase
    {
        #region Propriedades Auxiliares

        protected virtual IConfiguration Configuration
        {
            get
            {
                return Startup.Configuration;
            }
        }

        protected virtual string CallingAssemblyName
        {
            get
            {
                return new StackTrace()
                    .GetFrames()
                    .Select(f => f.GetMethod()?.ReflectedType?.Assembly.GetName().Name)
                    .Distinct()
                    .Where(x => !string.IsNullOrEmpty(x) && !x.Equals(Assembly.GetExecutingAssembly().GetName().Name))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region Métodos Auxiliares das Controllers

        protected virtual IActionResult ObterActionResult(HttpStatusCode httpStatusCode, string jsonString)
        {
            var contentResult = new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                StatusCode = (int?)httpStatusCode
            };

            return contentResult;
        }

        protected virtual IActionResult ObterActionResult(HttpStatusCode httpStatusCode, Core.Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var fileStream = System.IO.File.Open(Path.Combine(arquivoItem.DiretorioLocalUrl, arquivoItem.Guid), FileMode.Open, FileAccess.Read, FileShare.Read);

            return ObterActionResult(httpStatusCode, arquivoItem.Nome, fileStream);
        }

        protected virtual IActionResult ObterActionResult(HttpStatusCode httpStatusCode, string arquivoNome, Stream streamContent)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(arquivoNome, out string mimeType);

            mimeType ??= "application/octet-stream";

            return new FileStreamResult(streamContent, new MediaTypeHeaderValue(mimeType))
            {
                FileDownloadName = arquivoNome
            };
        }

        protected virtual IActionResult ObterActionResult(Exception ex)
        {
            return ObterActionResult(ex, string.Empty);
        }

        protected virtual IActionResult ObterActionResult(Exception ex, string parametroConteudo)
        {
            var stackTrace = new StackTrace(ex, true);

            var frame = stackTrace.GetFrame(0);

            var lineNumber = frame?.GetFileLineNumber();

            var method = frame?.GetMethod();

            var fileName = frame?.GetFileName();

            var loginItem = null as Core.Entidade.Login.LoginItem;

            var httpStatusCode = HttpStatusCode.BadRequest;

            if (ex.GetType() == typeof(UnauthorizedAccessException))
            {
                if (!string.IsNullOrEmpty(parametroConteudo))
                {
                    var jsonObject = ProcessarJsonParametro<JObject>(parametroConteudo);

                    if (!string.IsNullOrEmpty(jsonObject["token"]?.ToString()))
                    {
                        var loginAcessoItem = ObterLoginAcessoItemPorToken(jsonObject["token"]?.ToString());

                        if (loginAcessoItem != null)
                            loginItem = ObterLoginItem(loginAcessoItem.LoginId);
                    }
                }

                httpStatusCode = HttpStatusCode.Unauthorized;
            }
            else if (ex.GetType() == typeof(ApplicationException))
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
            }
            else if (ex.GetType() == typeof(MethodAccessException))
            {
                httpStatusCode = HttpStatusCode.MethodNotAllowed;
            }

            var jsonRetorno = new
            {
                ex.Message,
                ex.StackTrace,
                LineNumber = lineNumber,
                ex.Source,
                TargetSite = ex.TargetSite?.ReflectedType.Name + "." + ex.TargetSite?.Name + "()",
                MethodName = method?.Name,
                FileName = fileName,
                LoginItem = loginItem
            };

            return ObterActionResult(httpStatusCode, JsonConvert.SerializeObject(jsonRetorno));
        }

        protected virtual T ProcessarJsonParametro<T>(JToken jToken) where T : new()
        {
            if (jToken == null)
                return default;

            return ProcessarJsonParametro<T>(jToken.ToString(), new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        protected virtual T ProcessarJsonParametro<T>(string parametroConteudo) where T : new()
        {
            if (string.IsNullOrEmpty(parametroConteudo))
                return default;

            return ProcessarJsonParametro<T>(parametroConteudo, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        protected virtual T ProcessarJsonParametro<T>(string parametroConteudo, IsoDateTimeConverter isoDateTimeConverter) where T : new()
        {
            if (string.IsNullOrEmpty(parametroConteudo))
                return default;

            return JsonConvert.DeserializeObject<T>(parametroConteudo, new MinDateTimeConverter(), isoDateTimeConverter);
        }

        protected virtual T ProcessarJsonParametro<T>(JToken jToken, Core.Entidade.Login.Acesso.AcessoItem loginAcessoItem) where T : new()
        {
            if (jToken == null)
                return default;

            return ProcessarJsonParametro<T>(jToken.ToString(), loginAcessoItem);
        }

        protected virtual T ProcessarJsonParametro<T>(string parametroConteudo, Core.Entidade.Login.Acesso.AcessoItem loginAcessoItem) where T : new()
        {
            if (string.IsNullOrEmpty(parametroConteudo))
                return default;

            var objetoJson = JsonConvert.DeserializeObject<T>(parametroConteudo, new MinDateTimeConverter());

            if (loginAcessoItem != null)
            {
                var tratarObjetoJsonComoLista = objetoJson is IList && objetoJson.GetType().IsGenericType && objetoJson.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));

                if (tratarObjetoJsonComoLista)
                {
                    var typeItem = objetoJson.GetType();

                    var genericArgument = typeItem.GetGenericArguments()[0];

                    var genericType = typeof(List<>).MakeGenericType(genericArgument);

                    var objetoLista = (IList)Convert.ChangeType(objetoJson, genericType);

                    for (int i = 0; i < objetoLista.Count; i++)
                    {
                        var objetoItem = objetoLista[i];

                        var propriedadeItem = objetoItem.GetType().GetProperty("RegistroLoginId");

                        propriedadeItem?.SetValue(objetoItem, loginAcessoItem.LoginId);
                    }
                }
                else
                {
                    var propriedadeItem = objetoJson.GetType().GetProperty("RegistroLoginId");

                    propriedadeItem?.SetValue(objetoJson, loginAcessoItem.LoginId);
                }
            }

            return objetoJson;
        }

        protected virtual Dictionary<string, string> ObterTituloDicionarioItem<T>()
        {
            var dicionario = new Dictionary<string, string>();

            var type = typeof(T);

            type.GetProperties()
                .ToList()
                .ForEach(x =>
                {
                    var nomeExibicao = string.Empty;

                    var nomeOriginal = x.Name;

                    nomeOriginal
                        .ToList()
                        .ForEach(y =>
                        {
                            if (Char.IsUpper(y))
                                nomeExibicao += " " + y;
                            else
                                nomeExibicao += y;
                        });

                    var nomeSplit = nomeExibicao
                        .Trim()
                        .Split(' ')
                        .Reverse()
                        .ToList();

                    if (nomeSplit.Count >= 3)
                        nomeSplit.RemoveRange(3, nomeSplit.Count - 3);

                    for (int i = 1; i < nomeSplit.Count; i++)
                    {
                        if (nomeSplit[i].Length <= 2)
                            continue;

                        var palavraSubstring = nomeSplit[i][^2..];

                        if (palavraSubstring.Equals("ao"))
                        {
                            nomeSplit[i] = "de " + nomeSplit[i];
                        }
                        else if (new List<string>() { "e", "i", "s", "l", "n" }.Contains(palavraSubstring[1].ToString()))
                        {
                            nomeSplit[i] = "do " + nomeSplit[i];
                        }
                        else
                        {
                            nomeSplit[i] = "d" + palavraSubstring[1] + " " + nomeSplit[i];
                        }
                    }

                    if (new List<string>() { "Nome", "Titulo", "Codigo" }.Contains(nomeOriginal.Trim()))
                        nomeExibicao = nomeOriginal;
                    else
                        nomeExibicao = String.Join(' ', nomeSplit).Trim();

                    dicionario.Add(nomeOriginal, nomeExibicao);
                });

            dicionario = dicionario
                .Select(X =>
                {
                    var indice = 1;

                    if (!new List<string> { "Id", "Nome" }.Contains(X.Key))
                        indice = 2;

                    return new { Item = X, Indice = indice };
                })
                .OrderBy(x => x.Indice)
                .Select(x => x.Item)
                .ToDictionary(x => x.Key, y => y.Value);

            return dicionario;
        }

        protected virtual Core.Entidade.Login.Acesso.AcessoItem ValidarLoginAcessoItem(string parametroConteudo)
        {
            if (string.IsNullOrEmpty(parametroConteudo))
                throw new ApplicationException("Parâmetros necessários");

            var jsonObjeto = JsonConvert.DeserializeObject(parametroConteudo.ToLower());

            if (jsonObjeto == null)
                throw new ApplicationException("Parâmetros necessários");

            var token = ((JObject)jsonObjeto)["token"]?.ToString();

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token Necessário");

            var loginAcessoItem = ObterLoginAcessoItemPorToken(token);

            if (loginAcessoItem == null)
                throw new UnauthorizedAccessException("Token de acesso não encontrado");

            if (loginAcessoItem.DataValidade <= DateTime.Now)
                throw new UnauthorizedAccessException("Acesso do token expirado");

            loginAcessoItem.Ip = ObterConnectionRemoteIpAddress();

            ValidarRequisicaoPermissao(Request.Path.Value, loginAcessoItem);

            _ = Request.Path.Value;

            return loginAcessoItem;
        }

        private void ValidarRequisicaoPermissao(string requestPathValue, Core.Entidade.Login.Acesso.AcessoItem loginAcessoItem)
        {
            var requisicaoPermissaoAtribuicaoLista = new Core.Negocio.Requisicao.Permissao.Atribuicao.AtribuicaoItem().CarregarListaPorUrlDestino(requestPathValue);

            if (requisicaoPermissaoAtribuicaoLista.Count.Equals(0))
                return;

            var loginAtribuicaoLista = new Core.Negocio.Login.Atribuicao.AtribuicaoItem().CarregarListaPorLoginId(loginAcessoItem.LoginId);

            requisicaoPermissaoAtribuicaoLista = requisicaoPermissaoAtribuicaoLista
                .Where(x => loginAtribuicaoLista.Select(t => t.LoginGrupoId).Contains(x.LoginGrupoId) && loginAtribuicaoLista.Select(t => t.LoginPerfilId).Contains(x.LoginPerfilId))
                .ToList();

            if (requisicaoPermissaoAtribuicaoLista.Count.Equals(0))
                throw new MethodAccessException("Não Permitido");
        }

        #endregion

        #region Métodos Auxiliares de Sistema

        protected virtual string ObterConnectionRemoteIpAddress()
        {
            var remoteIpAddress = Request.Headers["X-Forwarded-For"].ToString().Trim();

            if (string.IsNullOrEmpty(remoteIpAddress))
                remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            return remoteIpAddress;
        }

        protected string ObterConfiguracaoValor(string key)
        {
            var keySplit = key
                .Split(':')
                .ToList();

            for (int i = 0; i < keySplit.Count; i++)
            {
                var keyLista = keySplit.ToList();

                keyLista.Insert(i, CallingAssemblyName);

                var keyJoin = String.Join(':', keyLista.ToArray());

                var value = Configuration[keyJoin]?.ToString();

                if (!string.IsNullOrEmpty(value))
                    return value;
            }

            return Configuration[key]?.ToString();
        }

        #endregion

        #region Métodos Privados 

        private Core.Entidade.Login.Acesso.AcessoItem ObterLoginAcessoItemPorToken(string token)
        {
            return new Core.Negocio.Login.Acesso.AcessoItem().CarregarItemPorToken(token);
        }

        private Core.Entidade.Login.LoginItem ObterLoginItem(int loginId)
        {
            return new Core.Negocio.Login.LoginItem().CarregarItem(loginId);
        }

        #endregion

        #region Classe para conversão de valores de DATA/HORA vazios

        private class MinDateTimeConverter : DateTimeConverterBase
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var valor = reader.Value.ToString();

                if (string.IsNullOrEmpty(valor))
                    return DateTime.MinValue;

                return DateTime.Parse(valor);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DateTime dateTimeValue = (DateTime)value;

                if (dateTimeValue == DateTime.MinValue)
                {
                    writer.WriteNull();

                    return;
                }

                writer.WriteValue(value);
            }
        }

        #endregion
    }
}
