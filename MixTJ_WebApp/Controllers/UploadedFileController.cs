using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixTJ_Application.Dtos.FileDto;
using MixTJ_Application.Responses;

namespace MixTJ_WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadedFileController(IUploadFileService _fileService, ILogger<UploadedFileController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<UploadedFileGetDto>>> GetAll([FromQuery] UploadedFileFilter filter)
    {
        _logger.LogInformation("Fetching all uploaded files with filter {@Filter}", filter);
        return await _fileService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<UploadedFileGetDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching uploaded file with ID {Id}", id);
        return await _fileService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<Response<string>> Create(UploadedFileCreateDto dto)
    {
        _logger.LogInformation("Uploading a new file");
        return await _fileService.CreateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Delete(int id)
    {
        _logger.LogInformation("Deleting uploaded file with ID {Id}", id);
        return await _fileService.DeleteAsync(id);
    }
}


