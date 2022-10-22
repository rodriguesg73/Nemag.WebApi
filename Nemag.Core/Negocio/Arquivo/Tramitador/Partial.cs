using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nemag.Core.Negocio.Arquivo.Tramitador
{
    public partial class TramitadorItem
    {
        #region Métodos Públicos

        public List<string> ExecutarArquivoTramitadorItem(int arquivoTramitadorId)
        {
            var arquivoTramitadorItem = CarregarItem(arquivoTramitadorId);

            return ExecutarArquivoTramitadorItem(arquivoTramitadorItem);
        }

        public List<string> ExecutarArquivoTramitadorItem(Entidade.Arquivo.Tramitador.TramitadorItem arquivoTramitadorItem)
        {
            var arquivoLista = new List<string>();

            var diretorioLocalUrl = Auxiliar.Util.ObterDiretorioAtualUrl();

            var diretorioTemporarioGuid = Guid.NewGuid().ToString();

            var diretorioTemporarioUrl = Auxiliar.Util.ValidarDiretorio(Path.Combine(diretorioLocalUrl, "Temp", diretorioTemporarioGuid));

            var arquivoTramitadorEmailLista = ObterArquivoTramitadorEmailListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            var arquivoTramitadorFtpLista = ObterArquivoTramitadorFtpListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            var arquivoTramitadorDiretorioLista = ObterArquivoTramitadorDiretorioListaPorArquivoTramitadorId(arquivoTramitadorItem.Id);

            ProcessarArquivoTramitadorEmailRecebimentoLista(diretorioTemporarioUrl, arquivoTramitadorEmailLista);

            ProcessarArquivoTramitadorFtpRecebimentoLista(diretorioTemporarioUrl, arquivoTramitadorFtpLista);

            ProcessarArquivoTramitadorDiretorioRecebimentoLista(diretorioTemporarioUrl, arquivoTramitadorDiretorioLista);

            ProcessarArquivoTramitadorEmailEnvioLista(diretorioTemporarioUrl, arquivoTramitadorEmailLista);

            ProcessarArquivoTramitadorFtpEnvioLista(diretorioTemporarioUrl, arquivoTramitadorFtpLista);

            ProcessarArquivoTramitadorDiretorioEnvioLista(diretorioTemporarioUrl, arquivoTramitadorDiretorioLista);

            LimparDiretorioTemporarioUrl(diretorioTemporarioUrl);

            return arquivoLista;
        }

        #endregion

        #region Métodos Privados

        private List<Entidade.Arquivo.Tramitador.Email.EmailItem> ObterArquivoTramitadorEmailListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorEmailNegocio = new Email.EmailItem();

            var arquivoTramitadorEmailLista = arquivoTramitadorEmailNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorEmailLista;
        }

        private List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> ObterArquivoTramitadorFtpListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorFtpNegocio = new Ftp.FtpItem();

            var arquivoTramitadorFtpLista = arquivoTramitadorFtpNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorFtpLista;
        }

        private List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> ObterArquivoTramitadorDiretorioListaPorArquivoTramitadorId(int arquivoTramitadorId)
        {
            var arquivoTramitadorDiretorioNegocio = new Diretorio.DiretorioItem();

            var arquivoTramitadorDiretorioLista = arquivoTramitadorDiretorioNegocio.CarregarListaPorArquivoTramitadorId(arquivoTramitadorId);

            return arquivoTramitadorDiretorioLista;
        }

        private static List<string> TratarFtpArquivoNomeListaDoUnix(List<string> respostaLista)
        {
            var lista = new List<string>();

            foreach (var respostaItem in respostaLista)
            {
                if (string.IsNullOrEmpty(respostaItem))
                    continue;

                var regex = new Regex(@"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

                var match = regex.Match(respostaItem);

                var diretorio = match.Groups[1].Value.Equals("d");

                var arquivoNome = "";

                if (!diretorio)
                {
                    arquivoNome = match.Groups[match.Groups.Count - 1].Value;

                    lista.Add(arquivoNome);
                }
            }

            return lista;
        }

        private static List<string> TratarFtpArquivoNomeListaDoWindows(List<string> respostaLista)
        {
            var lista = new List<string>();

            foreach (var respostaItem in respostaLista)
            {

                if (string.IsNullOrEmpty(respostaItem))
                    continue;

                var item = respostaItem;

                try
                {
                    while (item.IndexOf("  ") > -1)
                        item = item.Replace("  ", " ");

                    var dir = item.Substring(0, 5).Trim();

                    var diretorio = dir.Equals("<dir>", StringComparison.InvariantCultureIgnoreCase);

                    if (item.IndexOf(" ") > -1)
                    {
                        var nomeSplit = item.Split(' ')
                            .ToList();

                        if (nomeSplit.Count > 3)
                            nomeSplit.RemoveRange(0, 3);

                        item = string.Join(" ", nomeSplit);
                    }

                    var arquivoNome = item;

                    if (!diretorio)
                        lista.Add(arquivoNome);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "\n\n" + item, ex);
                }
            }

            return lista;
        }

        #endregion

        #region Métodos Funcionais

        private void ProcessarArquivoTramitadorEmailRecebimentoLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Email.EmailItem> arquivoTramitadorEmailLista)
        {
            arquivoTramitadorEmailLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(1))
                .ToList()
                .ForEach(x =>
                {
                    try
                    {
                        var pop3Client = new Auxiliar.Email.Pop3.Pop3Client();

                        pop3Client.Connect(x.Servidor, x.Porta, x.Porta.Equals(995));

                        pop3Client.Authenticate(x.Usuario, x.Senha);

                        int count = pop3Client.GetMessageCount();

                        if (count == 0)
                            return;

                        for (int i = count; i >= 1; i -= 1)
                        {
                            var message = pop3Client.GetMessage(i);

                            var plainTextPart = message.FindFirstPlainTextVersion();

                            var attachments = message.FindAllAttachments();

                            var rawMessage = Encoding.UTF8.GetString(message.RawMessage);

                            if (attachments.Count > 0)
                            {
                                foreach (var attachment in attachments)
                                {

                                    var arquivoFileInfo = new FileInfo(Path.Combine(diretorioTemporarioUrl, attachment.FileName));

                                    if (!arquivoFileInfo.Directory.Exists)
                                        arquivoFileInfo.Directory.Create();

                                    if (arquivoFileInfo.Exists)
                                        arquivoFileInfo.Delete();

                                    attachment.Save(arquivoFileInfo);

                                    arquivoFileInfo = new FileInfo(Path.Combine(diretorioTemporarioUrl, attachment.FileName));

                                    if (arquivoFileInfo.Length == 0)
                                        arquivoFileInfo.Delete();
                                }
                            }
                            else
                            {
                                while (rawMessage.IndexOf("begin 644") > 0)
                                {
                                    var anexoNome = rawMessage.Substring(rawMessage.IndexOf("begin 644 ") + "begin 644 ".Length);

                                    anexoNome = anexoNome.Substring(0, anexoNome.IndexOf("\r\n"));

                                    var anexoConteudo = rawMessage.Substring(rawMessage.IndexOf("begin 644"));

                                    anexoConteudo = anexoConteudo.Substring(0, anexoConteudo.IndexOf("end") + "end".Length);

                                    if (new Auxiliar.Encoding.UUCodec().DecodeFileFromString(anexoConteudo, diretorioTemporarioUrl))
                                    {
                                        rawMessage = rawMessage.Replace(anexoConteudo, "");
                                    }
                                }
                            }

                            pop3Client.DeleteMessage(i);
                        }

                        pop3Client.Disconnect();
                    }
                    catch (Exception)
                    {

                    }
                });
        }

        private void ProcessarArquivoTramitadorFtpRecebimentoLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> arquivoTramitadorFtpLista)
        {

            arquivoTramitadorFtpLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(1))
                .ToList()
                .ForEach(x =>
                {
                    var ftpCliente = new Auxiliar.Ftp.FtpClient(x.Servidor, x.Usuario, x.Senha);

                    var arquivoOrigemLista = new List<string>(ftpCliente.DirectoryListDetailed(x.DiretorioUrl));

                    if (arquivoOrigemLista == null || arquivoOrigemLista.Count.Equals(0))
                        return;

                    var arquivoTratadoLista = new List<string>();

                    if (arquivoOrigemLista[0].EndsWith(".") || arquivoOrigemLista[0].Contains("-r-"))
                        arquivoTratadoLista = TratarFtpArquivoNomeListaDoUnix(arquivoOrigemLista);
                    else
                        arquivoTratadoLista = TratarFtpArquivoNomeListaDoWindows(arquivoOrigemLista);

                    foreach (var arquivoTratadoItem in arquivoTratadoLista)
                    {
                        try
                        {
                            ftpCliente.Download(x.DiretorioUrl + "/" + arquivoTratadoItem, Path.Combine(diretorioTemporarioUrl, arquivoTratadoItem));

                            ftpCliente.Delete(x.DiretorioUrl + "/" + arquivoTratadoItem);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                    }
                });

        }

        private void ProcessarArquivoTramitadorDiretorioRecebimentoLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> arquivoTramitadorDiretorioLista)
        {
            arquivoTramitadorDiretorioLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(1))
                .ToList()
                .ForEach(x =>
                {
                    var arquivoLista = Directory.GetFiles(x.DiretorioUrl, "*.*", SearchOption.AllDirectories);

                    arquivoLista
                        .ToList()
                        .ForEach(arquivoItem =>
                        {
                            var fileInfo = new FileInfo(arquivoItem);

                            fileInfo.MoveTo(Path.Combine(diretorioTemporarioUrl, fileInfo.Name.ToString() + fileInfo.Extension));
                        });
                });

        }

        private void ProcessarArquivoTramitadorEmailEnvioLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Email.EmailItem> arquivoTramitadorEmailLista)
        {
            var emailRemetente = ObterConfiguracaoValor("Arquivo:Tramitador:Email:Remetente");

            if (string.IsNullOrEmpty(emailRemetente))
                emailRemetente = "sistemas@unishipping.com.br";

            arquivoTramitadorEmailLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(2))
                .ToList()
                .ForEach(x =>
                {
                    var smtpCliente = new Auxiliar.Email.Smtp.SmtpClient(x.Servidor);

                    var arquivoLista = Directory.GetFiles(diretorioTemporarioUrl, "*.*", SearchOption.AllDirectories);

                    arquivoLista
                        .ToList()
                        .ForEach(arquivoItem =>
                        {
                            smtpCliente.SendMail(emailRemetente, new List<string>() { x.Usuario }, false, "Envio EDI - Unimar", string.Empty, new List<string> { arquivoItem }, null);
                        });
                });

        }

        private void ProcessarArquivoTramitadorFtpEnvioLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Ftp.FtpItem> arquivoTramitadorFtpLista)
        {
            arquivoTramitadorFtpLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(2))
                .ToList()
                .ForEach(x =>
                {
                    var arquivoLista = Directory.GetFiles(diretorioTemporarioUrl, "*.*", SearchOption.AllDirectories);

                    arquivoLista
                        .ToList()
                        .ForEach(arquivoItem =>
                        {
                            var fileInfo = new FileInfo(arquivoItem);

                            var ftpClient = new Auxiliar.Ftp.FtpClient(x.Servidor, x.Usuario, x.Senha);

                            var arquivoRemotoUrl = string.Concat(x.DiretorioUrl, fileInfo.Name);

                            ftpClient.Upload(arquivoRemotoUrl, arquivoItem);
                        });
                });
        }

        private void ProcessarArquivoTramitadorDiretorioEnvioLista(string diretorioTemporarioUrl, List<Entidade.Arquivo.Tramitador.Diretorio.DiretorioItem> arquivoTramitadorDiretorioLista)
        {
            arquivoTramitadorDiretorioLista
                .Where(x => x.ArquivoTramitadorAcaoId.Equals(2))
                .ToList()
                .ForEach(x =>
                {
                    var arquivoLista = Directory.GetFiles(diretorioTemporarioUrl, "*.*", SearchOption.AllDirectories);

                    arquivoLista
                        .ToList()
                        .ForEach(arquivoItem =>
                        {
                            var fileInfo = new FileInfo(arquivoItem);

                            fileInfo.CopyTo(Path.Combine(x.DiretorioUrl, fileInfo.Name.ToString() + fileInfo.Extension), true);
                        });
                });
        }

        private void LimparDiretorioTemporarioUrl(string diretorioUrl)
        {
            var arquivoLista = Directory.GetFiles(diretorioUrl, "*.*", SearchOption.AllDirectories)
                .ToList();

            arquivoLista
                .ForEach(x =>
                {
                    File.Delete(x);
                });

            var diretorioLista = new DirectoryInfo(diretorioUrl).GetDirectories("*", SearchOption.AllDirectories)
                .ToList();

            diretorioLista
                .ForEach(x =>
                {
                    Directory.Delete(x.FullName);
                });

            Directory.Delete(diretorioUrl);
        }

        #endregion
    }
}
