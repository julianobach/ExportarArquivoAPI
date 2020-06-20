using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebGenerateFile.Service
{
    public class WebExportTools
    {
        private string defaultDirectory = @"C:\temp";
        private string defaultDestinationDirectory = @"C:\temp\ZipFile";
        public string GerarArquivo(string tabelaPlural, string tabelaSigular, string dominio, string controllerAPI, string controllerWeb, string viewIndex
                                , string viewComponentCampos, string viewComponentDelete, string viewComponentLista)
        {

            try
            {
                if (!string.IsNullOrEmpty(tabelaPlural) && !string.IsNullOrEmpty(tabelaSigular))
                {
                    string path = $"{defaultDirectory}\\{tabelaPlural}";
                    if (System.IO.Directory.Exists(path))
                        System.IO.Directory.Delete(path, true);

                    System.IO.Directory.CreateDirectory(path);

                    if (!string.IsNullOrEmpty(dominio))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}Dominio");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}Dominio\\{tabelaPlural}.cs", dominio);
                    }

                    if (!string.IsNullOrEmpty(controllerAPI))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}ControllerAPI");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}ControllerAPI\\{tabelaPlural}.cs", controllerAPI);
                    }

                    if (!string.IsNullOrEmpty(controllerWeb))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}ControllerWeb");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}ControllerWeb\\{tabelaPlural}.cs", controllerWeb);
                    }

                    if (!string.IsNullOrEmpty(viewIndex))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}Views\\{tabelaPlural}");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Index.cs", viewIndex);
                    }

                    if (!string.IsNullOrEmpty(viewComponentCampos))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Campos");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Campos\\Default.cshtml", viewComponentCampos);
                    }

                    if (!string.IsNullOrEmpty(viewComponentDelete))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Deletar");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Deletar\\Default.cshtml", viewComponentDelete);
                    }

                    if (!string.IsNullOrEmpty(viewComponentLista))
                    {
                        System.IO.Directory.CreateDirectory($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Lista");
                        System.IO.File.WriteAllText($"{path}\\{tabelaPlural}Views\\{tabelaPlural}\\Components\\Vc{tabelaSigular}Lista\\Default.cshtml", viewComponentLista);
                    }
                    System.IO.Stream stream = new System.IO.MemoryStream();

                    var fileName = $"{tabelaPlural}{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip";
                    var destinationFile = $"{defaultDestinationDirectory}\\{fileName}";
                    ZipFile.CreateFromDirectory(path, destinationFile);


                    

                    return destinationFile;

                }
                else
                {
                    return "Singular ou Plural não informados";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void LimparArquivos(string filePath, string tabelaPlural)
        {
            string path = $"{defaultDirectory}\\{tabelaPlural}";
            if (System.IO.Directory.Exists(path))
                System.IO.Directory.Delete(path, true);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }
    }

}
