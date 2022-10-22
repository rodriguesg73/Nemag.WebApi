using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Globalization;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Net.Http.Headers;

namespace Nemag.Auxiliar
{
    public static partial class Util
    {
        #region Requisições

        public static CookieContainer GetCookieContainer(string url)
        {
            var cookieContainer = new CookieContainer();

            var httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                CookieContainer = cookieContainer
            };

            var httpClient = new HttpClient(httpClientHandler)
            {
                Timeout = new TimeSpan(0, 0, 30)
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            var httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;
            _ = httpResponseMessage.Headers;

            var statusCode = (int)httpResponseMessage.StatusCode;

            if (statusCode >= 300 && statusCode <= 399)
            {
                var redirectUri = httpResponseMessage.Headers.Location;

                if (!redirectUri.IsAbsoluteUri)
                {
                    _ = new Uri(httpRequestMessage.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                }
            }

            return cookieContainer;
        }

        #region Get

        public static async Task<T> RequestGetAsync<T>(string url)
        {
            return await RequestGetAsync<T>(url, null, null, null);
        }

        public static async Task<T> RequestGetAsync<T>(string url, HttpContent httpContent)
        {
            return await RequestGetAsync<T>(url, httpContent, null, null);
        }

        public static async Task<T> RequestGetAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> headerLista)
        {
            return await RequestGetAsync<T>(url, httpContent, headerLista, null);
        }

        public static async Task<T> RequestGetAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            try
            {
                if (cookieContainer == null)
                    cookieContainer = new CookieContainer();

                using var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                    CookieContainer = cookieContainer
                };

                using var httpClient = new HttpClient(httpClientHandler)
                {
                    Timeout = new TimeSpan(0, 30, 0)
                };

                if (OperatingSystem.IsWindows())
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                    Content = httpContent
                };

                if (headerLista != null && headerLista.Count > 0)
                {
                    headerLista
                        .ToList()
                        .ForEach(x => httpRequestMessage.Headers.Add(x.Key, x.Value));
                }

                using var httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;

                var statusCode = (int)httpResponseMessage.StatusCode;

                if (statusCode >= 300 && statusCode <= 399)
                {
                    var redirectUri = httpResponseMessage.Headers.Location;

                    if (!redirectUri.IsAbsoluteUri)
                    {
                        redirectUri = new Uri(httpRequestMessage.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                    }

                    return await RequestGetAsync<T>(redirectUri.ToString(), httpContent, headerLista, cookieContainer);
                }

                var requisicaoRetorno = httpResponseMessage.Content.ReadAsStringAsync().Result;

                var retorno = default(T);

                if (typeof(T) == typeof(string))
                {
                    retorno = (T)(object)requisicaoRetorno;
                }
                else
                {
                    retorno = (T)(object)JsonConvert.DeserializeObject<T>(requisicaoRetorno);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO Request Get: " + ex.InnerException == null ? ex.Message : ex.InnerException.Message);

                throw;
            }
        }

        #endregion

        #region Post

        public static async Task<T> RequestPostAsync<T>(string url, object data, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            var parametroConteudo = "=" + JsonConvert.SerializeObject(data);

            var httpContent = new StringContent(parametroConteudo, System.Text.Encoding.Default, "application/x-www-form-urlencoded");

            return await RequestPostAsync<T>(url, httpContent, headerLista, cookieContainer);
        }

        public static async Task<T> RequestPostAsync<T>(string url, Dictionary<string, string> data, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            var httpContent = new FormUrlEncodedContent(data);

            return await RequestPostAsync<T>(url, httpContent, headerLista, cookieContainer);
        }

        public static async Task<T> RequestPostAsync<T>(string url, string stringContent, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            var httpContent = new StringContent(stringContent, System.Text.Encoding.Default, "application/json");

            return await RequestPostAsync<T>(url, httpContent, headerLista, cookieContainer);
        }

        public static async Task<T> RequestPostAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            try
            {
                if (cookieContainer == null)
                    cookieContainer = new CookieContainer();

                var httpClientHandler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                };

                using var httpClient = new HttpClient(httpClientHandler);

