using AutoMapper;
using CustomPdf_BE.DTOs;
using CustomPdf_BE.Models;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// // Map từ Entity → DTO
		CreateMap<ThuocTinhMau, ThuocTinhDTO>()
			 .ConvertUsing((src, dest, context) =>
		context.Mapper.Map<ThuocTinhDTO>(src.IdThuocTinhNavigation));

		CreateMap<MauPdf, MauPdfDTO>();

		CreateMap<ThuocTinh, ThuocTinhDTO>()
			.ForMember(dest => dest.TenLoai, opt => opt.MapFrom(src => src.IdLoaiNavigation.TenLoai))
			.ForMember(dest => dest.Box, opt => opt.MapFrom(src => new Box
			{
				Visible = src.Ovuong.Visible,
				List = GetWidthBoxs(src.Ovuong.Rong)
			}))
			.ForMember(dest => dest.Dot, opt => opt.MapFrom(src => new Dot
			{
				Visible = src.DauCham.Visible,
				Width = src.DauCham.Rong ?? 0
			}))
			.ForMember(dest => dest.TenLoai, opt => opt.MapFrom(src => src.IdLoaiNavigation.TenLoai));

		// // Map từ DTO → Entity
		// CreateMap<UserDto, User>();
	}

	private List<int> GetWidthBoxs(string? rong)
	{
		if (rong == null) return new List<int>();

		return rong.Split("-").Select(int.Parse).ToList();
	}
}
