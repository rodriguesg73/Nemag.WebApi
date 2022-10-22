using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nemag.WebApi.Controllers.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarMenuLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var menuLista = ObterMenuLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    MenuLista = menuLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarMenuListaPorLoginToken([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var menuLista = ObterMenuListaPorLoginId(loginAcessoItem.LoginId);

                var menuPaginaInicialItem = menuLista
                    .Where(x => x.Id.Equals(1))
                    .FirstOrDefault();

                menuLista = menuLista
                    .Where(x => !x.Id.Equals(1))
                    .OrderBy(x => x.MenuSuperiorId)
                    .ThenBy(x => x.Titulo)
                    .ToList();

                menuLista.Insert(0, menuPaginaInicialItem);

                var retorno = new 
                {
                    MenuLista = menuLista
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
        public IActionResult CarregarMenuItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["menuId"] == null)
                    throw new ArgumentException("Código do menu necessário");

                var menuId = Convert.ToInt32(jsonObjeto["menuId"].ToString());

                var menuItem = ObterMenuItem(menuId);

                var menuPermissaoAtribuicaoLista = ObterMenuPermissaoAtribuicaoListaPorMenuId(menuId);

                var menuPermissaoLoginLista = ObterMenuPermissaoLoginListaPorMenuId(menuId);

                var jsonRetorno = new 
                {
                    MenuItem = menuItem,
                    MenuPermissaoAtribuicaoLista = menuPermissaoAtribuicaoLista,
                    MenuPermissaoLoginLista = menuPermissaoLoginLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarMenuItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var menuItem = ProcessarJsonParametro<Core.Entidade.Menu.MenuItem>(jsonObjeto["menuItem"], loginAcessoItem);

                var menuPermissaoAtribuicaoLista = ProcessarJsonParametro<List<Core.Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem>>(jsonObjeto["menuPermissaoAtribuicaoLista"], loginAcessoItem);

                var menuPermissaoLoginLista = ProcessarJsonParametro<List<Core.Entidade.Menu.Permissao.Login.LoginItem>>(jsonObjeto["menuPermissaoLoginLista"], loginAcessoItem);

                menuItem = SalvarMenuItem(menuItem);

                menuPermissaoAtribuicaoLista = AtualizarMenuPermissaoAtribuicaoLista(menuItem, menuPermissaoAtribuicaoLista);

                //menuPermissaoLoginLista = AtualizarMenuPermissaoLoginLista(menuItem, menuPermissaoLoginLista);

                var jsonRetorno = new 
                {
                    MenuItem = menuItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirMenuItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var menuItem = ProcessarJsonParametro<Core.Entidade.Menu.MenuItem>(jsonObjeto["menuItem"], loginAcessoItem);

                menuItem = ExcluirMenuItem(menuItem);

                var jsonRetorno = new 
                {
                    MenuItem = menuItem
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

        private List<Core.Entidade.Menu.MenuItem> ObterMenuLista()
        {
            var menuNegocio = new Core.Negocio.Menu.MenuItem();

            var menuLista = menuNegocio.CarregarLista();

            return menuLista;
        }

        private Core.Entidade.Menu.MenuItem ObterMenuItem(int menuId)
        {
            var menuNegocio = new Core.Negocio.Menu.MenuItem();

            var menuItem = menuNegocio.CarregarItem(menuId);

            return menuItem;
        }

        private Core.Entidade.Menu.MenuItem SalvarMenuItem(Core.Entidade.Menu.MenuItem menuItem)
        {
            var menuNegocio = new Core.Negocio.Menu.MenuItem();

            menuItem = menuNegocio.SalvarItem(menuItem);

            return menuItem;
        }

        private Core.Entidade.Menu.MenuItem ExcluirMenuItem(Core.Entidade.Menu.MenuItem menuItem)
        {
            var menuNegocio = new Core.Negocio.Menu.MenuItem();

            menuItem = menuNegocio.ExcluirItem(menuItem);

            return menuItem;
        }

        private List<Core.Entidade.Menu.MenuItem> ObterMenuListaPorLoginId(int loginId)
        {
            var menuNegocio = new Core.Negocio.Menu.MenuItem();

            var menuLista = menuNegocio.CarregarListaPorLoginId(loginId);

            return menuLista;
        }

        private List<Core.Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> ObterMenuPermissaoAtribuicaoListaPorMenuId(int menuId)
        {
            var menuPermissaoAtribuicaoNegocio = new Core.Negocio.Menu.Permissao.Atribuicao.AtribuicaoItem();

            var menuPermissaoAtribuicaoLista = menuPermissaoAtribuicaoNegocio.CarregarListaPorMenuId(menuId);

            return menuPermissaoAtribuicaoLista;
        }

        private List<Core.Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> AtualizarMenuPermissaoAtribuicaoLista(Core.Entidade.Menu.MenuItem menuItem, List<Core.Entidade.Menu.Permissao.Atribuicao.AtribuicaoItem> menuPermissaoAtribuicaoNovoLista)
        {
            var menuPermissaoAtribuicaoNegocio = new Core.Negocio.Menu.Permissao.Atribuicao.AtribuicaoItem();

            var menuPermissaoAtribuicaoAtualLista = menuPermissaoAtribuicaoNegocio.CarregarListaPorMenuId(menuItem.Id);

            menuPermissaoAtribuicaoNovoLista
                .ForEach(x => x.MenuId = menuItem.Id);

            var menuPermissaoAtribuicaoInserirLista = menuPermissaoAtribuicaoNovoLista
                .Where(x => !menuPermissaoAtribuicaoAtualLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            var menuPermissaoAtribuicaoAtualizarLista = menuPermissaoAtribuicaoNovoLista
                .Where(x => menuPermissaoAtribuicaoAtualLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            var menuPermissaoAtribuicaoExcluirLista = menuPermissaoAtribuicaoAtualLista
                .Where(x => !menuPermissaoAtribuicaoNovoLista
                    .Select(a => a.Id)
                    .ToList()
                    .Contains(x.Id)
                ).ToList();

            foreach (var menuPermissaoAtribuicaoInserirItem in menuPermissaoAtribuicaoInserirLista)
                menuPermissaoAtribuicaoNegocio.InserirItem(menuPermissaoAtribuicaoInserirItem);

            foreach (var menuPermissaoAtribuicaoAtualizarItem in menuPermissaoAtribuicaoAtualizarLista)
                menuPermissaoAtribuicaoNegocio.AtualizarItem(menuPermissaoAtribuicaoAtualizarItem);

            foreach (var menuPermissaoAtribuicaoExcluirItem in menuPermissaoAtribuicaoExcluirLista)
                menuPermissaoAtribuicaoNegocio.ExcluirItem(menuPermissaoAtribuicaoExcluirItem);

            return menuPermissaoAtribuicaoNovoLista;
        }

        private List<Core.Entidade.Menu.Permissao.Login.LoginItem> ObterMenuPermissaoLoginListaPorMenuId(int menuId)
        {
            var menuPermissaoLoginNegocio = new Core.Negocio.Menu.Permissao.Login.LoginItem();

            var menuPermissaoLoginLista = menuPermissaoLoginNegocio.CarregarListaPorMenuId(menuId);

            return menuPermissaoLoginLista;
        }

        #endregion
    }
}