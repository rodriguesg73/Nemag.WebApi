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
    public partial class ClienteController : _BaseController
    {
        #region Requests

        [HttpPost]
        public IActionResult CarregarClienteLista([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var clienteLista = ObterClienteLista();

                var tituloDicionarioItem = ObterTituloDicionarioItem<Core.Entidade.Cliente.ClienteItem>();

                var jsonRetorno = JsonConvert.SerializeObject(new 
                {
                    ClienteLista = clienteLista,
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
        public IActionResult CarregarClienteItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                if (jsonObjeto["clienteId"] == null)
                    throw new ArgumentException("Código do cliente necessário");

                var clienteId = Convert.ToInt32(jsonObjeto["clienteId"].ToString());

                var clienteItem = ObterClienteItem(clienteId);

                var pessoaContatoLista = ObterPessoaContatoListaPorPessoaId(clienteItem.PessoaId);

                var jsonRetorno = new 
                {
                    ClienteItem = clienteItem,
                    PessoaContatoLista = pessoaContatoLista,
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult SalvarClienteItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var clienteItem = ProcessarJsonParametro<Core.Entidade.Cliente.ClienteItem>(jsonObjeto["clienteItem"], loginAcessoItem);
                
                var pessoaContatoLista = ProcessarJsonParametro<List<Core.Entidade.Pessoa.Contato.ContatoItem>>(jsonObjeto["pessoaContatoLista"], loginAcessoItem);

                clienteItem = SalvarClienteItem(clienteItem);

                pessoaContatoLista = AtualizarPessoaContatoLista(new Core.Entidade.Pessoa.PessoaItem() { Id = clienteItem.PessoaId }, pessoaContatoLista);

                var jsonRetorno = new 
                {
                    ClienteItem = clienteItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]
        public IActionResult ExcluirClienteItem([FromForm] string parametroConteudo)
        {
            try
            {
                var loginAcessoItem = ValidarLoginAcessoItem(parametroConteudo);

                var jsonObjeto = JObject.Parse(parametroConteudo);

                var clienteItem = ProcessarJsonParametro<Core.Entidade.Cliente.ClienteItem>(jsonObjeto["clienteItem"], loginAcessoItem);

                clienteItem = ExcluirClienteItem(clienteItem);

                var jsonRetorno = new 
                {
                    ClienteItem = clienteItem
                };

                return base.ObterActionResult(HttpStatusCode.OK, JsonConvert.SerializeObject(jsonRetorno));
            }
            catch (Exception ex)
            {
                return base.ObterActionResult(ex, parametroConteudo);
            }
        }

        [HttpPost]


        #endregion

        #region Métodos Privados

        private List<Core.Entidade.Cliente.ClienteItem> ObterClienteLista()
        {
            var clienteNegocio = new Core.Negocio.Cliente.ClienteItem();

            var clienteLista = clienteNegocio.CarregarLista();

            return clienteLista;
        }

        private Core.Entidade.Cliente.ClienteItem ObterClienteItem(int clienteId)
        {
            var clienteNegocio = new Core.Negocio.Cliente.ClienteItem();

            var clienteItem = clienteNegocio.CarregarItem(clienteId);

            return clienteItem;
        }

        private Core.Entidade.Cliente.ClienteItem SalvarClienteItem(Core.Entidade.Cliente.ClienteItem clienteItem)
        {
            var clienteNegocio = new Core.Negocio.Cliente.ClienteItem();

            clienteItem = clienteNegocio.SalvarItem(clienteItem);

            return clienteItem;
        }

        private Core.Entidade.Cliente.ClienteItem ExcluirClienteItem(Core.Entidade.Cliente.ClienteItem clienteItem)
        {
            var clienteNegocio = new Core.Negocio.Cliente.ClienteItem();

            clienteItem = clienteNegocio.ExcluirItem(clienteItem);

            return clienteItem;
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