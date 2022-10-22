using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nemag.WebApi.Controllers.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaisController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarPaisLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var paisLista = ObterPaisLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    PaisLista = paisLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarPaisItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["paisId"] == null)
                    throw new ArgumentException("Código do pais necessário");

                var paisId = Convert.ToInt32(jsonObjeto["paisId"].ToString());

                var paisItem = ObterPaisItem(paisId);

                var jsonRetorno = new 
                {
                    PaisItem = paisItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarPaisItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var paisItem = ProcessarJsonParametro<Core.Entidade.Pais.PaisItem>(jsonObjeto["paisItem"], loginAcessoItem);

                paisItem = SalvarPaisItem(paisItem);

                var jsonRetorno = new 
                {
                    PaisItem = paisItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirPaisItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var paisItem = ProcessarJsonParametro<Core.Entidade.Pais.PaisItem>(jsonObjeto["paisItem"], loginAcessoItem);

                paisItem = ExcluirPaisItem(paisItem);

                var jsonRetorno = new 
                {
                    PaisItem = paisItem
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
        
        private List<Core.Entidade.Pais.PaisItem> ObterPaisLista()
        {
            var paisNegocio = new Core.Negocio.Pais.PaisItem();

            var paisLista = paisNegocio.CarregarLista();

            return paisLista;
        }

        private Core.Entidade.Pais.PaisItem ObterPaisItem(int paisId)
        {
            var paisNegocio = new Core.Negocio.Pais.PaisItem();

            var paisItem = paisNegocio.CarregarItem(paisId);

            return paisItem;
        }

        private Core.Entidade.Pais.PaisItem SalvarPaisItem(Core.Entidade.Pais.PaisItem paisItem)
        {
            var paisNegocio = new Core.Negocio.Pais.PaisItem();

            paisItem = paisNegocio.SalvarItem(paisItem);

            return paisItem;
        }

        private Core.Entidade.Pais.PaisItem ExcluirPaisItem(Core.Entidade.Pais.PaisItem paisItem)
        {
            var paisNegocio = new Core.Negocio.Pais.PaisItem();

            paisItem = paisNegocio.ExcluirItem(paisItem);

            return paisItem;
        }
        
        #endregion
    }
}