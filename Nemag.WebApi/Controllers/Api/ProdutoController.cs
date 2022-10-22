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
    public class ProdutoController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarProdutoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var produtoLista = ObterProdutoLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    ProdutoLista = produtoLista,
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarProdutoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["produtoId"] == null)
                    throw new ArgumentException("Código do produto necessário");

                var produtoId = Convert.ToInt32(jsonObjeto["produtoId"].ToString());

                var produtoItem = ObterProdutoItem(produtoId);

                var jsonRetorno = new 
                {
                    ProdutoItem = produtoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarProdutoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var produtoItem = ProcessarJsonParametro<Core.Entidade.Produto.ProdutoItem>(jsonObjeto["produtoItem"], loginAcessoItem);

                produtoItem = SalvarProdutoItem(produtoItem);

                var jsonRetorno = new 
                {
                    ProdutoItem = produtoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirProdutoItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var produtoItem = ProcessarJsonParametro<Core.Entidade.Produto.ProdutoItem>(jsonObjeto["produtoItem"], loginAcessoItem);

                produtoItem = ExcluirProdutoItem(produtoItem);

                var jsonRetorno = new 
                {
                    ProdutoItem = produtoItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarProdutoCategoriaLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var produtoCategoriaLista = ObterProdutoCategoriaLista();

                produtoCategoriaLista = produtoCategoriaLista
                    .OrderBy(X => X.Nome)
                    .ToList();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    ProdutoCategoriaLista = produtoCategoriaLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarProdutoCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["produtoCategoriaId"] == null)
                    throw new ArgumentException("Código do produtoCategoria necessário");

                var produtoCategoriaId = Convert.ToInt32(jsonObjeto["produtoCategoriaId"].ToString());

                var produtoCategoriaItem = ObterProdutoCategoriaItem(produtoCategoriaId);

                var jsonRetorno = new 
                {
                    ProdutoCategoriaItem = produtoCategoriaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarProdutoCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var produtoCategoriaItem = ProcessarJsonParametro<Core.Entidade.Produto.Categoria.CategoriaItem>(jsonObjeto["produtoCategoriaItem"], loginAcessoItem);

                produtoCategoriaItem = SalvarProdutoCategoriaItem(produtoCategoriaItem);

                var jsonRetorno = new 
                {
                    ProdutoCategoriaItem = produtoCategoriaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirProdutoCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var produtoCategoriaItem = ProcessarJsonParametro<Core.Entidade.Produto.Categoria.CategoriaItem>(jsonObjeto["produtoCategoriaItem"], loginAcessoItem);

                produtoCategoriaItem = ExcluirProdutoCategoriaItem(produtoCategoriaItem);

                var jsonRetorno = new 
                {
                    ProdutoCategoriaItem = produtoCategoriaItem
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

        private List<Core.Entidade.Produto.ProdutoItem> ObterProdutoLista()
        {
            var produtoNegocio = new Core.Negocio.Produto.ProdutoItem();

            var produtoLista = produtoNegocio.CarregarLista();

            return produtoLista;
        }

        private Core.Entidade.Produto.ProdutoItem ObterProdutoItem(int produtoId)
        {
            var produtoNegocio = new Core.Negocio.Produto.ProdutoItem();

            var produtoItem = produtoNegocio.CarregarItem(produtoId);

            return produtoItem;
        }

        private Core.Entidade.Produto.ProdutoItem SalvarProdutoItem(Core.Entidade.Produto.ProdutoItem produtoItem)
        {
            var produtoNegocio = new Core.Negocio.Produto.ProdutoItem();

            produtoItem = produtoNegocio.SalvarItem(produtoItem);

            return produtoItem;
        }

        private Core.Entidade.Produto.ProdutoItem ExcluirProdutoItem(Core.Entidade.Produto.ProdutoItem produtoItem)
        {
            var produtoNegocio = new Core.Negocio.Produto.ProdutoItem();

            produtoItem = produtoNegocio.ExcluirItem(produtoItem);

            return produtoItem;
        }

        private List<Core.Entidade.Produto.Categoria.CategoriaItem> ObterProdutoCategoriaLista()
        {
            var produtoCategoriaNegocio = new Core.Negocio.Produto.Categoria.CategoriaItem();

            var produtoCategoriaLista = produtoCategoriaNegocio.CarregarLista();

            return produtoCategoriaLista;
        }

        private Core.Entidade.Produto.Categoria.CategoriaItem ObterProdutoCategoriaItem(int produtoCategoriaId)
        {
            var produtoCategoriaNegocio = new Core.Negocio.Produto.Categoria.CategoriaItem();

            var produtoCategoriaItem = produtoCategoriaNegocio.CarregarItem(produtoCategoriaId);

            return produtoCategoriaItem;
        }

        private Core.Entidade.Produto.Categoria.CategoriaItem SalvarProdutoCategoriaItem(Core.Entidade.Produto.Categoria.CategoriaItem produtoCategoriaItem)
        {
            var produtoCategoriaNegocio = new Core.Negocio.Produto.Categoria.CategoriaItem();

            produtoCategoriaItem = produtoCategoriaNegocio.SalvarItem(produtoCategoriaItem);

            return produtoCategoriaItem;
        }

        private Core.Entidade.Produto.Categoria.CategoriaItem ExcluirProdutoCategoriaItem(Core.Entidade.Produto.Categoria.CategoriaItem produtoCategoriaItem)
        {
            var produtoCategoriaNegocio = new Core.Negocio.Produto.Categoria.CategoriaItem();

            produtoCategoriaItem = produtoCategoriaNegocio.ExcluirItem(produtoCategoriaItem);

            return produtoCategoriaItem;
        }
        #endregion

    }
}