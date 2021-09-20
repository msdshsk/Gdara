using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Drawing;

using Font = DocumentFormat.OpenXml.Spreadsheet.Font;

using System.Diagnostics;

namespace WorkbookUtility
{
    class Creator
    {

        private SpreadsheetDocument document;
        const int PIXEL_SIZE = 9525;
        private uint startId = 0;
        private int lastSheetId = 1;
        public void Create(Stream stream)
        {
            SpreadsheetDocument document;
            // ファイル名を指定してExcelドキュメントを作成する。
            document = SpreadsheetDocument.Create(
                stream, SpreadsheetDocumentType.Workbook, true
            );

            // Excelドキュメントにワークブックパートを追加する。
            WorkbookPart wbpart = document.AddWorkbookPart();
            wbpart.Workbook = new Workbook();

            // ワークブックパートにプロパティを設定する
            wbpart.Workbook.WorkbookProperties = new WorkbookProperties();
            // ファイルを圧縮しない設定を追加
            wbpart.Workbook.WorkbookProperties.AutoCompressPictures = BooleanValue.FromBoolean(false);

            // ワークブックパートにスタイルパートを追加する。
            // スタイルを追加しないとExcelファイルを開いた際に警告がでる。
            WorkbookStylesPart stylesPart = wbpart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = CreateStylesheet();
            stylesPart.Stylesheet.Save();

            // ワークブックパートにシートコレクションを追加する。
            Sheets sheets = wbpart.Workbook.AppendChild<Sheets>(new Sheets());

            this.document = document;
        }

        private static Stylesheet CreateStylesheet()
        {
            // スタイルシートインスタンス。
            Stylesheet sSheet = new Stylesheet();

            // フォントコレクション。
            Fonts fonts = new Fonts();
            // フォントの作成。
            Font font = new Font();
            // コレクションへフォントを追加する。
            fonts.Append(font);
            // フォント数。
            fonts.Count = UInt32Value.FromUInt32((uint)fonts.ChildElements.Count);

            // 塗りつぶしコレクション。
            Fills fills = new Fills();
            // 塗りつぶしの作成（デフォルト：塗りつぶしなし）。　
            Fill fill = new Fill();
            // 塗りつぶしをコレクションへ追加する。
            fills.Append(fill);
            // 塗りつぶし数。
            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);

            // 罫線コレクション。
            Borders borders = new Borders();
            // 罫線の作成（デフォルト：罫線なし）。
            Border border = new Border();
            // コレクションへ罫線を追加する。
            borders.Append(border);
            // 罫線数。
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);

            CellStyleFormats csFormats = new CellStyleFormats();
            CellFormat format = new CellFormat();
            format.FontId = 0;
            format.FillId = 0;
            format.BorderId = 0;
            csFormats.Append(format);
            csFormats.Count = UInt32Value.FromUInt32((uint)csFormats.ChildElements.Count);

            // 数式コレクションの作成。
            NumberingFormats nFormats = new NumberingFormats();
            // 数式の作成。
            NumberingFormat nFormat = new NumberingFormat();
            nFormats.Append(nFormat);

            // セル書式の作成。
            CellFormats cFormats = new CellFormats();
            format = new CellFormat();
            format.NumberFormatId = 0;  // 上記参照
            format.FontId = 0;          // 上記参照
            format.FillId = 0;          // 上記参照
            format.BorderId = 0;        // 上記参照
            format.FormatId = 0;        // 上記参照
            cFormats.Append(format);    // 上記参照

            cFormats.Count = UInt32Value.FromUInt32((uint)cFormats.ChildElements.Count);

            sSheet.Append(fonts);
            sSheet.Append(fills);
            sSheet.Append(borders);
            sSheet.Append(csFormats);
            sSheet.Append(cFormats);

            CellStyles cStyles = new CellStyles();
            CellStyle cStyle = new CellStyle();
            cStyle.Name = StringValue.FromString("Normal");
            cStyle.FormatId = 0;
            cStyle.BuiltinId = 0;
            cStyles.Append(cStyle);
            cStyles.Count = UInt32Value.FromUInt32((uint)cStyles.ChildElements.Count);
            sSheet.Append(cStyles);

            DifferentialFormats dFormats = new DifferentialFormats();
            dFormats.Count = 0;
            sSheet.Append(dFormats);

