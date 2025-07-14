using System.Runtime.CompilerServices;
using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Helper;
using CustomPdf_BE.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace CustomPdf_BE.Services
{
    public interface IExportPdfService
    {
        Task<byte[]> ExportPhieuDKKhamBenh();
    }
    public class ExportPdfService : IExportPdfService
    {
        private static readonly int _extraHeight = 20;
        private static readonly float _extraHeightForTable = 0;
        private static readonly float _spacingDot = 5;
        private static readonly float _spacingDotY = 5;
        private static readonly float _heightCell = 25f;
        private static readonly float _spacingElementAndValue = 10;
        private static readonly float _paddingLabelInCell = 5;
        private readonly IThuocTinhService _tt;
        private readonly IMauPdfService _pdf;
        private readonly IMapper _mapper;
        public ExportPdfService(IMapper mapper, IThuocTinhService tt, IMauPdfService pdf)
        {
            _tt = tt;
            _pdf = pdf;
            _mapper = mapper;
        }
        public async Task<byte[]> ExportPhieuDKKhamBenh()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var responsePdf = await _pdf.GetById(1);
            var mauPdf = responsePdf.Data;
            var width = mauPdf.Rong;
            var height = mauPdf.Dai;

            var responseElements = await _tt.GetAllByPdfId(1);
            var responseTables = await _tt.GetTablesByPdfId(1);

            var tables = _mapper.Map<List<TableDTO>>(responseTables.Data);
            var elements = _mapper.Map<List<ThuocTinhDTO>>(responseElements.Data);
            var formatToDraw = DataConverter.FormatToDrawPdf(elements);


            // Data demo
            var data = new Demo { HoTen = "Nguyễn Văn A", NgaySinh = "1990-01-01", GioiTinh = "Nam", DienThoai = "0912345678", DiaChi = "Hà Nội", BHYT = "DN123456789", DKBD = "01", HanSD = "2026-12-31", Mach = "78", NhietDo = "36.5", HuyetAp = "120/80", CanNang = "65", TrieuChung = "Sốt nhẹ", LyDoVaoVien = "Khám sức khỏe", ChanDoan = "Cảm cúm", PhongKham = "PK1", SoThuTu = 1 };

            var tableDemoList = new List<TableDemo>
            {
                new TableDemo { NoiDung = "Dịch vụ A", DonViTinh = "Lần", NguonThanhToan = "Bảo hiểm" },
                new TableDemo { NoiDung = "Dịch vụ B", DonViTinh = "Lần", NguonThanhToan = "Tự chi trả" },
                new TableDemo { NoiDung = "Xét nghiệm máu", DonViTinh = "Lượt", NguonThanhToan = "Bảo hiểm" },
                new TableDemo { NoiDung = "Chụp X-Quang", DonViTinh = "Lượt", NguonThanhToan = "Bảo hiểm" },
                new TableDemo { NoiDung = "Siêu âm", DonViTinh = "Lần", NguonThanhToan = "Tự chi trả" },
                new TableDemo { NoiDung = "Khám tổng quát", DonViTinh = "Lần", NguonThanhToan = "Bảo hiểm" },
                new TableDemo { NoiDung = "Đo điện tim", DonViTinh = "Lần", NguonThanhToan = "Tự chi trả" },
                new TableDemo { NoiDung = "Chụp MRI", DonViTinh = "Lượt", NguonThanhToan = "Bảo hiểm" },
                new TableDemo { NoiDung = "Nội soi", DonViTinh = "Lần", NguonThanhToan = "Tự chi trả" },
                new TableDemo { NoiDung = "Tiêm vaccine", DonViTinh = "Mũi", NguonThanhToan = "Bảo hiểm" }
            };


            var bytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(width, height, Unit.Millimetre);
                    page.Margin(10, Unit.Millimetre);
                    float maxY = 0;
                    foreach (var e in elements)
                    {
                        if (e.Y > maxY)
                            maxY = (float)e.Y;
                    }

                    page.Content().Column(col =>
                    {
                        col.Item().Canvas((canvas, size) =>
                        {
                            var borderPaint = new SKPaint
                            {
                                Style = SKPaintStyle.Stroke,
                                StrokeWidth = 1,
                                Color = SKColors.Black,
                                IsAntialias = true
                            };

                            foreach (var item in tables)
                            {
                                float currentY;

                                // Vẽ header
                                var columnPositions = DrawHeader(canvas, item.Cots, borderPaint, out currentY);

                                // Vẽ data
                                foreach (var row in tableDemoList)
                                {
                                    DrawRow(canvas, row, columnPositions, ref currentY, borderPaint);
                                }
                            }

                            foreach (var list in formatToDraw)
                            {
                                int length = list.Count;
                                for (int i = 0; i < length; i++)
                                {
                                    var item = list[i];
                                    float x = UnitConverter.PxToPoints(list[i].X + _extraHeight);
                                    float y = UnitConverter.PxToPoints(list[i].Y + _extraHeight);
                                    string noiDung = list[i].NoiDung;
                                    string value = "";

                                    var paintForElement = GetPaintForText(item.Mau, item.FontSize, item.InDam, item.Nghieng);
                                    var paintForValue = GetPaintForText(item.MauGiaTri, item.FontSizeGiaTri, item.InDamGiaTri, item.InNghiengGiaTri);

                                    if (NameMapping.name.TryGetValue(noiDung, out string propertyName))
                                    {
                                        var prop = typeof(Demo).GetProperty(propertyName);
                                        if (prop != null)
                                        {
                                            value = prop.GetValue(data)?.ToString();
                                        }
                                    }

                                    DrawLabeledText(canvas, noiDung, value, item.IdLoai, x, y, paintForElement, paintForValue, item.GachChan);

                                    float xStart = x + paintForElement.MeasureText(noiDung) + _spacingDot;
                                    float xEnd = (i + 1 < length) ? (UnitConverter.PxToPoints(list[i + 1].X + _extraHeight) - _spacingDot) : size.Width;

                                    DrawDot(canvas, xStart, xEnd, y + _spacingDotY, paintForElement, item.Dot.Visible, item.Dot.Width);
                                }
                            }
                        });
                    });
                });
            }).GeneratePdf();

            return bytes;
        }

        private static SKPaint GetPaintForText(string mau, int fontSize, bool inDam = false, bool inNghieng = false)
        {
            var fontStyle = (inDam, inNghieng) switch
            {
                (true, true) => SKFontStyle.BoldItalic,
                (true, false) => SKFontStyle.Bold,
                (false, true) => SKFontStyle.Italic,
                _ => SKFontStyle.Normal
            };

            var paint = new SKPaint
            {
                Color = SKColor.Parse(mau),
                TextSize = fontSize,
                Typeface = SKTypeface.FromFamilyName("Arial", fontStyle),
                IsAntialias = true
            };

            return paint;
        }

        private static void DrawLabeledText(SKCanvas canvas, string noiDung, string? giaTri, int idLoai, float x, float y, SKPaint paintForElement, SKPaint paintForValue, bool gachChan = false)
        {
            string textToDraw = idLoai == 2 ? $"{noiDung}:" : noiDung;
            string value = giaTri ?? "";
            float textWidth = paintForElement.MeasureText(noiDung);
            var xValue = x + textWidth + _spacingElementAndValue;

            canvas.DrawText(textToDraw, x, y, paintForElement);
            canvas.DrawText(value, xValue, y, paintForValue);

            if (gachChan)
            {
                float underlineY = y + UnitConverter.PxToPoints(2);

                canvas.DrawLine(
                    x, underlineY,
                    x + textWidth, underlineY,
                    new SKPaint
                    {
                        Color = paintForElement.Color,
                        StrokeWidth = 1
                    }
                );
            }
        }

        private static void DrawDot(SKCanvas canvas, float xStart, float xEnd, float y, SKPaint paint, bool haveDot, int width = 0)
        {
            if (!haveDot) return;
            var dotPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = paint.TextSize
            };

            var endPosition = width == 0 ? xEnd : width;

            float dotSpacing = paint.MeasureText(".") + 1f;
            for (float dotX = xStart; dotX < endPosition; dotX += dotSpacing)
            {
                canvas.DrawText(".", dotX, y, dotPaint);
            }
        }

        private static float DrawLabelCell(SKCanvas canvas, float x, float y, float width, float height, string label, SKPaint paintBorder, bool isHeader = false, string align = "center")
        {
            var backgroundPaint = new SKPaint
            {
                Color = SKColor.Parse("#f8f8f8"),
                Style = SKPaintStyle.Fill
            };

            var paintLabel = GetPaintForText("#000000", 13, isHeader ? true : false);


            if (isHeader) canvas.DrawRect(x, y, width, height, backgroundPaint);
            canvas.DrawRect(x, y, width, height, paintBorder);


            var metrics = paintLabel.FontMetrics;
            var textHeight = metrics.Descent - metrics.Ascent;

            var yText = y + (height - textHeight) / 2 - metrics.Ascent;
            var xText = x + _paddingLabelInCell;

            switch (align)
            {
                case "center":
                    var halfWidth = width / 2;
                    var halfText = paintLabel.MeasureText(label) / 2;
                    xText = x + halfWidth - halfText;
                    break;
                case "right":
                    var textWidth = paintLabel.MeasureText(label);
                    xText = x + width - textWidth - _paddingLabelInCell;
                    break;
                default:
                    break;
            }

            canvas.DrawText(label, xText, yText, paintLabel);

            return y + height;
        }

        private static Dictionary<string, (float x, float width)> DrawHeader(SKCanvas canvas, List<CotDTO> columns, SKPaint borderPaint, out float headerHeight)
        {
            headerHeight = 0;
            var columnPositions = new Dictionary<string, (float x, float width)>();

            // Vẽ header
            foreach (var col in columns)
            {
                float y = UnitConverter.PxToPoints(col.Y ?? 0f) + _extraHeightForTable;
                float x = UnitConverter.PxToPoints(col.X ?? 0f);
                float rong = UnitConverter.PxToPoints(col.Rong ?? 0f);
                var heightCell = DrawLabelCell(canvas, x, y, rong, _heightCell, col.TenCot, borderPaint, true);
                if (heightCell > headerHeight) headerHeight = heightCell;
                columnPositions[col.TenCot] = (x, rong);
            }

            return columnPositions;
        }

        private void DrawRow(SKCanvas canvas, TableDemo item, Dictionary<string, (float x, float width)> columnPositions, ref float currentY, SKPaint borderPaint)
        {
            foreach (var key in columnPositions.Keys)
            {
                string value = "";
                var (x, width) = columnPositions[key];
                if (NameMapping.name.TryGetValue(key, out string propertyName))
                {
                    var prop = item.GetType().GetProperty(propertyName);
                    if (prop != null)
                    {
                        value = prop.GetValue(item)?.ToString();
                    }
                }

                DrawLabelCell(canvas, x, currentY, width, _heightCell, value, borderPaint);
            }

            currentY += _heightCell;
        }
    }
}