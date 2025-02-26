using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Dtos.VideoDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services;

public class VideoService(IVideoRepository repository, IMapper mapper) : IVideoService
{
    public async Task<Response<List<VideoGetDto>>> GetAllAsync(VideoFilter filter)
    {
        var videos = await repository.GetAllAsync();

        if (!string.IsNullOrEmpty(filter.Title))
            videos = videos.Where(v => v.Title.ToLower().Contains(filter.Title.ToLower())).ToList();
        if (!string.IsNullOrEmpty(filter.Description))
            videos = videos.Where(v => v.Description.ToLower().Contains(filter.Description.ToLower())).ToList();
        if (!string.IsNullOrEmpty(filter.URL))
            videos = videos.Where(v => v.URL.ToLower().Contains(filter.URL.ToLower())).ToList();
        if (!string.IsNullOrEmpty(filter.VideoType))
            videos = videos.Where(v => v.VideoType.ToLower().Contains(filter.VideoType.ToLower())).ToList();

        var dtoList = mapper.Map<List<VideoGetDto>>(videos);
        return new Response<List<VideoGetDto>>(dtoList);
    }

    public async Task<Response<VideoGetDto>> GetByIdAsync(int id)
    {
        var video = await repository.GetByIdAsync(id);
        if (video == null)
            return new Response<VideoGetDto>(HttpStatusCode.NotFound, "Video not found");

        var dto = mapper.Map<VideoGetDto>(video);
        return new Response<VideoGetDto>(dto);
    }

    public async Task<Response<string>> CreateAsync(VideoCreateDto dto)
    {
        var video = mapper.Map<Video>(dto);
        await repository.AddAsync(video);
        return new Response<string>(HttpStatusCode.Created, "Video created");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var video = await repository.GetByIdAsync(id);
        if (video == null)
            return new Response<string>(HttpStatusCode.NotFound, "Video not found");

        await repository.DeleteAsync(video);
        return new Response<string>(HttpStatusCode.OK, "Video deleted");
    }
}