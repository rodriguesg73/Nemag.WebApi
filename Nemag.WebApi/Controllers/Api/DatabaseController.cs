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
    public class DatabaseController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarDatabaseLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var databaseLista = ObterDatabaseLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    DatabaseLista = databaseLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarDatabaseItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["databaseId"] == null)
                    throw new ArgumentException("Código do database necessário");

                var databaseId = Convert.ToInt32(jsonObjeto["databaseId"].ToString());

                var databaseItem = ObterDatabaseItem(databaseId);

                var jsonRetorno = new 
                {
                    DatabaseItem = databaseItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarDatabaseItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var databaseItem = ProcessarJsonParametro<Core.Entidade.Database.DatabaseItem>(jsonObjeto["databaseItem"], loginAcessoItem);

                databaseItem = SalvarDatabaseItem(databaseItem);

                var jsonRetorno = new 
                {
                    DatabaseItem = databaseItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirDatabaseItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var databaseItem = ProcessarJsonParametro<Core.Entidade.Database.DatabaseItem>(jsonObjeto["databaseItem"], loginAcessoItem);

                databaseItem = ExcluirDatabaseItem(databaseItem);

                var jsonRetorno = new 
                {
                    DatabaseItem = databaseItem
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
        
        private List<Core.Entidade.Database.DatabaseItem> ObterDatabaseLista()
        {
            var databaseNegocio = new Core.Negocio.Database.DatabaseItem();

            var databaseLista = databaseNegocio.CarregarLista();

            return databaseLista;
        }

        private Core.Entidade.Database.DatabaseItem ObterDatabaseItem(int databaseId)
        {
            var databaseNegocio = new Core.Negocio.Database.DatabaseItem();

            var databaseItem = databaseNegocio.CarregarItem(databaseId);

            return databaseItem;
        }

        private Core.Entidade.Database.DatabaseItem SalvarDatabaseItem(Core.Entidade.Database.DatabaseItem databaseItem)
        {
            var databaseNegocio = new Core.Negocio.Database.DatabaseItem();

            databaseItem = databaseNegocio.SalvarItem(databaseItem);

            return databaseItem;
        }

        private Core.Entidade.Database.DatabaseItem ExcluirDatabaseItem(Core.Entidade.Database.DatabaseItem databaseItem)
        {
            var databaseNegocio = new Core.Negocio.Database.DatabaseItem();

            databaseItem = databaseNegocio.ExcluirItem(databaseItem);

            return databaseItem;
        }
        
        #endregion
    }
}