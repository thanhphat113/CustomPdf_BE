using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Models;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ThuocTinhMau, ThuocTinhDTO>()
			.ForMember(dest => dest.TenLoai, opt => opt.MapFrom(src => src.IdThuocTinhNavigation.IdLoaiNavigation.TenLoai))
			.ForMember(dest => dest.IdLoai, opt => opt.MapFrom(src => src.IdThuocTinhNavigation.IdLoai))
			.ForMember(dest => dest.NoiDung, opt => opt.MapFrom(src => src.IdThuocTinhNavigation.NoiDung))
			.ForMember(dest => dest.Box, opt => opt.MapFrom(src => src.Ovuong))
			.ForMember(dest => dest.Dot, opt => opt.MapFrom(src => src.DauCham))
			.ReverseMap();

		CreateMap<ThuocTinhMau, TableDTO>()
			.ForMember(dest => dest.Cots, opt => opt.MapFrom(src => src.IdThuocTinhNavigation.Cots));

		CreateMap<DauCham, DauChamDTO>()
			.ForMember(dest => dest.Visible, opt => opt.MapFrom(src => src.Visible))
			.ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Rong ?? 0))
			.ReverseMap();

		CreateMap<Ovuong, OVuongDTO>()
			.ForMember(dest => dest.Visible, opt => opt.MapFrom(src => src.Visible))
			.ForMember(dest => dest.List, opt => opt.MapFrom(src => GetWidthBoxs(src.Rong)));

		CreateMap<OVuongDTO, Ovuong>()
			.ForMember(dest => dest.Visible, opt => opt.MapFrom(src => src.Visible))
			.ForMember(dest => dest.Rong, opt => opt.MapFrom(src => string.Join("-", src.List)));


		CreateMap<MauPdf, MauPdfDTO>();
		CreateMap<Cot, CotDTO>();
	}

	private List<int> GetWidthBoxs(string? rong)
	{
		if (rong == null || rong == "") return new List<int>();

		return rong.Split("-").Select(int.Parse).ToList();
	}
}