            TableStyles tStyles = new TableStyles();
            tStyles.Count = 0;
            tStyles.DefaultTableStyle = StringValue.FromString("TableStyleMedium9");
            tStyles.DefaultPivotStyle = StringValue.FromString("PivotStyleLight16");
            sSheet.Append(tStyles);

            return sSheet;
        }

        public Sheet AddSheet(string sheetName)
        {
            WorksheetPart wspart = document.WorkbookPart.AddNewPart<WorksheetPart>();
            wspart.Worksheet = new Worksheet();

            SheetData sheetData = new SheetData();
            wspart.Worksheet.Append(sheetData);

            // シートコレクションにシートを追加する。
            Sheet sheet = new Sheet();
            sheet.Id = document.WorkbookPart.GetIdOfPart(wspart);
            sheet.SheetId = (uint)lastSheetId;
            lastSheetId++;
            sheet.Name = sheetName;
            document.WorkbookPart.Workbook.Sheets.Append(sheet);

            return sheet;
        }

        public bool SheetExists(string sheetName)
        {
            foreach (Sheet sheet in document.WorkbookPart.Workbook.Descendants<Sheet>())
            {
                if (sheet.Name == sheetName)
                {
                    return true;
                }
            }
            return false;
        }

        public ImageSizeProperties AddImage(Sheet targetSheet, ImageProperties imageProps)
        {
            // https://symfoware.blog.fc2.com/blog-entry-1145.html
            // https://symfoware.blog.fc2.com/blog-entry-1147.html

            // https://github.com/OfficeDev/Open-XML-SDK/releases/tag/v2.5

            WorkbookPart wbpart = document.WorkbookPart;
            WorksheetPart wspart = (WorksheetPart)(wbpart.GetPartById(targetSheet.Id));

            // 画像ファイルを追加
            string sImagePath = imageProps.Path;

            ImagePartType ipt;
            switch (sImagePath.Substring(sImagePath.LastIndexOf('.') + 1).ToLower())
            {
                case "png":
                    ipt = ImagePartType.Png;
                    break;
                case "jpg":
                case "jpeg":
                    ipt = ImagePartType.Jpeg;
                    break;
                case "gif":
                    ipt = ImagePartType.Gif;
                    break;
                default:
                    //throw Exception();
                    ipt = ImagePartType.Png;
                    break;
            }

            DrawingsPart dp;
            ImagePart imgp;
            WorksheetDrawing wsd;

            dp = wspart.DrawingsPart;
            if (dp == null)
            {
                dp = wspart.AddNewPart<DrawingsPart>();
                imgp = dp.AddImagePart(ipt, wspart.GetIdOfPart(dp));
                wsd = new WorksheetDrawing();
            }
            else
            {
                imgp = dp.AddImagePart(ipt);
                dp.CreateRelationshipToPart(imgp);
                wsd = dp.WorksheetDrawing;
            }


            using (FileStream fs = new FileStream(sImagePath, FileMode.Open))
            {
                imgp.FeedData(fs);
            }

            int imageNumber = dp.ImageParts.Count<ImagePart>();
            if (imageNumber == 1)
            {
                Drawing drawing = new Drawing();
                drawing.Id = dp.GetIdOfPart(imgp);
                wspart.Worksheet.Append(drawing);
            }

            NonVisualDrawingProperties nvdp = new NonVisualDrawingProperties();
            // startId += 3;
            nvdp.Id = UInt32Value.FromUInt32((uint)(1024 + imageNumber));
            nvdp.Name = imageProps.Name;
            nvdp.Description = imageProps.Description;
            DocumentFormat.OpenXml.Drawing.PictureLocks picLocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
            picLocks.NoChangeAspect = true;
            picLocks.NoChangeArrowheads = true;
            //picLocks.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            NonVisualPictureDrawingProperties nvpdp = new NonVisualPictureDrawingProperties();
            nvpdp.PictureLocks = picLocks;
            NonVisualPictureProperties nvpp = new NonVisualPictureProperties();
            nvpp.NonVisualDrawingProperties = nvdp;
            nvpp.NonVisualPictureDrawingProperties = nvpdp;

            DocumentFormat.OpenXml.Drawing.Stretch stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
            stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();
            //stretch.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            BlipFill blipFill = new BlipFill();
            DocumentFormat.OpenXml.Drawing.Blip blip = new DocumentFormat.OpenXml.Drawing.Blip();

            blip.Embed = dp.GetIdOfPart(imgp);
            blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
            //blip.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //blip.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            blipFill.Blip = blip;
            blipFill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
            //blipFill.SourceRectangle.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            blipFill.Append(stretch);
            DocumentFormat.OpenXml.Drawing.Transform2D t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
            DocumentFormat.OpenXml.Drawing.Offset offset = new DocumentFormat.OpenXml.Drawing.Offset();
            // 画像の貼り付け位置
            offset.X = imageProps.OffsetX * PIXEL_SIZE;
            offset.Y = imageProps.OffsetY * PIXEL_SIZE;
            t2d.Offset = offset;

            Bitmap bm = new Bitmap(sImagePath);
            ImageSizeProperties resultProps = new ImageSizeProperties();
            //http://en.wikipedia.org/wiki/English_Metric_Unit#DrawingML
            //http://stackoverflow.com/questions/1341930/pixel-to-centimeter
            //http://stackoverflow.com/questions/139655/how-to-convert-pixels-to-points-px-to-pt-in-net-c
            DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();


            ImageSizeProperties resized = resize(bm, imageProps);


            // サイズの縮小をする場合はこれを調整する
            extents.Cx = calcUnit(resized.Width, bm.HorizontalResolution);
            extents.Cy = calcUnit(resized.Height, bm.VerticalResolution);

            resultProps.Width = resized.Width;
            resultProps.Height = resized.Height;

            bm.Dispose();
            t2d.Extents = extents;
            ShapeProperties sp = new ShapeProperties();
            sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
            //t2d.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            sp.Transform2D = t2d;
            DocumentFormat.OpenXml.Drawing.PresetGeometry prstGeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
            //prstGeom.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            prstGeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
            prstGeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();

            sp.Append(prstGeom);
            //DocumentFormat.OpenXml.Drawing.NoFill nofill = new DocumentFormat.OpenXml.Drawing.NoFill();
            //nofill.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            //sp.Append(nofill);
            sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

            DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
            picture.NonVisualPictureProperties = nvpp;
            picture.BlipFill = blipFill;
            picture.ShapeProperties = sp;

            Position pos = new Position();
            pos.X = offset.X;
            pos.Y = offset.Y;
            Extent ext = new Extent();
            ext.Cx = extents.Cx;
            ext.Cy = extents.Cy;
            AbsoluteAnchor anchor = new AbsoluteAnchor();
            anchor.Position = pos;
            anchor.Extent = ext;
            anchor.Append(picture);
            anchor.Append(new ClientData());



            wsd.Append(anchor);
            wsd.Save(dp);
            return resultProps;
        }

        private long calcUnit(int num, float dpi = 72)
        {
            return (long)num * (long)((float)914400 / dpi);
        }

        private ImageSizeProperties resize(Bitmap bm, ImageProperties prop)
        {
            double Width = (double)bm.Width;
            double Height = (double)bm.Height;
            double MaxWidth = (double)prop.MaxWidth;
            double MaxHeight = (double)prop.MaxHeight;

            ImageSizeProperties result = new ImageSizeProperties();

            double h_par = -1;
            double w_par = -1;
            if (Height > MaxHeight && MaxHeight > 0)
            {
                h_par = (MaxHeight / Height);
            }

            if (Width > MaxWidth && MaxWidth > 0)
            {
                w_par = (MaxWidth / Width);
            }
            
            if (h_par == -1 && w_par == -1)
            {
                result.Width = (int)Width;
                result.Height = (int)Height;

                return result;
            }

            if (h_par > w_par)
            {
                result.Width = (int)(Width * h_par);
                result.Height = (int)(Height * h_par);
            }
            else
            {
                result.Width = (int)(Width * w_par);
                result.Height = (int)(Height * w_par);
            }
            return result;
        }

        public SpreadsheetDocument getDocument()
        {
            return this.document;
        }

        public void SaveAs(string savePath)
        {
            document.SaveAs(savePath);
        }

        public void Dispose()
        {
            document.Close();
            document.Dispose();
        }
    }

    class ImageProperties
    {
        public string Path { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;

        public int MaxWidth { get; set; } = 0;
        public int MaxHeight { get; set; } = 0;

        public static bool IsImage(string filePath)
        {
            string[] imageExts = { "png", "jpg", "jpeg", "gif", "bmp" };
            string ext = GetExtension(filePath);
            return imageExts.Contains(ext);
        }

        public static string GetExtension(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf('.') + 1).ToLower();
        }
    }

    class ImageSizeProperties
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}