                if (OperatingSystem.IsWindows())
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url) { Content = httpContent };

                if (headerLista != null && headerLista.Count > 0)
                {
                    headerLista
                        .ToList()
                        .ForEach(x =>
                        {
                            if (x.Key.Equals("Content-Type"))
                                httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(x.Value);
                            else
                                httpRequestMessage.Headers.Add(x.Key, x.Value);
                        });
                }

                using var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                var statusCode = (int)httpResponseMessage.StatusCode;

                if (statusCode >= 300 && statusCode <= 399)
                {
                    var redirectUri = httpResponseMessage.Headers.Location;

                    if (!redirectUri.IsAbsoluteUri)
                    {
                        redirectUri = new Uri(httpRequestMessage.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                    }

                    return await RequestGetAsync<T>(redirectUri.ToString(), httpContent, null, cookieContainer);
                }

                var requisicaoRetorno = await httpResponseMessage.Content.ReadAsStringAsync();

                var retorno = default(T);

                if (typeof(T) == typeof(string))
                {
                    retorno = (T)(object)requisicaoRetorno;
                }
                else
                {
                    retorno = (T)Activator.CreateInstance(typeof(T));

                    var tratarComoLista = retorno is IList && retorno.GetType().IsGenericType && retorno.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));

                    if (tratarComoLista)
                    {
                        var objetoGenerico = JsonConvert.DeserializeObject<JObject>(requisicaoRetorno);

                        var primeiraPropriedade = objetoGenerico
                            .Properties()
                            .Select(p => p.Name)
                            .FirstOrDefault();

                        retorno = (T)(object)JsonConvert.DeserializeObject<T>(objetoGenerico[primeiraPropriedade]?.ToString());
                    }
                    else
                    {
                        retorno = (T)(object)JsonConvert.DeserializeObject<T>(requisicaoRetorno);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO Request Post: " + ex.InnerException == null ? ex.Message : ex.InnerException.Message);

                throw;
            }
        }

        #endregion

        #region Get

        public static async Task<T> RequestPatchAsync<T>(string url)
        {
            return await RequestPatchAsync<T>(url, null, null, null);
        }

        public static async Task<T> RequestPatchAsync<T>(string url, HttpContent httpContent)
        {
            return await RequestPatchAsync<T>(url, httpContent, null, null);
        }

        public static async Task<T> RequestPatchAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> headerLista)
        {
            return await RequestPatchAsync<T>(url, httpContent, headerLista, null);
        }

        public static async Task<T> RequestPatchAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> headerLista, CookieContainer cookieContainer)
        {
            if (cookieContainer == null)
                cookieContainer = new CookieContainer();

            using var httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                CookieContainer = cookieContainer
            };

            using var httpClient = new HttpClient(httpClientHandler)
            {
                Timeout = new TimeSpan(0, 30, 0)
            };

            if (OperatingSystem.IsWindows())
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Patch,
                Content = httpContent
            };

            if (headerLista != null && headerLista.Count > 0)
            {
                headerLista
                    .ToList()
                    .ForEach(x => httpRequestMessage.Headers.Add(x.Key, x.Value));
            }

            using var httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;

            var statusCode = (int)httpResponseMessage.StatusCode;

            if (statusCode >= 300 && statusCode <= 399)
            {
                var redirectUri = httpResponseMessage.Headers.Location;

                if (!redirectUri.IsAbsoluteUri)
                {
                    redirectUri = new Uri(httpRequestMessage.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                }

                return await RequestPatchAsync<T>(redirectUri.ToString(), httpContent, headerLista, cookieContainer);
            }

            var requisicaoRetorno = httpResponseMessage.Content.ReadAsStringAsync().Result;

            var retorno = default(T);

            if (typeof(T) == typeof(string))
            {
                retorno = (T)(object)requisicaoRetorno;
            }
            else
            {
                retorno = (T)(object)JsonConvert.DeserializeObject<T>(requisicaoRetorno);
            }

            return retorno;
        }

        #endregion

        public static string ObterUrlRedirect(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.AllowAutoRedirect = true;

            try
            {
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                var headerLocation = ex.Response.Headers["Location"]?.ToString();

                if (!string.IsNullOrEmpty(headerLocation))
                    return headerLocation;
            }

            return null;
        }

        #endregion

        #region Métodos Públicos

        public static string CompactarDiretorioItem(string diretorioOrigemUrl, string arquivoDestinoUrl)
        {
            ValidarDiretorio(Path.GetDirectoryName(arquivoDestinoUrl));

            var diretorioOrigemDirectoryInfo = new DirectoryInfo(diretorioOrigemUrl);

            ZipFile.CreateFromDirectory(diretorioOrigemDirectoryInfo.FullName, arquivoDestinoUrl, CompressionLevel.Optimal, false);

            var arquivoDestinoFileInfo = new FileInfo(arquivoDestinoUrl);

            return arquivoDestinoFileInfo.FullName;
        }

        public static List<string> DescompactarArquivoItem(string arquivoOrigemUrl, string diretorioDestinoUrl)
        {
            var zipFile = ZipFile.Open(arquivoOrigemUrl, ZipArchiveMode.Read);

            zipFile.ExtractToDirectory(diretorioDestinoUrl, true);

            var arquivoDestinoLista = Directory.GetFiles(diretorioDestinoUrl)
                .ToList();

            return arquivoDestinoLista;
        }

        public static byte[] ObterByteArrayPorStream(Stream stream)
        {
            try
            {
                stream.Position = 0;
            }
            catch
            {
            }

            byte[] readBuffer = new byte[1024];
            List<byte> outputBytes = new List<byte>();

            int offset = 0;

            while (true)
            {
                int bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else if (bytesRead == readBuffer.Length)
                {
                    outputBytes.AddRange(readBuffer);
                }
                else
                {
                    byte[] tempBuf = new byte[bytesRead];

                    Array.Copy(readBuffer, tempBuf, bytesRead);

                    outputBytes.AddRange(tempBuf);

                    break;
                }

                offset += bytesRead;
            }

            return outputBytes.ToArray();
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static Dictionary<string, string> ObterHtmlElementoLista(string html)
        {
            var parametroLista = new Dictionary<string, string>();

            var tag = "p";

            var attr = "class";

            var regexInput = @"<" + tag + @"[^>]*?" + attr + @"s*=s*[""\']?([^\'"" >]+?)[ \'""][^>]*?>";

            var regexAttr = "(\\S+)=[\"']?((?:.(?![\"']?\\s+(?:\\S+)=|[>\"']))+.)[\"']?";

            var matchLista = Regex.Matches(html, regexInput, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match matchItem in matchLista)
            {
                var attrLista = Regex.Matches(matchItem.Groups[0].Value, regexAttr);

                var attrName = "";

                var attrValue = "";

                foreach (Match attrItem in attrLista)
                {
                    if (attrItem.Groups[1].ToString().Equals("name"))
                        attrName = attrItem.Groups[2].ToString();

                    if (attrItem.Groups[1].ToString().Equals("value"))
                        attrValue = attrItem.Groups[2].ToString();

                    Console.WriteLine("Attr: " + attrName + " = " + attrValue);
                }

                parametroLista.Add(attrName, attrValue);
            }

            return parametroLista;
        }

        public static List<string> ObterHtmlElementoLista(string html, string element, string attribute, string valor = null)
        {
            var parametroLista = new List<string>();

            var regexInput = @"<" + element + "[^>]*?" + attribute + @"s*=s*[""\']?([^\'"" >]+?)[ \'""][^>]*?>(.*?)</" + element + ">";

            if (string.IsNullOrEmpty(attribute))
                regexInput = "<" + element + ".*?>(.*?)<\\/" + element + ">";

            var regexAttr = "(\\S+)=[\"']?((?:.(?![\"']?\\s+(?:\\S+)=|[>\"']))+.)[\"']?";

            var matchLista = Regex.Matches(html, regexInput, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match matchItem in matchLista)
            {
                var attributeLista = Regex.Matches(matchItem.Groups[0].Value, regexAttr);

                if (!string.IsNullOrEmpty(attribute))
                    foreach (Match attributeItem in attributeLista)
                    {
                        if (valor != null && attributeItem.Groups[1].ToString().Equals(attribute) && attributeItem.Groups[2].ToString().Equals(valor))
                            parametroLista.Add(matchItem.Groups[2].ToString());
                    }
                else
                    parametroLista.Add(matchItem.Groups[1].ToString());

            }

            return parametroLista;
        }

        public static string RemoverAcento(this string text)
        {
            StringBuilder stringReturn = new StringBuilder();

            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    stringReturn.Append(letter);
                }
            }

            return stringReturn.ToString();
        }

        public static string RemoverCaracterEspecial(this string text)
        {
            text = Regex.Replace(text, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);

            return text;
        }

        public static string ConverterDataTableParaCSV(DataTable dataTable)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                stringBuilder.Append(dataTable.Columns[i]);

                if (i < dataTable.Columns.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            stringBuilder.Append('\n');

            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        var value = dr[i].ToString();

                        if (value.Contains(','))
                        {
                            value = string.Format("\"{0}\"", value);

                            stringBuilder.Append(value);
                        }
                        else
                        {
                            stringBuilder.Append(dr[i].ToString());
                        }
                    }

                    if (i < dataTable.Columns.Count - 1)
                    {
                        stringBuilder.Append(';');
                    }
                }
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }

        public static string ValidarDiretorio(string diretorioUrl)
        {
            if (!Directory.Exists(diretorioUrl))
                Directory.CreateDirectory(diretorioUrl);

            if (OperatingSystem.IsLinux())
                ExecutarBashSync("sudo chmod 777 -R " + diretorioUrl);

            return diretorioUrl;
        }

        public static string ObterDiretorioAtualUrl()
        {
            var localPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;

            if (string.IsNullOrEmpty(localPath))
                try
                {
                    localPath = new Uri(Assembly.GetCallingAssembly().Location).LocalPath;
                }
                catch (Exception)
                {
                    localPath = string.Empty;
                }

            var directoryName = Path.GetDirectoryName(localPath);

            return directoryName;
        }

        #endregion

        #region Métodos de manipulação de processos 

        public static List<Process> CarregarProcessoAtualLista()
        {
            var processoLista = Process.GetProcesses().ToList();

            return processoLista;
        }

        public static Process ExecutarBashSync(string script)
        {
            script = script.Replace("\"", "\\\"");

            return IniciarProcessoSync("/bin/bash", $"-c \"" + script + "\"");
        }

        public static Process IniciarProcessoSync(string arquivoNome, string argumento)
        {
            return IniciarProcessoSync(arquivoNome, argumento, null);
        }

        public static Process IniciarProcessoSync(string arquivoNome, string argumento, EventHandler callback)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = arquivoNome,
                    Arguments = argumento,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };

            if (callback != null)
                process.Exited += callback;

            process.Start();

            process.WaitForExit();

            return process;
        }

        public static async Task<Process> ExecutarBashAsync(string script)
        {
            script = script.Replace("\"", "\\\"");

            return await IniciarProcessoAsync("/bin/bash", $"-c \"" + script + "\"");
        }

        public static async Task<Process> IniciarProcessoAsync(string arquivoNome)
        {
            return await IniciarProcessoAsync(arquivoNome, string.Empty, null, null, null);
        }

        public static async Task<Process> IniciarProcessoAsync(string arquivoNome, string argumento)
        {
            return await IniciarProcessoAsync(arquivoNome, argumento, null, null, null);
        }

        public static async Task<Process> IniciarProcessoAsync(string arquivoNome, string argumento, EventHandler exitHandler, DataReceivedEventHandler outputHandler, DataReceivedEventHandler errorHandler)
        {
            var diretorioAtualUrl = Path.GetDirectoryName(arquivoNome);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = arquivoNome,
                    Arguments = argumento,
                    WorkingDirectory = diretorioAtualUrl,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };

            return await ExecutarProcessoAsync(process, exitHandler, outputHandler, errorHandler).ConfigureAwait(true);
        }

        private static Task<Process> ExecutarProcessoAsync(Process process, EventHandler exitHandler, DataReceivedEventHandler outputHandler, DataReceivedEventHandler errorHandler)
        {
            var taskCompletionSource = new TaskCompletionSource<Process>();

            process.Exited += exitHandler;

            if (outputHandler != null)
                process.OutputDataReceived += (sender, dataReceivedEventArgs) => outputHandler(sender, dataReceivedEventArgs);

            if (errorHandler != null)
                process.ErrorDataReceived += (sender, dataReceivedEventArgs) => errorHandler(sender, dataReceivedEventArgs);

            var started = process.Start();

            if (!started)
                throw new InvalidOperationException("Could not start process: " + process);

            taskCompletionSource.SetResult(process);

            //process.BeginOutputReadLine();

            return taskCompletionSource.Task;
        }

        public static string ConverterDatabaseNomeParaClasseNome(string colunaNome)
        {
            var palavraTratada = string.Empty;

            var palavraLista = colunaNome
                .Split('_')
                .ToList();

            for (int i = 0; i < palavraLista.Count; i++)
            {
                var palavraItem = palavraLista[i];

                if (palavraItem.Equals("H"))
                    palavraTratada += "Historico";
                else
                    palavraTratada += palavraItem.Substring(0, 1).ToUpper() + palavraItem[1..].ToLower();
            }

            return palavraTratada;
        }

        public static string ConverterClasseNomeParaDatabaseNome(string classeNome)
        {
            if (classeNome.EndsWith("Inicial"))
                classeNome = classeNome.Substring(0, classeNome.Length - "Inicial".Length);
            else if (classeNome.EndsWith("Final"))
                classeNome = classeNome.Substring(0, classeNome.Length - "Final".Length);

            Regex.Matches(classeNome, "[A-Z]")
                .GroupBy(x => x.Value)
                .Select(x => x.First().Value)
                .ToList()
                .ForEach(x => classeNome = classeNome.Replace(x, "_" + x));

            classeNome = classeNome[1..].ToUpper();

            return classeNome;
        }

        #endregion

        #region Classe para conversão de valores de DATA/HORA vazios

        private class MinDateTimeConverter : DateTimeConverterBase
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var valor = reader.Value.ToString();

                if (string.IsNullOrEmpty(valor))
                    return DateTime.MinValue;

                return DateTime.Parse(valor);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DateTime dateTimeValue = (DateTime)value;

                if (dateTimeValue == DateTime.MinValue)
                {
                    writer.WriteNull();

                    return;
                }

                writer.WriteValue(value);
            }
        }

        #endregion
    }
}
