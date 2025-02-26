using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Interfaces;
using MixTJ_Application.Dtos.FileDto;
using MixTJ_Application.Interfaces.Repositories;
using MixTJ_Application.Responses;

namespace Infrastructure.Services;

public class UploadFileService(IUploadFileRepository repository, IMapper mapper) : IUploadFileService
{
    public async Task<Response<List<UploadedFileGetDto>>> GetAllAsync(UploadedFileFilter filter)
        {
            var files = await repository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.FileName))
                files = files.Where(f => f.FileName.ToLower().Contains(filter.FileName.ToLower())).ToList();
            if (!string.IsNullOrEmpty(filter.FilePath))
                files = files.Where(f => f.FilePath.ToLower().Contains(filter.FilePath.ToLower())).ToList();

            var dtoList = mapper.Map<List<UploadedFileGetDto>>(files);
            return new Response<List<UploadedFileGetDto>>(dtoList);
        }

        public async Task<Response<UploadedFileGetDto>> GetByIdAsync(int id)
        {
            var file = await repository.GetByIdAsync(id);
            if (file == null)
                return new Response<UploadedFileGetDto>(HttpStatusCode.NotFound, "File not found");

            var dto = mapper.Map<UploadedFileGetDto>(file);
            return new Response<UploadedFileGetDto>(dto);
        }

        public async Task<Response<string>> CreateAsync(UploadedFileCreateDto dto)
        {
            var file = mapper.Map<UploadedFile>(dto);
            await repository.AddAsync(file);
            return new Response<string>(HttpStatusCode.Created, "File uploaded");
        }

        public async Task<Response<string>> DeleteAsync(int id)
        {
            var file = await repository.GetByIdAsync(id);
            if (file == null)
                return new Response<string>(HttpStatusCode.NotFound, "File not found");

            await repository.DeleteAsync(file);
            return new Response<string>(HttpStatusCode.OK, "File deleted");
        }
    }