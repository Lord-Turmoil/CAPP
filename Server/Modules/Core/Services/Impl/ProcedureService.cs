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

        var now = DateTime.Now;
        EntityEntry<Procedure> procedure = await procedureRepo.InsertAsync(new Procedure {
            Group = group,
            Description = description,
            Order = order,
            CreatedAt = now,
            UpdatedAt = now
        });

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProcedureDto>(_mapper.Map<Procedure, ProcedureDto>(procedure.Entity));
    }

    public async Task<ApiResponse<ProcedureDto>> UpdateProcedureAsync(int id, string description)
    {
        IRepository<Procedure> repo = _unitOfWork.GetRepository<Procedure>();
        Procedure? procedure = await repo.FindAsync(id);
        if (procedure == null)
        {
            return new ApiResponse<ProcedureDto>("Not found");
        }

        procedure.Description = description;
        procedure.UpdatedAt = DateTime.Now;

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProcedureDto>(_mapper.Map<Procedure, ProcedureDto>(procedure));
    }

    public async Task<ApiResponse> DeleteProcedureAsync(int id)
    {
        IRepository<Procedure> repo = _unitOfWork.GetRepository<Procedure>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }
}