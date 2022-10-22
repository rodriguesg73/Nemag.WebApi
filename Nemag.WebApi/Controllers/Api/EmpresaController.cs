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
    public partial class EmpresaController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarEmpresaLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var empresaLista = ObterEmpresaLista();

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Empresa.EmpresaItem>();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    EmpresaLista = empresaLista,
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
        public IActionResult CarregarEmpresaListaPorEmpresaCategoriaId([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var empresaCategoriaId = Convert.ToInt32(jsonObjeto["empresaCategoriaId"].ToString());

                var empresaLista = ObterEmpresaListaPorEmpresaCategoriaId(empresaCategoriaId);

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Empresa.EmpresaItem>();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    EmpresaLista = empresaLista,
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
        public IActionResult CarregarEmpresaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["empresaId"] == null)
                    throw new ArgumentException("Código do empresa necessário");

                var empresaId = Convert.ToInt32(jsonObjeto["empresaId"].ToString());

                var empresaItem = ObterEmpresaItem(empresaId);

                var jsonRetorno = new 
                {
                    EmpresaItem = empresaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarEmpresaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var empresaItem = ProcessarJsonParametro<Core.Entidade.Empresa.EmpresaItem>(jsonObjeto["empresaItem"], loginAcessoItem);

                empresaItem = SalvarEmpresaItem(empresaItem);

                var jsonRetorno = new 
                {
                    EmpresaItem = empresaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirEmpresaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var empresaItem = ProcessarJsonParametro<Core.Entidade.Empresa.EmpresaItem>(jsonObjeto["empresaItem"], loginAcessoItem);

                empresaItem = ExcluirEmpresaItem(empresaItem);

                var jsonRetorno = new 
                {
                    EmpresaItem = empresaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarEmpresaCategoriaLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var empresaCategoriaLista = ObterEmpresaCategoriaLista();

                empresaCategoriaLista = empresaCategoriaLista
                    .OrderBy(X => X.Nome)
                    .ToList();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    EmpresaCategoriaLista = empresaCategoriaLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarEmpresaCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["empresaCategoriaId"] == null)
                    throw new ArgumentException("Código do empresaCategoria necessário");

                var empresaCategoriaId = Convert.ToInt32(jsonObjeto["empresaCategoriaId"].ToString());

                var empresaCategoriaItem = ObterEmpresaCategoriaItem(empresaCategoriaId);

                var jsonRetorno = new 
                {
                    EmpresaCategoriaItem = empresaCategoriaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarEmpresaCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var empresaCategoriaItem = ProcessarJsonParametro<Core.Entidade.Empresa.Categoria.CategoriaItem>(jsonObjeto["empresaCategoriaItem"], loginAcessoItem);

                empresaCategoriaItem = SalvarEmpresaCategoriaItem(empresaCategoriaItem);

                var jsonRetorno = new 
                {
                    EmpresaCategoriaItem = empresaCategoriaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirEmpresaCategoriaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var empresaCategoriaItem = ProcessarJsonParametro<Core.Entidade.Empresa.Categoria.CategoriaItem>(jsonObjeto["empresaCategoriaItem"], loginAcessoItem);

                empresaCategoriaItem = ExcluirEmpresaCategoriaItem(empresaCategoriaItem);

                var jsonRetorno = new 
                {
                    EmpresaCategoriaItem = empresaCategoriaItem
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

        private List<Core.Entidade.Empresa.EmpresaItem> ObterEmpresaLista()
        {
            var empresaNegocio = new Core.Negocio.Empresa.EmpresaItem();

            var empresaLista = empresaNegocio.CarregarLista();

            return empresaLista;
        }

        private List<Core.Entidade.Empresa.EmpresaItem> ObterEmpresaListaPorEmpresaCategoriaId(int empresaCategoriaId)
        {
            var empresaNegocio = new Core.Negocio.Empresa.EmpresaItem();

            var empresaLista = empresaNegocio.CarregarListaPorEmpresaCategoriaId(empresaCategoriaId);

            return empresaLista;
        }

        private Core.Entidade.Empresa.EmpresaItem ObterEmpresaItem(int empresaId)
        {
            var empresaNegocio = new Core.Negocio.Empresa.EmpresaItem();

            var empresaItem = empresaNegocio.CarregarItem(empresaId);

            return empresaItem;
        }

        private Core.Entidade.Empresa.EmpresaItem SalvarEmpresaItem(Core.Entidade.Empresa.EmpresaItem empresaItem)
        {
            var empresaNegocio = new Core.Negocio.Empresa.EmpresaItem();

            empresaItem = empresaNegocio.SalvarItem(empresaItem);

            return empresaItem;
        }

        private Core.Entidade.Empresa.EmpresaItem ExcluirEmpresaItem(Core.Entidade.Empresa.EmpresaItem empresaItem)
        {
            var empresaNegocio = new Core.Negocio.Empresa.EmpresaItem();

            empresaItem = empresaNegocio.ExcluirItem(empresaItem);

            return empresaItem;
        }

        private List<Core.Entidade.Empresa.Categoria.CategoriaItem> ObterEmpresaCategoriaLista()
        {
            var empresaCategoriaNegocio = new Core.Negocio.Empresa.Categoria.CategoriaItem();

            var empresaCategoriaLista = empresaCategoriaNegocio.CarregarLista();

            return empresaCategoriaLista;
        }

        private Core.Entidade.Empresa.Categoria.CategoriaItem ObterEmpresaCategoriaItem(int empresaCategoriaId)
        {
            var empresaCategoriaNegocio = new Core.Negocio.Empresa.Categoria.CategoriaItem();

            var empresaCategoriaItem = empresaCategoriaNegocio.CarregarItem(empresaCategoriaId);

            return empresaCategoriaItem;
        }

        private Core.Entidade.Empresa.Categoria.CategoriaItem SalvarEmpresaCategoriaItem(Core.Entidade.Empresa.Categoria.CategoriaItem empresaCategoriaItem)
        {
            var empresaCategoriaNegocio = new Core.Negocio.Empresa.Categoria.CategoriaItem();

            empresaCategoriaItem = empresaCategoriaNegocio.SalvarItem(empresaCategoriaItem);

            return empresaCategoriaItem;
        }

        private Core.Entidade.Empresa.Categoria.CategoriaItem ExcluirEmpresaCategoriaItem(Core.Entidade.Empresa.Categoria.CategoriaItem empresaCategoriaItem)
        {
            var empresaCategoriaNegocio = new Core.Negocio.Empresa.Categoria.CategoriaItem();

            empresaCategoriaItem = empresaCategoriaNegocio.ExcluirItem(empresaCategoriaItem);

            return empresaCategoriaItem;
        }


        #endregion
    }
}