using AutoMapper;
using Domain.Entites;
using Domain.Entities;
using MixTJ_Application.Dtos.CommentDto;
using MixTJ_Application.Dtos.FileDto;
using MixTJ_Application.Dtos.NewsDto;
using MixTJ_Application.Dtos.TagDto;
using MixTJ_Application.Dtos.UserDto;
using MixTJ_Application.Dtos.VideoDto;

namespace MixTJ_Application.Profiles;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<Comment, CommentGetDto>();

        CreateMap<CommentCreateDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<CommentUpdateDto, Comment>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

        CreateMap<News, NewsGetDto>();

        CreateMap<NewsCreateDto, News>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<NewsUpdateDto, News>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

        CreateMap<Tag, TagCreateDto>().ReverseMap();
        CreateMap<Tag, TagGetDto>();  
        CreateMap<TagUpdateDto, Tag>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));
        CreateMap<TagCreateDto, Tag>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Ensure non-null mapping

        CreateMap<User, UserGetDto>();

        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Nickname));

        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Nickname));

        CreateMap<Video, VideoGetDto>();

        CreateMap<VideoCreateDto, Video>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

        CreateMap<VideoUpdateDto, Video>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

        CreateMap<UploadedFile, UploadedFileGetDto>();

        CreateMap<UploadedFileCreateDto, UploadedFile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
    }
}
