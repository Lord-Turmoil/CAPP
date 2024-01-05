using System.Net;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Core.Models;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class PartService : BaseService<PartService>, IPartService
{
    public PartService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PartService> logger) : base(unitOfWork, mapper, logger)
    {
    }

    public async Task<ApiResponse<Part>> GetPartAsync(int id)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        Part? part = await repo.GetFirstOrDefaultAsync(
            predicate: p => p.Id == id,
            include: p => p.Include(p => p.Procedures).ThenInclude(p => p.Steps)
        );
        return part == null ? new ApiResponse<Part>("Not found") : new ApiResponse<Part>(part);
    }

    public async Task<ApiResponse<List<Part>>> GetPartsAsync()
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        IList<Part> parts = await repo.GetAllAsync();

        return new ApiResponse<List<Part>>(parts.ToList());
    }

    public async Task<ApiResponse<Part>> CreatePartAsync(string name, string opitz)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        Part part = new() {
            Name = name,
            Opitz = opitz,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await repo.InsertAsync(part);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<Part>(part);
    }

    public async Task<ApiResponse> DeletePartAsync(int id)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }
}