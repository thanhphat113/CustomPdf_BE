using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPdf_BE.Helper
{
    public class NameMapping
    {
        public static readonly Dictionary<string, string> name = new()
        {
            { "Họ và tên", "HoTen" },
            { "Ngày sinh", "NgaySinh" },
            { "Giới tính", "GioiTinh" },
            { "Ðiện thoại", "DienThoai" },
            { "Ðịa chỉ", "DiaChi" },
            { "Mã thẻ BHYT", "BHYT" },
            { "Mã nơi ÐKBÐ", "DKBD" },
            { "Hạn SD", "HanSD" },
            { "Mạch", "Mach" },
            { "Nhiệt độ", "NhietDo" },
            { "Huyết áp", "HuyetAp" },
            { "Cân nặng", "CanNang" },
            { "Triệu chứng", "TrieuChung" },
            { "Lý do vào viện", "LyDoVaoVien" },
            { "Chẩn doán", "ChanDoan" },
            { "Phòng khám", "PhongKham" },
            { "Số thứ tự", "SoThuTu" },
            {"Nội dung", "NoiDung"},
            {"Đơn vị tính","DonViTinh"},
            {"Nguồn thanh toán (đồng)","NguonThanhToan"}
        };
    }
}