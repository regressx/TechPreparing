using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.Orders.Reports
{
    class ExcelReportFactory : IReportFactory
    {
        private readonly string _reportName;
        private MapTreeOnListService<ReportNode> _mapTreeOnListService;
        private ICollection<LevelColor> _colorCollection;
        public ExcelReportFactory(string reportName, MapTreeOnListService<ReportNode> mapTreeOnListService, ICollection<LevelColor> colorCollection)
        {
            _reportName = reportName;
            if (colorCollection == null)
            {
                throw new ArgumentNullException("colorCollection","Отсутствует ссылка на коллекцию цветов состава изделия");
            }

            if (colorCollection == null)
            {
                throw new ArgumentNullException("colorCollection","Отсутствует ссылка на коллекцию цветов состава изделия");
            }

            if (mapTreeOnListService == null)
            {
                throw new ArgumentNullException("mapTreeOnListService","Отсутствует ссылка маппер из дерева на список");
            }

            _colorCollection = colorCollection;
            _mapTreeOnListService = mapTreeOnListService;
        }

        public void Create(ReportNode element)
        {
            IList<ReportNode> myList = _mapTreeOnListService.MapTreeOnList(element);

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(_reportName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylePart.Stylesheet = CreateStyleSheet(_colorCollection);
                stylePart.Stylesheet.Save();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = myList[0].Designation };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row headerRow = new Row();

                headerRow.Append(
                    ConstructCell("№ п/п", CellValues.String, 2),
                    ConstructCell("Обозначение", CellValues.String, 2),
                    ConstructCell("Наименование", CellValues.String, 2),
                    ConstructCell("Первичная применяемость", CellValues.String, 2),
                    ConstructCell("Кол-во по спец.", CellValues.String, 2),
                    ConstructCell("Номер изменения", CellValues.String, 2),
                    ConstructCell("Извещение", CellValues.String, 2),
                    ConstructCell("Примечание", CellValues.String, 2),
                    ConstructCell("Литера", CellValues.String, 2),
                    ConstructCell("Тип объекта", CellValues.String, 2),
                    ConstructCell("Intermech Id", CellValues.String, 2),
                    ConstructCell("Этап ЖЦ", CellValues.String, 2));

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(headerRow);

                for (int i = 0; i < myList.Count; i++)
                {
                    Row row = new Row();
                    foreach (string value in myList[i])
                    {
                        uint style = 0;
                        if (myList[i].TypeId == 1074 || myList[i].TypeId == 1078 ||
                            myList[i].TypeId == 1794 || myList[i].TypeId == 1097 ||
                            myList[i].TypeId == 1159 || myList[i].TypeId == 1052 ||
                            myList[i].TypeId == 1019)
                        {

                            style = (uint)(myList[i].Level + 2);
                        }

                        Cell cell = new Cell()
                        {
                            DataType = new EnumValue<CellValues>(CellValues.String),
                            CellValue = new CellValue(value),
                            StyleIndex = style
                        };
                        row.Append(cell);
                    }
                    sheetData.AppendChild(row);
                }

                worksheetPart.Worksheet.Save();
                document.Close();
            }

        }


        private Fill CreateFill(string hexColorString)
        {
            ForegroundColor foreGroundColor = new ForegroundColor()
            {
                Rgb = new HexBinaryValue(hexColorString)
            };
            PatternFill patternFill = new PatternFill();
            patternFill.ForegroundColor = foreGroundColor;
            patternFill.PatternType = PatternValues.Solid;
            return new Fill(patternFill);
        }

        private Cell ConstructCell(string value, CellValues dataType, uint styleIndex)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }



        private Stylesheet CreateStyleSheet(ICollection<LevelColor> colorCollection)
        {
            Stylesheet styleSheet = null;
            Fonts fonts = null;
            Fills fills = null;
            CellFormats cellFormats = null;
            fonts = new Fonts(
                // Index 0 - documents
                new Font(
                    new FontSize() { Val = 12 },
                    new FontName() { Val = "Times New Roman"}),

                // Index 1 - other
                new Font(new FontSize() { Val = 12 },
                    new FontName() { Val = "Times New Roman" },
                    new Bold()));

            ICollection<Fill> fillArray = new List<Fill>();
            fillArray.Add(new Fill(new PatternFill() { PatternType = PatternValues.None })); // Index 0 - documents
            fillArray.Add(new Fill(new PatternFill() { PatternType = PatternValues.Gray125 })); // Index 1 - default); // Index 0 - documents
           

            foreach (LevelColor colorString in colorCollection)
            {
                fillArray.Add(CreateFill(colorString.HexColorString));
            }

            fills = new Fills(fillArray);

            Borders borders = new Borders(
                new Border(), // index 0 default
                new Border( // index 1 black border
                    new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new DiagonalBorder()));

            ICollection<CellFormat> cellformatsCollection = new List<CellFormat>();
            cellformatsCollection.Add(new CellFormat());
            for (int i = 1; i < colorCollection.Count + 2; i++)
            {
                CellFormat cellFormat = new CellFormat() { FontId = 1, FillId = (uint)i, BorderId = 1, ApplyFill = true, ApplyBorder = true, ApplyFont = true };
                cellformatsCollection.Add(cellFormat);
            }

            cellFormats = new CellFormats(cellformatsCollection);


            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);
            return styleSheet;
        }

    }
}