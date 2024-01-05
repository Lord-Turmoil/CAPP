using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Models;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class ProcedureService : BaseService<ProcedureService>, IProcedureService
{
    public ProcedureService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProcedureService> logger) : base(unitOfWork, mapper, logger)
    {
    }

    public async Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int partId, string description, int order)
    {
        IRepository<Part> partRepo = _unitOfWork.GetRepository<Part>();
        IRepository<Procedure> procedureRepo = _unitOfWork.GetRepository<Procedure>();

        Part? part = await partRepo.FindAsync(partId);
        if (part == null)
        {
            return new ApiResponse<ProcedureDto>("Not found");
        }

        Procedure procedure = new() {
            Part = part,
            Description = description,
            Order = order,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await procedureRepo.InsertAsync(procedure);

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProcedureDto>(_mapper.Map<ProcedureDto>(procedure));
    }
}