using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using openXmlDrawing = DocumentFormat.OpenXml.Drawing;
using openXmlDrawingSpreadsheet = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace Nemag.Auxiliar.Excel
{
    public static class Excel
    {
        #region Métodos Publicos

        public static void ExportarDataSet(string arquivoDestinoUrl, DataSet dataSet)
        {
            using var spreadsheetDocument = SpreadsheetDocument.Create(arquivoDestinoUrl, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadsheetDocument.AddWorkbookPart();

            spreadsheetDocument.WorkbookPart.Workbook = new Workbook
            {
                Sheets = new Sheets()
            };

            var workbookStylesPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorkbookStylesPart>();

            workbookStylesPart.Stylesheet = new Stylesheet
            {
                Fonts = new Fonts
                {
                    Count = 2
                }
            };

            workbookStylesPart.Stylesheet.Fonts.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Font());

            workbookStylesPart.Stylesheet.Fonts.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Font()
            {
                Bold = new Bold()
            });

            workbookStylesPart.Stylesheet.Fills = new Fills
            {
                Count = 2
            };

            workbookStylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel

            workbookStylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel

            workbookStylesPart.Stylesheet.Borders = new Borders(
                new Border(),
                new Border(
                    new LeftBorder(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = new HexBinaryValue() { Value = "AAAAAA" } }) { Style = BorderStyleValues.Dotted },
                    new RightBorder(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = new HexBinaryValue() { Value = "AAAAAA" } }) { Style = BorderStyleValues.Dotted },
                    new TopBorder(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = new HexBinaryValue() { Value = "AAAAAA" } }) { Style = BorderStyleValues.Dotted },
                    new BottomBorder(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = new HexBinaryValue() { Value = "AAAAAA" } }) { Style = BorderStyleValues.Dotted },
                    new DiagonalBorder()
                )
            )
            {
                Count = 2
            };

            workbookStylesPart.Stylesheet.Borders.AppendChild(new Border());

            // blank cell format list
            workbookStylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats
            {
                Count = 1
            };

            workbookStylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());

            workbookStylesPart.Stylesheet.CellFormats = new CellFormats
            {
                Count = 3
            };

            workbookStylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());

            workbookStylesPart.Stylesheet.CellFormats
                .AppendChild(new CellFormat
                {
                    FormatId = 0,
                    FontId = 0,
                    BorderId = 1,
                    FillId = 0,
                    ApplyBorder = true
                })
                .AppendChild(new Alignment
                {
                    Horizontal = HorizontalAlignmentValues.Center
                });

            workbookStylesPart.Stylesheet.CellFormats
                .AppendChild(new CellFormat
                {
                    FormatId = 0,
                    FontId = 1,
                    BorderId = 1,
                    FillId = 0,
                    ApplyBorder = true
                })
                .AppendChild(new Alignment
                {
                    Horizontal = HorizontalAlignmentValues.Center
                });

            workbookStylesPart.Stylesheet.Save();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                AdicionarWorksheet(spreadsheetDocument, dataTable);
            }
        }

        public static void ExportarDataTable(string arquivoDestinoUrl, DataTable dataTable)
        {
            using var spreadsheetDocument = SpreadsheetDocument.Create(arquivoDestinoUrl, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadsheetDocument.AddWorkbookPart();

            spreadsheetDocument.WorkbookPart.Workbook = new Workbook
            {
                Sheets = new Sheets()
            };

            AdicionarWorksheet(spreadsheetDocument, dataTable);
        }

        #endregion

        #region Métodos Privados

        private static CellValues IdentificarCellValue(DataColumn dataColumn)
        {
            if (dataColumn.DataType == typeof(int))
                return CellValues.Number;

            return CellValues.String;
        }

        private static void AdicionarWorksheet(SpreadsheetDocument spreadsheetDocument, DataTable dataTable)
        {
            var worksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();

            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>();

            var relationshipId = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);

            var sheetId = (uint)1;

            if (sheets.Elements<Sheet>().Any())
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;

            var sheet = new Sheet()
            {
                Id = relationshipId,

                SheetId = sheetId,

                Name = dataTable.TableName
            };

            sheets.Append(sheet);

            for (int i = 0; i < 4; i++)
                sheetData.AppendChild(new Row());

            var headerRow = sheetData.AppendChild(new Row()); ;

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                var cell = new Cell()
                {
                    DataType = CellValues.String,

                    CellValue = new CellValue(dataColumn.ColumnName),

                    StyleIndex = 2
                };

                headerRow.AppendChild(cell);
            }

            for (int r = 0; r < dataTable.Rows.Count; r++)
            {
                var dataRow = dataTable.Rows[r];

                var row = sheetData.AppendChild(new Row());

                for (int c = 0; c < dataTable.Columns.Count; c++)
                {
                    var dataColumn = dataTable.Columns[c];

                    var dataType = IdentificarCellValue(dataColumn);

                    var dataRowValue = dataRow[dataColumn.ColumnName].ToString();

                    var cellValue = new CellValue(dataRowValue);

                    row.AppendChild(new Cell()
                    {
                        CellValue = new CellValue(dataRowValue),

                        DataType = dataType,

                        StyleIndex = 1
                    });
                }
            }

            var imageFileName = ObterDiretorioAtualUrl() + "\\Template\\Lineup\\Sac\\lineup-logo-sac.png";

            using var imageStream = new FileStream(imageFileName, FileMode.Open);

            // We need the image stream more than once, thus we create a memory copy
            var imageMemStream = new MemoryStream();

            imageStream.Position = 0;
            imageStream.CopyTo(imageMemStream);
            imageStream.Position = 0;

            var drawingsPart = worksheetPart.DrawingsPart;
            if (drawingsPart == null)
                drawingsPart = worksheetPart.AddNewPart<DrawingsPart>();

            if (!worksheetPart.Worksheet.ChildElements.OfType<Drawing>().Any())
            {
                worksheetPart.Worksheet.Append(new Drawing { Id = worksheetPart.GetIdOfPart(drawingsPart) });
            }

            if (drawingsPart.WorksheetDrawing == null)
            {
                drawingsPart.WorksheetDrawing = new openXmlDrawingSpreadsheet.WorksheetDrawing();
            }

            var worksheetDrawing = drawingsPart.WorksheetDrawing;

            var bm = new Bitmap(imageMemStream);
            var imagePart = drawingsPart.AddImagePart(GetImagePartTypeByBitmap(bm));
            imagePart.FeedData(imageStream);

            openXmlDrawing.Extents extents = new openXmlDrawing.Extents();
            var extentsCx = bm.Width * (long)(914400 / bm.HorizontalResolution);
            var extentsCy = bm.Height * (long)(914400 / bm.VerticalResolution);
            bm.Dispose();

            var colOffset = 0;
            var rowOffset = 0;

            var nvps = worksheetDrawing.Descendants<openXmlDrawingSpreadsheet.NonVisualDrawingProperties>();
            var nvpId = nvps.Any()
                ? (UInt32Value)worksheetDrawing.Descendants<openXmlDrawingSpreadsheet.NonVisualDrawingProperties>().Max(p => p.Id.Value) + 1
                : 1U;

            var oneCellAnchor = new openXmlDrawingSpreadsheet.OneCellAnchor(
                new openXmlDrawingSpreadsheet.FromMarker
                {
                    ColumnId = new openXmlDrawingSpreadsheet.ColumnId((0).ToString()),

                    RowId = new openXmlDrawingSpreadsheet.RowId((0).ToString()),
                    
                    ColumnOffset = new openXmlDrawingSpreadsheet.ColumnOffset(colOffset.ToString()),
                   
                    RowOffset = new openXmlDrawingSpreadsheet.RowOffset(rowOffset.ToString())
                },
                new openXmlDrawingSpreadsheet.Extent { Cx = extentsCx, Cy = extentsCy },
                new openXmlDrawingSpreadsheet.Picture(
                    new openXmlDrawingSpreadsheet.NonVisualPictureProperties(
                        new openXmlDrawingSpreadsheet.NonVisualDrawingProperties { Id = nvpId, Name = "Picture " + nvpId, Description = "Image" },
                        new openXmlDrawingSpreadsheet.NonVisualPictureDrawingProperties(new openXmlDrawing.PictureLocks { NoChangeAspect = true })
                    ),
                    new openXmlDrawingSpreadsheet.BlipFill(
                        new openXmlDrawing.Blip { Embed = drawingsPart.GetIdOfPart(imagePart), CompressionState = openXmlDrawing.BlipCompressionValues.Print },
                        new openXmlDrawing.Stretch(new openXmlDrawing.FillRectangle())
                    ),
                    new openXmlDrawingSpreadsheet.ShapeProperties(
                        new openXmlDrawing.Transform2D(
                            new openXmlDrawing.Offset { X = 0, Y = 0 },
                            new openXmlDrawing.Extents { Cx = extentsCx, Cy = extentsCy }
                        ),
                        new openXmlDrawing.PresetGeometry { Preset = openXmlDrawing.ShapeTypeValues.Rectangle }
                    )
                ),
                new openXmlDrawingSpreadsheet.ClientData()
            );

            worksheetDrawing.Append(oneCellAnchor);
        }

        public static ImagePartType GetImagePartTypeByBitmap(Bitmap image)
        {
            if (ImageFormat.Bmp.Equals(image.RawFormat))
                return ImagePartType.Bmp;
            else if (ImageFormat.Gif.Equals(image.RawFormat))
                return ImagePartType.Gif;
            else if (ImageFormat.Png.Equals(image.RawFormat))
                return ImagePartType.Png;
            else if (ImageFormat.Tiff.Equals(image.RawFormat))
                return ImagePartType.Tiff;
            else if (ImageFormat.Icon.Equals(image.RawFormat))
                return ImagePartType.Icon;
            else if (ImageFormat.Jpeg.Equals(image.RawFormat))
                return ImagePartType.Jpeg;
            else if (ImageFormat.Emf.Equals(image.RawFormat))
                return ImagePartType.Emf;
            else if (ImageFormat.Wmf.Equals(image.RawFormat))
                return ImagePartType.Wmf;
            else
                throw new Exception("Image type could not be determined.");
        }

        private static string ObterDiretorioAtualUrl()
        {
            var localPath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;

            var directoryName = Path.GetDirectoryName(localPath);

            //var codeBaseUrl = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

            return directoryName;
        }

        #endregion
    }
}
