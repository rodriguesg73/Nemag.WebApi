using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nemag.WebApi.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArquivoController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarArquivoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var arquivoLista = ObterArquivoLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    ArquivoLista = arquivoLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarArquivoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["arquivoId"] == null)
                    throw new ArgumentException("Código do arquivo necessário");

                var arquivoId = Convert.ToInt32(jsonObjeto["arquivoId"].ToString());

                var arquivoItem = ObterArquivoItem(arquivoId);

                var jsonRetorno = new 
                {
                    ArquivoItem = arquivoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarArquivoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoItem = ProcessarJsonParametro<Core.Entidade.Arquivo.ArquivoItem>(jsonObjeto["arquivoItem"], loginAcessoItem);

                arquivoItem = SalvarArquivoItem(arquivoItem);

                var jsonRetorno = new 
                {
                    ArquivoItem = arquivoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirArquivoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoItem = ProcessarJsonParametro<Core.Entidade.Arquivo.ArquivoItem>(jsonObjeto["arquivoItem"], loginAcessoItem);

                arquivoItem = ExcluirArquivoItem(arquivoItem);

                var jsonRetorno = new 
                {
                    ArquivoItem = arquivoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpGet]
        public IActionResult EfetuarDownload(int arquivoId, string token, string checksun)
        {
            using TransactionScope transactionScope = new(TransactionScopeOption.Suppress);

            try
            {
                var loginId = 1;

                var ip = ObterConnectionRemoteIpAddress();

                if (!string.IsNullOrEmpty(token))
                {
                    var loginAcessoItem = ValidarLoginAcessoItem(JsonConvert.SerializeObject(new { token }));

                    loginId = loginAcessoItem.LoginId;
                }

                var iActionResult = null as IActionResult;

                var arquivoItem = ObterArquivoItem(arquivoId);

                if (string.IsNullOrEmpty(arquivoItem.Checksun))
                {
                    arquivoItem.Checksun = new Core.Negocio.Arquivo.ArquivoItem().ValidarArquivoChecksun(arquivoItem);

                    SalvarArquivoItem(arquivoItem);
                }

                if (!arquivoItem.Checksun.Equals(checksun))
                {
                    iActionResult = ObterActionResult(HttpStatusCode.OK, arquivoItem);

                    InserirArquivoAcessoItem(new Core.Entidade.Arquivo.Acesso.AcessoItem
                    {
                        ArquivoId = arquivoId,
                        RegistroLoginId = loginId,
                        Ip = ip,
                        DataInclusao = DateTime.Now
                    });
                }
                else
                {
                    var jsonRetorno = new
                    {
                        arquivoId,

                        checksun
                    };

                    iActionResult = ObterActionResult(HttpStatusCode.NotModified, JsonConvert.SerializeObject(jsonRetorno));
                }

                return iActionResult;
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex);
            }
        }

        [HttpPost, HttpGet]
        public IActionResult LimparArquivoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var fileLista = new List<string>();

                var arquivoLista = ObterArquivoLista();

                var diretorioUtilizadoLista = arquivoLista
                    .Select(x => x.DiretorioLocalUrl)
                    .Distinct()
                    .Where(x => Directory.Exists(x))
                    .ToList();

                diretorioUtilizadoLista
                    .ForEach(x =>
                    {
                        fileLista.AddRange(Directory.GetFiles(x, "*", SearchOption.AllDirectories).ToList());
                    });

                arquivoLista = arquivoLista
                    .Select(x =>
                    {
                        if (diretorioUtilizadoLista.Contains(x.DiretorioLocalUrl) && !System.IO.File.Exists(Path.Combine(x.DiretorioLocalUrl, x.GuidPrimario)))
                            x = ExcluirArquivoItem(x);

                        return x;
                    })
                    .Where(x => x != null && !x.RegistroSituacaoId.Equals(3))
                    .ToList();

                fileLista
                    .ForEach(x =>
                    {
                        var diretorioUrl = Path.GetDirectoryName(x);

                        var arquivoGuid = Path.GetFileName(x);

                        var arquivoItem = arquivoLista
                            .FirstOrDefault(x => x.DiretorioLocalUrl.Equals(diretorioUrl) && x.GuidPrimario.Equals(arquivoGuid));

                        if (arquivoItem == null)
                            System.IO.File.Delete(x);
                        else
                        {
                            arquivoItem.Checksun = new Core.Negocio.Arquivo.ArquivoItem().ValidarArquivoChecksun(arquivoItem);

                            SalvarArquivoItem(arquivoItem);
                        }
                    });

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoLista = arquivoLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CompactarArquivoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoLista = ProcessarJsonParametro<List<Core.Entidade.Arquivo.ArquivoItem>>(jsonObjeto["arquivoLista"], loginAcessoItem);

                var arquivoItem = new Core.Negocio.Arquivo.ArquivoItem().CompactarArquivoLista(arquivoLista);

                arquivoItem.RegistroLoginId = loginAcessoItem.LoginId;

                arquivoItem = SalvarArquivoItem(arquivoItem);

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoItem = arquivoItem
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult DescompactarArquivoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoLista = ProcessarJsonParametro<List<Core.Entidade.Arquivo.ArquivoItem>>(jsonObjeto["arquivoLista"], loginAcessoItem);

                var arquivoDescompactadoLista = new Core.Negocio.Arquivo.ArquivoItem().DescompactarArquivoLista(arquivoLista);

                arquivoDescompactadoLista = arquivoDescompactadoLista
                    .Select(x =>
                    {
                        x.RegistroLoginId = loginAcessoItem.LoginId;

                        x = SalvarArquivoItem(x);

                        return x;
                    })
                    .ToList();

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoLista = arquivoDescompactadoLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarArquivoTramitadorLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var arquivoTramitadorLista = ObterArquivoTramitadorLista();

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoTramitadorLista = arquivoTramitadorLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarArquivoTramitadorListaPorFiltroItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoTramitadorFiltroItem = ProcessarJsonParametro<Core.Filtro.Arquivo.Tramitador.TramitadorItem>(jsonObjeto["arquivoTramitadorFiltroItem"]);

                var arquivoTramitadorLista = ObterArquivoTramitadorListaPorFiltroItem(arquivoTramitadorFiltroItem);

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoTramitadorLista = arquivoTramitadorLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarArquivoTramitadorItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["arquivoTramitadorId"] == null)
                    throw new ArgumentException("Código do arquivoTramitador necessário");

                var arquivoTramitadorId = Convert.ToInt32(jsonObjeto["arquivoTramitadorId"].ToString());

                var arquivoTramitadorItem = ObterArquivoTramitadorItem(arquivoTramitadorId);

                var arquivoTramitadorEmailLista = ObterArquivoTramitadorEmailListaPorArquivoTramitadorId(arquivoTramitadorId);

                var arquivoTramitadorFtpLista = ObterArquivoTramitadorFtpListaPorArquivoTramitadorId(arquivoTramitadorId);

                var arquivoTramitadorDiretorioLista = ObterArquivoTramitadorDiretorioListaPorArquivoTramitadorId(arquivoTramitadorId);

                var jsonRetorno = new
                {
                    ArquivoTramitadorItem = arquivoTramitadorItem,
                    ArquivoTramitadorEmailLista = arquivoTramitadorEmailLista,
                    ArquivoTramitadorFtpLista = arquivoTramitadorFtpLista,
                    ArquivoTramitadorDiretorioLista = arquivoTramitadorDiretorioLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarArquivoTramitadorItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoTramitadorItem = ProcessarJsonParametro<Core.Entidade.Arquivo.Tramitador.TramitadorItem>(jsonObjeto["arquivoTramitadorItem"], loginAcessoItem);

                var arquivoTramitadorEmailLista = ProcessarJsonParametro<List<Core.Entidade.Arquivo.Tramitador.Email.EmailItem>>(jsonObjeto["arquivoTramitadorEmailLista"], loginAcessoItem);

                var arquivoTramitadorFtpLista = ProcessarJsonParametro<List<Core.Entidade.Arquivo.Tramitador.Ftp.FtpItem>>(jsonObjeto["arquivoTramitadorFtpLista"], loginAcessoItem);

                var arquivoTramitadorDiretorioLista = ProcessarJsonParametro<List<Core.Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem>>(jsonObjeto["arquivoTramitadorDiretorioLista"], loginAcessoItem);

                arquivoTramitadorItem = SalvarArquivoTramitadorItem(arquivoTramitadorItem);

                arquivoTramitadorEmailLista = SalvarArquivoTramitadorEmailLista(arquivoTramitadorItem, arquivoTramitadorEmailLista);

                arquivoTramitadorFtpLista = SalvarArquivoTramitadorFtpLista(arquivoTramitadorItem, arquivoTramitadorFtpLista);

                arquivoTramitadorDiretorioLista = SalvarArquivoTramitadorDiretorioLista(arquivoTramitadorItem, arquivoTramitadorDiretorioLista);

                var jsonRetorno = new
                {
                    ArquivoTramitadorItem = arquivoTramitadorItem,
                    ArquivoTramitadorEmailLista = arquivoTramitadorEmailLista,
                    ArquivoTramitadorFtpLista = arquivoTramitadorFtpLista,
                    ArquivoTramitadorDiretorioLista = arquivoTramitadorDiretorioLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirArquivoTramitadorItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoTramitadorItem = ProcessarJsonParametro<Core.Entidade.Arquivo.Tramitador.TramitadorItem>(jsonObjeto["arquivoTramitadorItem"], loginAcessoItem);

                arquivoTramitadorItem = ExcluirArquivoTramitadorItem(arquivoTramitadorItem);

                var jsonRetorno = new
                {
                    ArquivoTramitadorItem = arquivoTramitadorItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarArquivoTramitadorAcaoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var arquivoTramitadorAcaoLista = ObterArquivoTramitadorAcaoLista();

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    ArquivoTramitadorAcaoLista = arquivoTramitadorAcaoLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExecutarArquivoTramitadorItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var arquivoTramitadorId = Convert.ToInt32(jsonObjeto["arquivoTramitadorId"].ToString());



                var jsonRetorno = new
                {
                    
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        #endregion

        #region Rotina de Upload

        [HttpPost]
        [RequestSizeLimit(2147483648)]
        public async Task<IActionResult> EfetuarArquivoUpload([FromForm] Model.Input.Arquivo.ArquivoUploadModel parametroItem)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(JsonConvert.SerializeObject(parametroItem));

                var arquivoLista = new List<Core.Entidade.Arquivo.ArquivoItem>();

                var diretorioLocalUrl = Path.Combine(Auxiliar.Util.ObterDiretorioAtualUrl(), ObterConfiguracaoValor("Arquivo:Diretorio:Local:Url"));

                Auxiliar.Util.ValidarDiretorio(diretorioLocalUrl);

                var formFileLista = parametroItem.FormFileLista;

                foreach (IFormFile formFileItem in formFileLista)
                {
                    var arquivoGuid = Guid.NewGuid().ToString();

                    var arquivoUrl = Path.Combine(diretorioLocalUrl, arquivoGuid);

                    using (FileStream output = System.IO.File.Create(arquivoUrl))
                        await formFileItem.CopyToAsync(output);

                    var arquivoNome = ContentDispositionHeaderValue.Parse(formFileItem.ContentDisposition).FileName.Trim('"');

                    var arquivoItem = new Core.Negocio.Arquivo.ArquivoItem().PrepararArquivoItem(arquivoNome, arquivoUrl);

                    arquivoItem.RegistroLoginId = loginAcessoItem.LoginId;

                    arquivoItem = SalvarArquivoItem(arquivoItem);

                    arquivoLista.Add(arquivoItem);
                }

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    arquivoLista,
                    diretorioLocalUrl
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private List<Core.Entidade.Arquivo.ArquivoItem> ObterArquivoLista()
        {
            var arquivoNegocio = new Core.Negocio.Arquivo.ArquivoItem();

            var arquivoLista = arquivoNegocio.CarregarLista();

            return arquivoLista;
        }

        private Core.Entidade.Arquivo.ArquivoItem ObterArquivoItem(int arquivoId)
        {
            var arquivoNegocio = new Core.Negocio.Arquivo.ArquivoItem();

            var arquivoItem = arquivoNegocio.CarregarItem(arquivoId);

            return arquivoItem;
        }

        private Core.Entidade.Arquivo.ArquivoItem SalvarArquivoItem(Core.Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var arquivoNegocio = new Core.Negocio.Arquivo.ArquivoItem();

            arquivoItem = arquivoNegocio.SalvarItem(arquivoItem);

            return arquivoItem;
        }

        private Core.Entidade.Arquivo.ArquivoItem ExcluirArquivoItem(Core.Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var arquivoNegocio = new Core.Negocio.Arquivo.ArquivoItem();

            arquivoItem = arquivoNegocio.ExcluirItem(arquivoItem);

            return arquivoItem;
        }

        private Core.Entidade.Arquivo.Acesso.AcessoItem InserirArquivoAcessoItem(Core.Entidade.Arquivo.Acesso.AcessoItem arquivoAcessoItem)
        {
            var arquivoAcessoNegocio = new Core.Negocio.Arquivo.Acesso.AcessoItem();

            arquivoAcessoItem = arquivoAcessoNegocio.InserirItem(arquivoAcessoItem);

            return arquivoAcessoItem;
        }

        private List<Core.Entidade.Arquivo.Tramitador.TramitadorItem> ObterArquivoTramitadorLista()
        {
            var arquivoTramitadorNegocio = new Core.Negocio.Arquivo.Tramitador.TramitadorItem();

            var arquivoTramitadorLista = arquivoTramitadorNegocio.CarregarLista();

            return arquivoTramitadorLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Acao.AcaoItem> ObterArquivoTramitadorAcaoLista()
        {
            var arquivoTramitadorAcaoNegocio = new Core.Negocio.Arquivo.Tramitador.Acao.AcaoItem();

            var arquivoTramitadorAcaoLista = arquivoTramitadorAcaoNegocio.CarregarLista();

            return arquivoTramitadorAcaoLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.TramitadorItem> ObterArquivoTramitadorListaPorFiltroItem(Core.Filtro.Arquivo.Tramitador.TramitadorItem arquivoTramitadorFiltroItem)
        {
            var arquivoTramitadorNegocio = new Core.Negocio.Arquivo.Tramitador.TramitadorItem();

            var arquivoTramitadorLista = arquivoTramitadorNegocio.CarregarListaPorFiltroItem<Core.Entidade.Arquivo.Tramitador.TramitadorItem, Core.Filtro.Arquivo.Tramitador.TramitadorItem>(arquivoTramitadorFiltroItem);

            return arquivoTramitadorLista;
        }

        private Core.Entidade.Arquivo.Tramitador.TramitadorItem ObterArquivoTramitadorItem(int arquivoTramitadorId)
        {
            var arquivoTramitadorNegocio = new Core.Negocio.Arquivo.Tramitador.TramitadorItem();

            var arquivoTramitadorItem = arquivoTramitadorNegocio.CarregarItem(arquivoTramitadorId);

            return arquivoTramitadorItem;
        }

        private Core.Entidade.Arquivo.Tramitador.TramitadorItem SalvarArquivoTramitadorItem(Core.Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem)
        {
            var arquivoTramitadorNegocio = new Core.Negocio.Arquivo.Tramitador.TramitadorItem();

            arquivoTramitadorItem = arquivoTramitadorNegocio.SalvarItem(arquivoTramitadorItem);

            return arquivoTramitadorItem;
        }

        private Core.Entidade.Arquivo.Tramitador.TramitadorItem ExcluirArquivoTramitadorItem(Core.Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem)
        {
            var arquivoTramitadorNegocio = new Core.Negocio.Arquivo.Tramitador.TramitadorItem();

            arquivoTramitadorItem = arquivoTramitadorNegocio.ExcluirItem(arquivoTramitadorItem);

            return arquivoTramitadorItem;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Email.EmailItem> ObterArquivoTramitadorEmailListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorEmailNegocio = new Core.Negocio.Arquivo.Tramitador.Email.EmailItem();

            var arquivoTramitadorEmailLista = arquivoTramitadorEmailNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorEmailLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Email.EmailItem> SalvarArquivoTramitadorEmailLista(Core.Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem, List<Core.Entidade.Arquivo.Tramitador.Email.EmailItem> arquivoTramitadorEmailNovoLista)
        {
            if (arquivoTramitadorEmailNovoLista == null)
                return null;

            var arquivoTramitadorEmailNegocio = new Core.Negocio.Arquivo.Tramitador.Email.EmailItem();

            var arquivoTramitadorEmailAtualLista = arquivoTramitadorEmailNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            arquivoTramitadorEmailAtualLista
                .Where(x => !arquivoTramitadorEmailNovoLista.Select(y => y.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => arquivoTramitadorEmailNegocio.ExcluirItem(x));

            arquivoTramitadorEmailNovoLista = arquivoTramitadorEmailNovoLista
                .Where(x => x.Id.Equals(0) || arquivoTramitadorEmailAtualLista.Select(y => y.Id).Contains(x.Id))
                .Select(x =>
                {
                    x.ArquivoTramitadorId = arquivoTramitadorItem.Id;

                    x = arquivoTramitadorEmailNegocio.SalvarItem(x);

                    return x;
                })
                .ToList();

            return arquivoTramitadorEmailNovoLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Ftp.FtpItem> ObterArquivoTramitadorFtpListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorFtpNegocio = new Core.Negocio.Arquivo.Tramitador.Ftp.FtpItem();

            var arquivoTramitadorFtpLista = arquivoTramitadorFtpNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorFtpLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Ftp.FtpItem> SalvarArquivoTramitadorFtpLista(Core.Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem, List<Core.Entidade.Arquivo.Tramitador.Ftp.FtpItem> arquivoTramitadorFtpNovoLista)
        {
            if (arquivoTramitadorFtpNovoLista == null)
                return null;

            var arquivoTramitadorFtpNegocio = new Core.Negocio.Arquivo.Tramitador.Ftp.FtpItem();

            var arquivoTramitadorFtpAtualLista = arquivoTramitadorFtpNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            arquivoTramitadorFtpAtualLista
                .Where(x => !arquivoTramitadorFtpNovoLista.Select(y => y.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => arquivoTramitadorFtpNegocio.ExcluirItem(x));

            arquivoTramitadorFtpNovoLista = arquivoTramitadorFtpNovoLista
                .Where(x => x.Id.Equals(0) || arquivoTramitadorFtpAtualLista.Select(y => y.Id).Contains(x.Id))
                .Select(x =>
                {
                    x.ArquivoTramitadorId = arquivoTramitadorItem.Id;

                    x = arquivoTramitadorFtpNegocio.SalvarItem(x);

                    return x;
                })
                .ToList();

            return arquivoTramitadorFtpNovoLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> ObterArquivoTramitadorDiretorioListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorDiretorioNegocio = new Core.Negocio.Arquivo.Tramitador.Diretorio.DiretorioItem();

            var arquivoTramitadorDiretorioLista = arquivoTramitadorDiretorioNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorDiretorioLista;
        }

        private List<Core.Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> SalvarArquivoTramitadorDiretorioLista(Core.Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem, List<Core.Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> arquivoTramitadorDiretorioNovoLista)
        {
            if (arquivoTramitadorDiretorioNovoLista == null)
                return null;

            var arquivoTramitadorDiretorioNegocio = new Core.Negocio.Arquivo.Tramitador.Diretorio.DiretorioItem();

            var arquivoTramitadorDiretorioAtualLista = arquivoTramitadorDiretorioNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            arquivoTramitadorDiretorioAtualLista
                .Where(x => !arquivoTramitadorDiretorioNovoLista.Select(y => y.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => arquivoTramitadorDiretorioNegocio.ExcluirItem(x));

            arquivoTramitadorDiretorioNovoLista = arquivoTramitadorDiretorioNovoLista
                .Where(x => x.Id.Equals(0) || arquivoTramitadorDiretorioAtualLista.Select(y => y.Id).Contains(x.Id))
                .Select(x =>
                {
                    x.ArquivoTramitadorId = arquivoTramitadorItem.Id;

                    x = arquivoTramitadorDiretorioNegocio.SalvarItem(x);

                    return x;
                })
                .ToList();

            return arquivoTramitadorDiretorioNovoLista;
        }

        #endregion


    }
}
