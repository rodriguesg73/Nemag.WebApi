using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Nemag.FrontEnd.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginIndex LoginIndex { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var requisicaoParametroItem = new
            {
                usuario = LoginIndex.Usuario,

                senha = LoginIndex.Senha
            };

            var apiUrl = Global.ObterApiUrl(Request);

            var jsonObjeto = Auxiliar.Util.RequestPostAsync<JObject>(apiUrl + "/Api/Login/EfetuarLogin", requisicaoParametroItem, null, null).Result;

            var loginItem = jsonObjeto["LoginItem"]?.ToString();

            var loginToken = jsonObjeto["LoginAcessoItem"]?["Token"]?.ToString();

            if (string.IsNullOrEmpty(loginToken)) {
                var mensagem = jsonObjeto["Message"]?.ToString();

                return new UnauthorizedObjectResult(new { StatusCode = 401, Value = mensagem });
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Delete("loginToken");

            Response.Cookies.Delete("loginItem");

            Response.Cookies.Append("loginToken", loginToken, cookieOptions);

            Response.Cookies.Append("loginItem", WebUtility.UrlEncode(loginItem), cookieOptions);

            return Page();
        }
    }

    public class LoginIndex
    {
        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string Token { get; set; }
    }
}
