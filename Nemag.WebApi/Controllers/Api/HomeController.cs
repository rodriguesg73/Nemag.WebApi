using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Transactions;

namespace Nemag.WebApi.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : _BaseController
    {
        [HttpGet, HttpPost]
        public IActionResult Index()
        {
            var jsonRetorno = JsonConvert.SerializeObject(new
            {
                Valor = "teste"
            });

            return ObterActionResult(HttpStatusCode.OK, jsonRetorno);
        }

    }
}