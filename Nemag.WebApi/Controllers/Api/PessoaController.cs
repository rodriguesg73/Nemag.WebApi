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
    public class PessoaController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarPessoaLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var pessoaLista = ObterPessoaLista();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    PessoaLista = pessoaLista
                });

                return base.ObterActionResult(HttpStatusCode.OK, jsonRetorno);
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult CarregarPessoaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["pessoaId"] == null)
                    throw new ArgumentException("Código do pessoa necessário");

                var pessoaId = Convert.ToInt32(jsonObjeto["pessoaId"].ToString());

                var pessoaItem = ObterPessoaItem(pessoaId);

                var jsonRetorno = new 
                {
                    PessoaItem = pessoaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarPessoaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var pessoaItem = ProcessarJsonParametro<Core.Entidade.Pessoa.PessoaItem>(jsonObjeto["pessoaItem"], loginAcessoItem);

                pessoaItem = SalvarPessoaItem(pessoaItem);

                var jsonRetorno = new 
                {
                    PessoaItem = pessoaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirPessoaItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var pessoaItem = ProcessarJsonParametro<Core.Entidade.Pessoa.PessoaItem>(jsonObjeto["pessoaItem"], loginAcessoItem);

                pessoaItem = ExcluirPessoaItem(pessoaItem);

                var jsonRetorno = new 
                {
                    PessoaItem = pessoaItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }


        [HttpPost]
        public IActionResult CarregarPessoaContatoTipoLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var pessoaContatoTipoLista = ObterPessoaContatoTipoLista();

                var jsonRetorno = new
                {
                    PessoaContatoTipoLista = pessoaContatoTipoLista
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

        private List<Core.Entidade.Pessoa.PessoaItem> ObterPessoaLista()
        {
            var pessoaNegocio = new Core.Negocio.Pessoa.PessoaItem();

            var pessoaLista = pessoaNegocio.CarregarLista();

            return pessoaLista;
        }

        private Core.Entidade.Pessoa.PessoaItem ObterPessoaItem(int pessoaId)
        {
            var pessoaNegocio = new Core.Negocio.Pessoa.PessoaItem();

            var pessoaItem = pessoaNegocio.CarregarItem(pessoaId);

            return pessoaItem;
        }

        private Core.Entidade.Pessoa.PessoaItem SalvarPessoaItem(Core.Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var pessoaNegocio = new Core.Negocio.Pessoa.PessoaItem();

            pessoaItem = pessoaNegocio.SalvarItem(pessoaItem);

            return pessoaItem;
        }

        private Core.Entidade.Pessoa.PessoaItem ExcluirPessoaItem(Core.Entidade.Pessoa.PessoaItem pessoaItem)
        {
            var pessoaNegocio = new Core.Negocio.Pessoa.PessoaItem();

            pessoaItem = pessoaNegocio.ExcluirItem(pessoaItem);

            return pessoaItem;
        }

        private List<Core.Entidade.Pessoa.Contato.Tipo.TipoItem> ObterPessoaContatoTipoLista()
        {
            var pessoaContatoTipoNegocio = new Core.Negocio.Pessoa.Contato.Tipo.TipoItem();

            var pessoaContatoTipoLista = pessoaContatoTipoNegocio.CarregarLista();

            return pessoaContatoTipoLista;
        }

        #endregion
    }
}