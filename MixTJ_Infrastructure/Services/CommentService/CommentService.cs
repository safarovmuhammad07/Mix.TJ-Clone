using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Interfaces;
using MixTJ_Application.Dtos.CommentDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services
{
    public class CommentService(ICommentRepository repository, IMapper mapper) : ICommentService
    {
        public async Task<Response<List<CommentGetDto>>> GetAllAsync(CommentFilter filter)
        {
            var comments = await repository.GetAllAsync();
            
            if (!string.IsNullOrEmpty(filter.Text))
                comments = comments.Where(c => c.Text.ToLower().Contains(filter.Text.ToLower())).ToList();
            var dtoList = mapper.Map<List<CommentGetDto>>(comments);
            return new Response<List<CommentGetDto>>(dtoList);
        }
        
        public async Task<Response<CommentGetDto>> GetByIdAsync(int id)
        {
            var comment = await repository.GetByIdAsync(id);
            if (comment == null)
                return new Response<CommentGetDto>(HttpStatusCode.NotFound, "Comment Not Found");
            
            var dto = mapper.Map<CommentGetDto>(comment);
            return new Response<CommentGetDto>(dto);
        }
        
        public async Task<Response<string>> CreateAsync(CommentCreateDto dto)
        {
            var comment = mapper.Map<Comment>(dto);
            await repository.AddAsync(comment);
            return new Response<string>(HttpStatusCode.Created, "Comment Created");
        }
        
        public async Task<Response<string>> UpdateAsync(CommentUpdateDto dto)
        {
            var comment = await repository.GetByIdAsync(dto.Id);
            if (comment == null)
                return new Response<string>(HttpStatusCode.NotFound, "Comment Not Found");
            
            mapper.Map(dto, comment);
            await repository.UpdateAsync(comment);
            return new Response<string>(HttpStatusCode.OK, "Comment Updated");
        }
        
        public async Task<Response<string>> DeleteAsync(int id)
        {
            var comment = await repository.GetByIdAsync(id);
            if (comment == null)
                return new Response<string>(HttpStatusCode.NotFound, "Comment Not Found");
            
            await repository.DeleteAsync(comment);
            return new Response<string>(HttpStatusCode.OK, "Comment Deleted");
        }
    }
}
