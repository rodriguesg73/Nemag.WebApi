using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.StyledXmlParser.Css.Media;
using System;
using System.Collections.Generic;

namespace Nemag.Auxiliar.Pdf
{
    public class Pdf
    {
        public static string GerarPorHtml(string arquivoUrl, string html)
        {
            PdfWriter writer = new PdfWriter(arquivoUrl);

            PdfDocument pdf = new PdfDocument(writer);

            pdf.SetTagged();

            PageSize pageSize = PageSize.A4.Rotate();

            pdf.SetDefaultPageSize(pageSize);

            ConverterProperties properties = new ConverterProperties();

            MediaDeviceDescription mediaDeviceDescription = new MediaDeviceDescription(MediaType.SCREEN);

            mediaDeviceDescription.SetWidth(pageSize.GetWidth());

            properties.SetMediaDeviceDescription(mediaDeviceDescription);
            
            FontProvider fontProvider = new DefaultFontProvider(true, true, true);

            //FontProgram fontProgram = FontProgramFactory.CreateFont(FONT);

            //fontProvider.AddFont(fontProgram, "Winansi");

            properties.SetFontProvider(fontProvider);

            HtmlConverter.ConvertToPdf(html, pdf, properties);

            return arquivoUrl;
        }

        public static string Unir(string arquivoUrl, List<string> arquivoLista)
        {
            if (arquivoLista == null && arquivoLista.Count.Equals(0))
                throw new Exception("Arquivos Necessários");

            var pdfDocument = new PdfDocument(new PdfReader(arquivoLista[0]), new PdfWriter(arquivoUrl));

            for (int i = 1; i < arquivoLista.Count; i++)
            {
                var pdfDocumentAdicional = new PdfDocument(new PdfReader(arquivoLista[i]));

                var pdfMerger = new PdfMerger(pdfDocument);

                pdfMerger.Merge(pdfDocumentAdicional, 1, pdfDocumentAdicional.GetNumberOfPages());

                pdfDocumentAdicional.Close();
            }

            pdfDocument.Close();

            return arquivoUrl;
        }
    }
}
