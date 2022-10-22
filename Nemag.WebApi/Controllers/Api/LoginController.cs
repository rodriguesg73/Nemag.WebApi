using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nemag.WebApi.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : _BaseController
    {
        #region Requests
        [HttpPost]
        public IActionResult EfetuarLogin([FromForm] string parametroConteudo)
        {
            try
            {
                if (string.IsNullOrEmpty(parametroConteudo))
                    return base.ObterActionResult(new Exception("Argumento primário necessário"));

                var jsonObjeto = JsonConvert.DeserializeObject(parametroConteudo);

                var loginUsuario = ((JObject)jsonObjeto)["usuario"].ToString().ToLower();

                var loginSenha = ((JObject)jsonObjeto)["senha"].ToString();

                var loginItem = ObterLoginItemPorUsuario(loginUsuario);

                if (loginItem == null)
                    throw new ApplicationException("Usuário não encontrado!");

                var loginSenhaMd5 = Auxiliar.Util.GerarHashMd5(loginSenha);

                var usuarioValido = ValidarUsuarioNoActiveDirectory("unishipping.corp", loginUsuario, loginSenha);

                if (!loginItem.Senha.Equals(loginSenhaMd5) && !usuarioValido)
                    throw new ApplicationException("Senha inválida!");

                var requestIp = base.ObterConnectionRemoteIpAddress();

                var loginAcessoItem = CarregarLoginAcessoItem(loginItem.Id, requestIp);

                if (loginAcessoItem != null)
                {
                    loginAcessoItem.DataValidade = DateTime.Now.AddMinutes(-10);

                    SalvarLoginAcessoItem(loginAcessoItem);

                    loginAcessoItem = null;
                }

                loginAcessoItem = new Core.Entidade.Login.Acesso.AcessoItem
                {
                    RegistroLoginId = loginItem.Id,

                    LoginId = loginItem.Id,

                    Token = GerarLoginAcessoToken(),

                    Ip = requestIp,

                    DataInclusao = DateTime.Now,

                    DataValidade = DateTime.Now.AddHours(10)
                };

                SalvarLoginAcessoItem(loginAcessoItem);

                loginItem.Senha = string.Empty;

                var retorno = new
                {
                    LoginItem = loginItem,
                    LoginAcessoItem = loginAcessoItem
                };

                var jsonRetorno = JsonConvert.SerializeObject(retorno);

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost, HttpGet]
        public IActionResult EfetuarLoginPorUsuario(string usuario, string senha)
        {
            try
            {
                var loginUsuario = usuario;

                var loginSenha = senha;

                var loginItem = ObterLoginItemPorUsuario(loginUsuario);

                if (loginItem == null)
                    throw new ApplicationException("Usuário não encontrado!");

                var loginSenhaMd5 = Auxiliar.Util.GerarHashMd5(loginSenha);

                var usuarioValido = ValidarUsuarioNoActiveDirectory("unishipping.corp", loginUsuario, loginSenha);

                if (!loginItem.Senha.Equals(loginSenhaMd5) && !usuarioValido)
                    throw new ApplicationException("Senha inválida!");

                var requestIp = base.ObterConnectionRemoteIpAddress();

                var loginAcessoItem = CarregarLoginAcessoItem(loginItem.Id, requestIp);

                if (loginAcessoItem != null)
                {
                    loginAcessoItem.DataValidade = DateTime.Now.AddMinutes(-10);

                    SalvarLoginAcessoItem(loginAcessoItem);

                    loginAcessoItem = null;
                }

                loginAcessoItem = new Core.Entidade.Login.Acesso.AcessoItem
                {
                    RegistroLoginId = loginItem.Id,

                    LoginId = loginItem.Id,

                    Token = GerarLoginAcessoToken(),

                    Ip = requestIp,

                    DataInclusao = DateTime.Now,

                    DataValidade = DateTime.Now.AddHours(10)
                };

                SalvarLoginAcessoItem(loginAcessoItem);

                loginItem.Senha = string.Empty;

                var retorno = new
                {
                    LoginItem = loginItem,
                    LoginAcessoItem = loginAcessoItem
                };

                var jsonRetorno = JsonConvert.SerializeObject(retorno);

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex);
            }
        }

        [HttpPost]
        public IActionResult RenovarLoginAcessoItem([FromForm] string parametroConteudo)
        {
            try
            {
                if (string.IsNullOrEmpty(parametroConteudo))
                    return base.ObterActionResult(new Exception("Argumento primário necessário"));

                var jsonObjeto = JsonConvert.DeserializeObject(parametroConteudo);

                var loginToken = ((JObject)jsonObjeto)["token"].ToString().ToLower();

                var loginSenha = ((JObject)jsonObjeto)["senha"].ToString();

                var loginAcessoItem = ObterLoginAcessoItemPorToken(loginToken);

                if (loginAcessoItem == null)
                    throw new ApplicationException("Usuário/Token não encontrado! Para continuar, faça login novamente.");

                var loginItem = ObterLoginItem(loginAcessoItem.LoginId);

                if (loginItem == null)
                    throw new UnauthorizedAccessException("Usuário não encontrado!");

                var loginSenhaMd5 = Auxiliar.Util.GerarHashMd5(loginSenha);

                var usuarioValido = ValidarUsuarioNoActiveDirectory("unishipping.corp", loginItem.Usuario, loginSenha);

                if (!loginItem.Senha.Equals(loginSenhaMd5) && !usuarioValido)
                    throw new UnauthorizedAccessException("Senha inválida!");

                var requestIp = base.ObterConnectionRemoteIpAddress();

                loginAcessoItem = CarregarLoginAcessoItem(loginItem.Id, requestIp);

                if (loginAcessoItem == null)
                {
                    loginAcessoItem = new Core.Entidade.Login.Acesso.AcessoItem
                    {
                        RegistroLoginId = loginItem.Id,

                        LoginId = loginItem.Id,

                        Token = GerarLoginAcessoToken(),

                        Ip = requestIp,

                        DataInclusao = DateTime.Now,

                        DataValidade = DateTime.Now.AddHours(10)
                    };

                    SalvarLoginAcessoItem(loginAcessoItem);
                }

                loginItem.Senha = string.Empty;

                var retorno = new
                {
                    LoginItem = loginItem,
                    LoginAcessoItem = loginAcessoItem
                };

                var jsonRetorno = JsonConvert.SerializeObject(retorno);

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var loginLista = ObterLoginLista();

                loginLista
                    .ForEach(x => x.Senha = string.Empty);

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Login.LoginItem>();

                var jsonRetorno = new
                {
                    LoginLista = loginLista,
                    TituloDicionarioItem = tituloDicionarioItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginListaPorFiltroItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginFiltroItem = ProcessarJsonParametro<Core.Filtro.Login.LoginItem>(jsonObjeto["loginFiltroItem"]);

                var loginLista = ObterLoginListaPorFiltroItem(loginFiltroItem);

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Login.LoginItem>();

                var jsonRetorno = JsonConvert.SerializeObject(new
                {
                    LoginLista = loginLista,
                    TituloDicionarioItem = tituloDicionarioItem
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["loginId"] == null)
                    throw new ArgumentException("Código do login necessário");

                var loginId = Convert.ToInt32(jsonObjeto["loginId"].ToString());

                var loginItem = ObterLoginItem(loginId);

                loginItem.Senha = string.Empty;

                var loginAtribuicaoLista = ObterLoginAtribuicaoListaPorLoginId(loginId);

                var pessoaContatoLista = ObterPessoaContatoListaPorPessoaId(loginItem.PessoaId);

                var jsonRetorno = new
                {
                    LoginItem = loginItem,
                    LoginAtribuicaoLista = loginAtribuicaoLista,
                    PessoaContatoLista = pessoaContatoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginItemPorToken([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var loginItem = ObterLoginItem(loginAcessoItem.LoginId);

                var loginAtribuicaoLista = ObterLoginAtribuicaoListaPorLoginId(loginAcessoItem.LoginId);

                var loginNotificacaoLista = ObterLoginNotificacaoListaPorLoginId(loginAcessoItem.LoginId);

                loginItem.Senha = string.Empty;

                var jsonRetorno = new
                {
                    LoginItem = loginItem,
                    LoginAtribuicaoLista = loginAtribuicaoLista,
                    LoginNotificacaoLista = loginNotificacaoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarLoginItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginItem = ProcessarJsonParametro<Core.Entidade.Login.LoginItem>(jsonObjeto["loginItem"], loginAcessoItem);

                var loginAtribuicaoLista = ProcessarJsonParametro<List<Core.Entidade.Login.Atribuicao.AtribuicaoItem>>(jsonObjeto["loginAtribuicaoLista"], loginAcessoItem);

                var pessoaContatoLista = ProcessarJsonParametro<List<Core.Entidade.Pessoa.Contato.ContatoItem>>(jsonObjeto["pessoaContatoLista"], loginAcessoItem);

                if (!string.IsNullOrEmpty(loginItem.Senha))
                    loginItem.Senha = Auxiliar.Util.GerarHashMd5(loginItem.Senha);

                loginItem = SalvarLoginItem(loginItem);

                loginAtribuicaoLista = AtualizarLoginAtribuicaoLista(loginItem, loginAtribuicaoLista);

                pessoaContatoLista = AtualizarPessoaContatoLista(new Core.Entidade.Pessoa.PessoaItem() { Id = loginItem.PessoaId }, pessoaContatoLista);

                var jsonRetorno = new
                {
                    LoginItem = loginItem,
                    LoginAtribuicaoLista = loginAtribuicaoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginGrupoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginGrupoLista = ObterLoginGrupoLista();

                var jsonRetorno = new
                {
                    LoginGrupoLista = loginGrupoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginGrupoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["loginGrupoId"] == null)
                    throw new ArgumentException("Código do grupo de acesso necessário");

                var loginGrupoId = Convert.ToInt32(jsonObjeto["loginGrupoId"].ToString());

                var loginGrupoItem = ObterLoginGrupoItem(loginGrupoId);

                var jsonRetorno = new
                {
                    LoginGrupoItem = loginGrupoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarLoginGrupoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["loginGrupoItem"] == null)
                    throw new ArgumentException("Informações do grupo de acesso necessário");

                var loginGrupoItem = ProcessarJsonParametro<Core.Entidade.Login.Grupo.GrupoItem>(jsonObjeto["loginGrupoItem"], loginAcessoItem);

                loginGrupoItem = SalvarLoginGrupoItem(loginGrupoItem);

                var jsonRetorno = new
                {
                    LoginGrupoItem = loginGrupoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginPerfilLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginPerfilLista = ObterLoginPerfilLista();

                var jsonRetorno = new
                {
                    LoginPerfilLista = loginPerfilLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginPerfilItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["loginPerfilId"] == null)
                    throw new ArgumentException("Código do grupo de acesso necessário");

                var loginPerfilId = Convert.ToInt32(jsonObjeto["loginPerfilId"].ToString());

                var loginPerfilItem = ObterLoginPerfilItem(loginPerfilId);

                var jsonRetorno = new
                {
                    LoginPerfilItem = loginPerfilItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarLoginPerfilItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["loginPerfilItem"] == null)
                    throw new ArgumentException("Informações do grupo de acesso necessário");

                var loginPerfilItem = ProcessarJsonParametro<Core.Entidade.Login.Perfil.PerfilItem>(jsonObjeto["loginPerfilItem"], loginAcessoItem);

                loginPerfilItem = SalvarLoginPerfilItem(loginPerfilItem);

                var jsonRetorno = new
                {
                    LoginPerfilItem = loginPerfilItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginSituacaoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginSituacaoLista = ObterLoginSituacaoLista();

                var jsonRetorno = new
                {
                    LoginSituacaoLista = loginSituacaoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarLoginSituacaoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginSituacaoId = Convert.ToInt32(jsonObjeto["loginSituacaoId"].ToString());

                var loginSituacaoItem = ObterLoginSituacaoItem(loginSituacaoId);

                var jsonRetorno = new
                {
                    LoginSituacaoItem = loginSituacaoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarLoginSituacaoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginSituacaoItem = ProcessarJsonParametro<Core.Entidade.Login.Situacao.SituacaoItem>(jsonObjeto["loginSituacaoItem"], loginAcessoItem);

                loginSituacaoItem = SalvarLoginSituacaoItem(loginSituacaoItem);

                var jsonRetorno = new
                {
                    LoginSituacaoItem = loginSituacaoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirLoginNotificacaoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginNotificacaoLista = ProcessarJsonParametro<List<Core.Entidade.Login.Notificacao.NotificacaoItem>>(jsonObjeto["loginNotificacaoLista"], loginAcessoItem);

                loginNotificacaoLista
                    .ForEach(x => ExcluirLoginNotificacaoItem(x));

                var jsonRetorno = new
                {
                    LoginNotificacaoLista = loginNotificacaoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirLoginNotificacaoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var loginNotificacaoItem = ProcessarJsonParametro<Core.Entidade.Login.Notificacao.NotificacaoItem>(jsonObjeto["loginNotificacaoItem"], loginAcessoItem);

                loginNotificacaoItem = ExcluirLoginNotificacaoItem(loginNotificacaoItem);

                var jsonRetorno = new
                {
                    LoginNotificacaoItem = loginNotificacaoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }


        #endregion

        #region Métodos Privados

        private string GerarLoginAcessoToken()
        {
            return (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "");
        }

        private List<Core.Entidade.Login.LoginItem> ObterLoginLista()
        {
            var loginNegocio = new Core.Negocio.Login.LoginItem();

            var loginLista = loginNegocio.CarregarLista();

            return loginLista;
        }

        private List<Core.Entidade.Login.LoginItem> ObterLoginListaPorFiltroItem(Core.Filtro.Login.LoginItem loginFiltroItem)
        {
            var loginNegocio = new Core.Negocio.Login.LoginItem();

            var loginLista = loginNegocio.CarregarListaPorFiltroItem<Core.Entidade.Login.LoginItem, Core.Filtro.Login.LoginItem>(loginFiltroItem);

            return loginLista;
        }

        private Core.Entidade.Login.LoginItem ObterLoginItem(int loginId)
        {
            var loginNegocio = new Core.Negocio.Login.LoginItem();

            var loginItem = loginNegocio.CarregarItem(loginId);

            return loginItem;
        }

        private List<Core.Entidade.Login.Atribuicao.AtribuicaoItem> ObterLoginAtribuicaoListaPorLoginId(int loginId)
        {
            var loginAtribuicaoNegocio = new Core.Negocio.Login.Atribuicao.AtribuicaoItem();

            var loginAtribuicaoLista = loginAtribuicaoNegocio.CarregarListaPorLoginId(loginId);

            return loginAtribuicaoLista;
        }

        private Core.Entidade.Login.LoginItem ObterLoginItemPorUsuario(string loginUsuario)
        {
            var loginNegocio = new Core.Negocio.Login.LoginItem();

            var loginItem = loginNegocio.CarregarItemPorUsuario(loginUsuario);

            return loginItem;
        }

        private Core.Entidade.Login.LoginItem SalvarLoginItem(Core.Entidade.Login.LoginItem loginItem)
        {
            var loginGrupoNegocio = new Core.Negocio.Login.LoginItem();

            loginItem = loginGrupoNegocio.SalvarItem(loginItem);

            return loginItem;
        }

        private List<Core.Entidade.Login.Atribuicao.AtribuicaoItem> AtualizarLoginAtribuicaoLista(Core.Entidade.Login.LoginItem loginItem, List<Core.Entidade.Login.Atribuicao.AtribuicaoItem> loginAtribuicaoNovoLista)
        {
            var loginAtribuicaoNegocio = new Core.Negocio.Login.Atribuicao.AtribuicaoItem();

            var loginAtribuicaoAtualLista = loginAtribuicaoNegocio.CarregarListaPorLoginId(loginItem.Id);

            for (int i = 0; i < loginAtribuicaoNovoLista.Count; i++)
            {
                var loginAtribuicaoNovoItem = loginAtribuicaoNovoLista[i];

                if (loginAtribuicaoNovoItem.LoginId.Equals(0))
                    loginAtribuicaoNovoItem.LoginId = loginItem.Id;
            }

            var loginAtribuicaoInserirLista = loginAtribuicaoNovoLista
                .Where(x => !loginAtribuicaoAtualLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            var loginAtribuicaoAtualizarLista = loginAtribuicaoNovoLista
                .Where(x => loginAtribuicaoAtualLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            var loginAtribuicaoExcluirLista = loginAtribuicaoAtualLista
                .Where(x => !loginAtribuicaoNovoLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            foreach (var loginAtribuicaoInserirItem in loginAtribuicaoInserirLista)
                loginAtribuicaoNegocio.InserirItem(loginAtribuicaoInserirItem);

            foreach (var loginAtribuicaoAtualizarItem in loginAtribuicaoAtualizarLista)
                loginAtribuicaoNegocio.AtualizarItem(loginAtribuicaoAtualizarItem);

            foreach (var loginAtribuicaoExcluirItem in loginAtribuicaoExcluirLista)
                loginAtribuicaoNegocio.ExcluirItem(loginAtribuicaoExcluirItem);

            return loginAtribuicaoNovoLista;
        }

        private Core.Entidade.Login.Acesso.AcessoItem CarregarLoginAcessoItem(int loginId, string ip)
        {
            var loginAcessoNegocio = new Core.Negocio.Login.Acesso.AcessoItem();

            var loginAcessoItem = loginAcessoNegocio.CarregarItemValidoPorRegistroLoginId(loginId, ip);

            return loginAcessoItem;
        }

        private Core.Entidade.Login.Acesso.AcessoItem SalvarLoginAcessoItem(Core.Entidade.Login.Acesso.AcessoItem loginAcessoItem)
        {
            var loginAcessoNegocio = new Core.Negocio.Login.Acesso.AcessoItem();

            if (loginAcessoItem.Id.Equals(0))
                loginAcessoItem = loginAcessoNegocio.InserirItem(loginAcessoItem);
            else
                loginAcessoItem = loginAcessoNegocio.AtualizarItem(loginAcessoItem);

            return loginAcessoItem;
        }

        private Core.Entidade.Login.Grupo.GrupoItem ObterLoginGrupoItem(int loginGrupoId)
        {
            var loginGrupoNegocio = new Core.Negocio.Login.Grupo.GrupoItem();

            var loginGrupoItem = loginGrupoNegocio.CarregarItem(loginGrupoId);

            return loginGrupoItem;
        }

        private List<Core.Entidade.Login.Grupo.GrupoItem> ObterLoginGrupoLista()
        {
            var loginGrupoNegocio = new Core.Negocio.Login.Grupo.GrupoItem();

            var loginGrupoLista = loginGrupoNegocio.CarregarLista();

            return loginGrupoLista;
        }

        private Core.Entidade.Login.Grupo.GrupoItem SalvarLoginGrupoItem(Core.Entidade.Login.Grupo.GrupoItem loginGrupoItem)
        {
            var loginGrupoNegocio = new Core.Negocio.Login.Grupo.GrupoItem();

            loginGrupoItem = loginGrupoNegocio.SalvarItem(loginGrupoItem);

            return loginGrupoItem;
        }

        private Core.Entidade.Login.Perfil.PerfilItem ObterLoginPerfilItem(int loginPerfilId)
        {
            var loginPerfilNegocio = new Core.Negocio.Login.Perfil.PerfilItem();

            var loginPerfilItem = loginPerfilNegocio.CarregarItem(loginPerfilId);

            return loginPerfilItem;
        }

        private List<Core.Entidade.Login.Perfil.PerfilItem> ObterLoginPerfilLista()
        {
            var loginPerfilNegocio = new Core.Negocio.Login.Perfil.PerfilItem();

            var loginPerfilLista = loginPerfilNegocio.CarregarLista();

            return loginPerfilLista;
        }

        private Core.Entidade.Login.Perfil.PerfilItem SalvarLoginPerfilItem(Core.Entidade.Login.Perfil.PerfilItem loginPerfilItem)
        {
            var loginPerfilNegocio = new Core.Negocio.Login.Perfil.PerfilItem();

            loginPerfilItem = loginPerfilNegocio.SalvarItem(loginPerfilItem);

            return loginPerfilItem;
        }

        private Core.Entidade.Login.Acesso.AcessoItem ObterLoginAcessoItemPorToken(string loginToken)
        {
            return new Core.Negocio.Login.Acesso.AcessoItem().CarregarItemPorToken(loginToken);
        }

        private Core.Entidade.Login.Situacao.SituacaoItem ObterLoginSituacaoItem(int loginSituacaoId)
        {
            var loginSituacaoNegocio = new Core.Negocio.Login.Situacao.SituacaoItem();

            var loginSituacaoItem = loginSituacaoNegocio.CarregarItem(loginSituacaoId);

            return loginSituacaoItem;
        }

        private List<Core.Entidade.Login.Situacao.SituacaoItem> ObterLoginSituacaoLista()
        {
            var loginSituacaoNegocio = new Core.Negocio.Login.Situacao.SituacaoItem();

            var loginSituacaoLista = loginSituacaoNegocio.CarregarLista();

            return loginSituacaoLista;
        }

        private Core.Entidade.Login.Situacao.SituacaoItem SalvarLoginSituacaoItem(Core.Entidade.Login.Situacao.SituacaoItem loginSituacaoItem)
        {
            var loginSituacaoNegocio = new Core.Negocio.Login.Situacao.SituacaoItem();

            loginSituacaoItem = loginSituacaoNegocio.SalvarItem(loginSituacaoItem);

            return loginSituacaoItem;
        }

        private bool ValidarUsuarioNoActiveDirectory(string domainName, string userName, string password)
        {
            try
            {
                var directoryEntry = new DirectoryEntry("LDAP://" + domainName, userName, password);

                var directorySearcher = new DirectorySearcher(directoryEntry);

                var searchResult = null as SearchResult;

                directorySearcher.Filter = "(!userAccountControl:1.2.840.113556.1.4.803:=2)";

                if ((searchResult = directorySearcher.FindOne()) != null)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        private List<Core.Entidade.Login.Notificacao.NotificacaoItem> ObterLoginNotificacaoListaPorLoginId(int loginId)
        {
            return new Core.Negocio.Login.Notificacao.NotificacaoItem().CarregarListaPorLoginId(loginId);
        }

        private Core.Entidade.Login.Notificacao.NotificacaoItem ExcluirLoginNotificacaoItem(Core.Entidade.Login.Notificacao.NotificacaoItem loginNotificacaoItem)
        {
            return new Core.Negocio.Login.Notificacao.NotificacaoItem().ExcluirItem(loginNotificacaoItem);
        }

        private List<Core.Entidade.Pessoa.Contato.ContatoItem> ObterPessoaContatoListaPorPessoaId(int pessoaId)
        {
            var pessoaContatoNegocio = new Core.Negocio.Pessoa.Contato.ContatoItem();

            var pessoaContatoLista = pessoaContatoNegocio.CarregarListaPorPessoaId(pessoaId);

            return pessoaContatoLista;
        }

        private List<Core.Entidade.Pessoa.Contato.ContatoItem> AtualizarPessoaContatoLista(Core.Entidade.Pessoa.PessoaItem pessoaItem, List<Core.Entidade.Pessoa.Contato.ContatoItem> pessoaContatoNovoLista)
        {
            if (pessoaContatoNovoLista == null)
                return null;

            var pessoaContatoNegocio = new Core.Negocio.Pessoa.Contato.ContatoItem();

            var pessoaContatoAtualLista = pessoaContatoNegocio.CarregarListaPorPessoaId(pessoaItem.Id);

            pessoaContatoAtualLista
                .Where(x => !pessoaContatoNovoLista.Select(y => y.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => pessoaContatoNegocio.ExcluirItem(x));

            pessoaContatoNovoLista = pessoaContatoNovoLista
                .Where(x => x.Id.Equals(0) || pessoaContatoAtualLista.Select(y => y.Id).Contains(x.Id))
                .Select(x =>
                {
                    x.PessoaId = pessoaItem.Id;

                    x = pessoaContatoNegocio.SalvarItem(x);

                    return x;
                })
                .ToList();

            return pessoaContatoNovoLista;
        }

        #endregion
    }
}