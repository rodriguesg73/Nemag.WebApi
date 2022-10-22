using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Nemag.WebApi.Model.Input.Arquivo
{
    public class ArquivoUploadModel : _BaseItem
    {
        public IList<IFormFile> FormFileLista { get; set; }
    }
}
