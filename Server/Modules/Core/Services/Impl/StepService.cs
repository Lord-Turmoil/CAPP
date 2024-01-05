using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Server.Modules.Core.Models;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class StepService : BaseService<StepService>, IStepService
{
    public StepService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StepService> logger) : base(unitOfWork, mapper, logger)
    {
    }

    public async Task<ApiResponse<StepDto>> CreateStepAsync(int procedureId, string description)
    {
        IRepository<Procedure> procedureRepo = _unitOfWork.GetRepository<Procedure>();
        IRepository<Step> stepRepo = _unitOfWork.GetRepository<Step>();

        Procedure? procedure = await procedureRepo.FindAsync(procedureId);
        if (procedure == null)
        {
            return new ApiResponse<StepDto>("Not found");
        }

        Step step = new() {
            Procedure = procedure,
            Description = description
        };
        await stepRepo.InsertAsync(step);

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<StepDto>(_mapper.Map<StepDto>(step));
    }
}