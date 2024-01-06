using System.Net;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Models;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class PartService : BaseService<PartService>, IPartService
{
    public PartService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PartService> logger) : base(unitOfWork, mapper, logger)
    {
    }

    public async Task<ApiResponse<PartDto>> GetPartAsync(int id)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        Part? part = await repo.GetFirstOrDefaultAsync(predicate: p => p.Id == id);
        if (part == null)
        {
            return new ApiResponse<PartDto>("Not found");
        }

        return new ApiResponse<PartDto>(_mapper.Map<Part, PartDto>(part));
    }


    public async Task<ApiResponse<List<PartDto>>> GetPartsAsync()
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        IList<Part> parts = await repo.GetAllAsync(orderBy: order => order.OrderBy(p => p.Name));

        return new ApiResponse<List<PartDto>>(parts.Select(_mapper.Map<Part, PartDto>).ToList());
    }

    public async Task<ApiResponse<PartDto>> CreatePartAsync(string name, string opitz)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        EntityEntry<Part> part = await repo.InsertAsync(new Part {
            Name = name,
            Opitz = opitz,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<PartDto>(_mapper.Map<Part, PartDto>(part.Entity));
    }

    public async Task<ApiResponse> DeletePartAsync(int id)
    {
        IRepository<Part> repo = _unitOfWork.GetRepository<Part>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }
}