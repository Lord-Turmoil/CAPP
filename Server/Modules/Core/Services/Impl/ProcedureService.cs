// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Net;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Models;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class ProcedureService : BaseService<ProcedureService>, IProcedureService
{
    public ProcedureService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProcedureService> logger) : base(unitOfWork,
        mapper, logger)
    {
    }

    public async Task<ApiResponse<ProcedureDto>> CreateProcedureAsync(int groupId, string description, int order)
    {
        IRepository<Group> groupRepo = _unitOfWork.GetRepository<Group>();
        IRepository<Procedure> procedureRepo = _unitOfWork.GetRepository<Procedure>();

        Group? group = await groupRepo.FindAsync(groupId);
        if (group == null)
        {
            return new ApiResponse<ProcedureDto>("Not found");
        }

        EntityEntry<Procedure> procedure = await procedureRepo.InsertAsync(new Procedure {
            Group = group,
            Description = description,
            Order = order,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProcedureDto>(_mapper.Map<Procedure, ProcedureDto>(procedure.Entity));
    }

    public async Task<ApiResponse> DeleteProcedureAsync(int id)
    {
        IRepository<Procedure> repo = _unitOfWork.GetRepository<Procedure>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }
}