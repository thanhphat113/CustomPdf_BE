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

		CreateMap<ThuocTinh, ThuocTinhDTO>()
			.ForMember(dest => dest.TenLoai, opt => opt.MapFrom(src => src.IdLoaiNavigation.TenLoai));

		// // Map từ DTO → Entity
		// CreateMap<UserDto, User>();
	}
}
