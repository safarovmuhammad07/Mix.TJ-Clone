using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MixTJ_Application.Dtos.TagDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services;

public class TagService(ITagRepository repository, IMapper mapper) : ITagService
{
    public async Task<Response<List<TagGetDto>>> GetAllAsync(TagFilter filter)
        {
            var tags = await repository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.Name))
                tags = tags.Where(t => t.Name.ToLower().Contains(filter.Name.ToLower())).ToList();

            var dtoList = mapper.Map<List<TagGetDto>>(tags);
            return new Response<List<TagGetDto>>(dtoList);
        }

        public async Task<Response<TagGetDto>> GetByIdAsync(int id)
        {
            var tag = await repository.GetByIdAsync(id);
            if (tag == null)
                return new Response<TagGetDto>(HttpStatusCode.NotFound, "Tag not found");

            var dto = mapper.Map<TagGetDto>(tag);
            return new Response<TagGetDto>(dto);
        }

        public async Task<Response<string>> CreateAsync(TagCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 5)
                return new Response<string>(HttpStatusCode.BadRequest, "Tag name must be at least 5 characters");

            var tag = mapper.Map<Tag>(dto);
            await repository.AddAsync(tag);
            return new Response<string>(HttpStatusCode.Created, "Tag created");
        }

        public async Task<Response<string>> UpdateAsync(TagUpdateDto dto)
        {
            var tag = await repository.GetByIdAsync(dto.Id);
            if (tag == null)
                return new Response<string>(HttpStatusCode.NotFound, "Tag not found");

            mapper.Map(dto, tag);
            await repository.UpdateAsync(tag);
            return new Response<string>(HttpStatusCode.OK, "Tag updated");
        }

        public async Task<Response<string>> DeleteAsync(int id)
        {
            var tag = await repository.GetByIdAsync(id);
            if (tag == null)
                return new Response<string>(HttpStatusCode.NotFound, "Tag not found");

            await repository.DeleteAsync(tag);
            return new Response<string>(HttpStatusCode.OK, "Tag deleted");
        }
    }