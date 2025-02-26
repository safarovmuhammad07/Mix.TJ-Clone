using System.Net;
using AutoMapper;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Interfaces;
using MixTJ_Application.Dtos.NewsDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services;

public class NewsService(INewsRepository repository, IMapper mapper) : INewsService
{
    public async Task<Response<List<NewsGetDto>>> GetAllAsync(NewsFilter filter)
        {
            var newsList = await repository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.Title))
                newsList = newsList.Where(n => n.Title.ToLower().Contains(filter.Title.ToLower())).ToList();
            if (!string.IsNullOrEmpty(filter.Content))
                newsList = newsList.Where(n => n.Content.ToLower().Contains(filter.Content.ToLower())).ToList();
            if (!string.IsNullOrEmpty(filter.Category))
                newsList = newsList.Where(n => n.Category.ToLower().Contains(filter.Category.ToLower())).ToList();

            var dtoList = mapper.Map<List<NewsGetDto>>(newsList);
            return new Response<List<NewsGetDto>>(dtoList);
        }

        public async Task<Response<NewsGetDto>> GetByIdAsync(int id)
        {
            var news = await repository.GetByIdAsync(id);
            if (news == null)
                return new Response<NewsGetDto>(HttpStatusCode.NotFound, "News not found");

            var dto = mapper.Map<NewsGetDto>(news);
            return new Response<NewsGetDto>(dto);
        }

        public async Task<Response<string>> CreateAsync(NewsCreateDto dto)
        {
            var news = mapper.Map<News>(dto);
            await repository.AddAsync(news);
            return new Response<string>(HttpStatusCode.Created, "News created");
        }

        public async Task<Response<string>> UpdateAsync(NewsUpdateDto dto)
        {
            var news = await repository.GetByIdAsync(dto.Id);
            if (news == null)
                return new Response<string>(HttpStatusCode.NotFound, "News not found");

            mapper.Map(dto, news);
            await repository.UpdateAsync(news);
            return new Response<string>(HttpStatusCode.OK, "News updated");
        }

        public async Task<Response<string>> DeleteAsync(int id)
        {
            var news = await repository.GetByIdAsync(id);
            if (news == null)
                return new Response<string>(HttpStatusCode.NotFound, "News not found");

            await repository.DeleteAsync(news);
            return new Response<string>(HttpStatusCode.OK, "News deleted");
        }
    }