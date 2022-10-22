using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Nemag.Core.Negocio.Arquivo
{
    public partial class ArquivoItem
    {
        public Entidade.Arquivo.ArquivoItem PrepararArquivoItem(string arquivoNome, string arquivoTemporariolUrl)
        {
            var diretorioLocalUrl = Path.Combine(DiretorioAtualUrl, ObterConfiguracaoValor("Arquivo:Diretorio:Local:Url"));

            Auxiliar.Util.ValidarDiretorio(diretorioLocalUrl);

            var arquivoGuid = Guid.NewGuid().ToString();

            var arquivoPermamenteUrl = Path.Combine(diretorioLocalUrl, arquivoGuid);

            File.Move(arquivoTemporariolUrl, arquivoPermamenteUrl);

            var arquivoChecksun = ValidarArquivoChecksun(arquivoPermamenteUrl);

            var arquivoItem = new Entidade.Arquivo.ArquivoItem()
            {
                DiretorioLocalUrl = diretorioLocalUrl,

                Checksun = arquivoChecksun,

                Nome = arquivoNome,

                Guid = arquivoGuid
            };

            return arquivoItem;
        }

        public string ValidarArquivoChecksun(Entidade.Arquivo.ArquivoItem arquivoItem)
        {
            var arquivoChecksun = ValidarArquivoChecksun(Path.Combine(arquivoItem.DiretorioLocalUrl, arquivoItem.GuidPrimario));

            return arquivoChecksun;
        }

        public string ValidarArquivoChecksun(string arquivoUrl)
        {
            using var md5 = MD5.Create();

            using var stream = File.OpenRead(arquivoUrl);

            var hash = md5.ComputeHash(stream);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public Entidade.Arquivo.ArquivoItem CompactarArquivoLista(List<Entidade.Arquivo.ArquivoItem> arquivoLista)
        {
            var diretorioTemporarioUrl = Path.Combine(DiretorioAtualUrl, "Temp", Guid.NewGuid().ToString().Replace("-", string.Empty)[12..]);

            Auxiliar.Util.ValidarDiretorio(diretorioTemporarioUrl);

            var diretorioRepositorioUrl = Path.Combine(diretorioTemporarioUrl, "Repo");

            Auxiliar.Util.ValidarDiretorio(diretorioRepositorioUrl);

            arquivoLista
                .ForEach(x =>
                {
                    x = CarregarItem(x.Id);

                    File.Copy(Path.Combine(x.DiretorioLocalUrl, x.GuidPrimario), Path.Combine(diretorioRepositorioUrl, x.Nome));
                });

            var arquivoNome = Guid.NewGuid().ToString().Replace("-", string.Empty)[12..] + ".gz";

            var arquivoDestinoUrl = Path.Combine(diretorioTemporarioUrl, arquivoNome);

            Auxiliar.Util.CompactarDiretorioItem(diretorioRepositorioUrl, arquivoDestinoUrl);

            var arquivoItem = PrepararArquivoItem(arquivoNome, arquivoDestinoUrl);

            arquivoLista
                .ForEach(x => ExcluirItem(x));

            Directory.Delete(diretorioTemporarioUrl, true);

            return arquivoItem;
        }

        public List<Entidade.Arquivo.ArquivoItem> DescompactarArquivoLista(List<Entidade.Arquivo.ArquivoItem> arquivoCompactadoLista)
        {
            var diretorioTemporarioUrl = Path.Combine(DiretorioAtualUrl, "Temp", Guid.NewGuid().ToString().Replace("-", string.Empty)[12..]);

            Auxiliar.Util.ValidarDiretorio(diretorioTemporarioUrl);

            var diretorioRepositorioUrl = Path.Combine(diretorioTemporarioUrl, "Repo");

            Auxiliar.Util.ValidarDiretorio(diretorioRepositorioUrl);

            var arquivoDescompactadoLista = new List<Entidade.Arquivo.ArquivoItem>();

            arquivoCompactadoLista
                .ForEach(x =>
                {
                    x = CarregarItem(x.Id);

                    var arquivoExtensao = Path.GetExtension(x.Nome);

                    if (!new List<string> { ".gz", ".zip" }.Contains(arquivoExtensao))
                        return;

                    var arquivoLocalLista = Auxiliar.Util.DescompactarArquivoItem(Path.Combine(x.DiretorioLocalUrl, x.GuidPrimario), diretorioRepositorioUrl);

                    arquivoLocalLista
                        .ForEach(t =>
                        {
                            var arquivoNome = Path.GetFileNameWithoutExtension(t) + Path.GetExtension(t);

                            var arquivoDescompactadoItem = PrepararArquivoItem(arquivoNome, t);

                            arquivoDescompactadoLista.Add(arquivoDescompactadoItem);
                        });
                });

            Directory.Delete(diretorioTemporarioUrl, true);

            return arquivoDescompactadoLista;
        }
    }
}
