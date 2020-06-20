using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebGenerateFile.Service;

namespace WebGenerateFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("ObterArquivo")]
        public IActionResult ExportCode(string tabelaPlural, string tabelaSigular, string dominio, string controllerAPI, string controllerWeb, string viewIndex
                                , string viewComponentCampos, string viewComponentDelete, string viewComponentLista)
        {
            WebExportTools webExportTools = new WebExportTools();
 
            dominio = System.IO.File.ReadAllText(dominio);
            controllerAPI = System.IO.File.ReadAllText(controllerAPI);
            controllerWeb = System.IO.File.ReadAllText(controllerWeb);
            viewIndex = System.IO.File.ReadAllText(viewIndex);
            viewComponentCampos = System.IO.File.ReadAllText(viewComponentCampos);
            viewComponentDelete = System.IO.File.ReadAllText(viewComponentDelete);
            viewComponentLista = System.IO.File.ReadAllText(viewComponentLista);


            var resultado = webExportTools.GerarArquivo(tabelaPlural, tabelaSigular, dominio, controllerAPI, controllerWeb, viewIndex, viewComponentCampos, viewComponentDelete, viewComponentLista);

            const string contentType = "application/zip";
            HttpContext.Response.ContentType = contentType;
            var result = new FileContentResult(System.IO.File.ReadAllBytes(resultado), contentType)
            {
                FileDownloadName = $"{tabelaPlural}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip"
            };

            webExportTools.LimparArquivos(resultado, tabelaPlural);
            return result;

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
