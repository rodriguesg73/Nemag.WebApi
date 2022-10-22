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
    public partial class VendaController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarVendaLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var vendaLista = ObterVendaLista();

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Venda.VendaItem>();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    VendaLista = vendaLista,
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
        public IActionResult CarregarVendaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["vendaId"] == null)
                    throw new ArgumentException("Código do venda necessário");

                var vendaId = Convert.ToInt32(jsonObjeto["vendaId"].ToString());

                var vendaItem = ObterVendaItem(vendaId);

                var vendaProdutoLista = ObterVendaProdutoListaPorVendaId(vendaId);

                var jsonRetorno = new 
                {
                    VendaItem = vendaItem,
                    VendaProdutoLista = vendaProdutoLista
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarVendaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var vendaItem = ProcessarJsonParametro<Core.Entidade.Venda.VendaItem>(jsonObjeto["vendaItem"], loginAcessoItem);

                var vendaProdutoLista = ProcessarJsonParametro<List<Core.Entidade.Venda.Produto.ProdutoItem>>(jsonObjeto["produtoVendaLista"], loginAcessoItem);

                vendaItem = SalvarVendaItem(vendaItem);

                vendaProdutoLista = vendaProdutoLista
                    .Select(x =>
                    {
                        x.VendaId = vendaItem.Id;
                        
                        x.ProdutoId = x.ProdutoId;

                        x.RegistroLoginId = loginAcessoItem.LoginId;

                        x = SalvarVendaProdutoItem(x);
                        
                        return x;
                    }).ToList();

                var jsonRetorno = new 
                {
                    VendaItem = vendaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirVendaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var vendaItem = ProcessarJsonParametro<Core.Entidade.Venda.VendaItem>(jsonObjeto["vendaItem"], loginAcessoItem);

                vendaItem = ExcluirVendaItem(vendaItem);

                var jsonRetorno = new 
                {
                    VendaItem = vendaItem
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

        private List<Core.Entidade.Venda.VendaItem> ObterVendaLista()
        {
            var vendaNegocio = new Core.Negocio.Venda.VendaItem();

            var vendaLista = vendaNegocio.CarregarLista();

            return vendaLista;
        }

        private Core.Entidade.Venda.VendaItem ObterVendaItem(int vendaId)
        {
            var vendaNegocio = new Core.Negocio.Venda.VendaItem();

            var vendaItem = vendaNegocio.CarregarItem(vendaId);

            return vendaItem;
        }

        private Core.Entidade.Venda.VendaItem SalvarVendaItem(Core.Entidade.Venda.VendaItem vendaItem)
        {
            var vendaNegocio = new Core.Negocio.Venda.VendaItem();

            vendaItem = vendaNegocio.SalvarItem(vendaItem);

            return vendaItem;

        }


        private Core.Entidade.Venda.VendaItem ExcluirVendaItem(Core.Entidade.Venda.VendaItem vendaItem)
        {
            var vendaNegocio = new Core.Negocio.Venda.VendaItem();

            vendaItem = vendaNegocio.ExcluirItem(vendaItem);

            return vendaItem;
        }

        private Core.Entidade.Venda.Produto.ProdutoItem SalvarVendaProdutoItem(Core.Entidade.Venda.Produto.ProdutoItem vendaProdutoItem)
        {
            var vendaProdutoNegocio = new Core.Negocio.Venda.Produto.ProdutoItem();

            vendaProdutoItem = vendaProdutoNegocio.SalvarItem(vendaProdutoItem);

            return vendaProdutoItem;
        }

        private List<Core.Entidade.Venda.Produto.ProdutoItem> ObterVendaProdutoListaPorVendaId(int vendaId)
        {
            var vendaProdutoNegocio = new Core.Negocio.Venda.Produto.ProdutoItem();

            var vendaProdutoLista = vendaProdutoNegocio.CarregarListaPorVendaId(vendaId);

            return vendaProdutoLista;

        }
        #endregion
    }
}