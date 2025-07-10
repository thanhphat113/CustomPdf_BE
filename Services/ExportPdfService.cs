using System.Runtime.CompilerServices;
using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Helper;
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
        private static readonly float _spacingDot = 5;
        private static readonly float _spacingDotY = 5;
        private static readonly float _spacingElementAndValue = 10;
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
            var elements = _mapper.Map<List<ThuocTinhDTO>>(responseElements.Data);
            var formatToDraw = DataConverter.FormatToDrawPdf(elements);

            var data = new Demo { HoTen = "Nguyễn Văn A", NgaySinh = "1990-01-01", GioiTinh = "Nam", DienThoai = "0912345678", DiaChi = "Hà Nội", BHYT = "DN123456789", DKBD = "01", HanSD = "2026-12-31", Mach = "78", NhietDo = "36.5", HuyetAp = "120/80", CanNang = "65", TrieuChung = "Sốt nhẹ", LyDoVaoVien = "Khám sức khỏe", ChanDoan = "Cảm cúm", PhongKham = "PK1", SoThuTu = 1 };

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
                        col.Item().Height(UnitConverter.PxToPoints(maxY)).Canvas((canvas, size) =>
                        {
                            var borderPaint = new SKPaint
                            {
                                Style = SKPaintStyle.Stroke, // viền
                                StrokeWidth = 1,
                                Color = SKColors.Black,
                                IsAntialias = true
                            };

                            // Vẽ viền xung quanh toàn bộ vùng canvas được cấp
                            canvas.DrawRect(0, 0, size.Width, size.Height, borderPaint);
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

    }
}