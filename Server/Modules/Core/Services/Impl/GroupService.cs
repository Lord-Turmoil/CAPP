// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Net;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Modules.Core.Models;
using Shared.Common;
using Shared.Dtos;
using Tonisoft.AspExtensions.Module;
using Tonisoft.AspExtensions.Response;

namespace Server.Modules.Core.Services.Impl;

public class GroupService : BaseService<GroupService>, IGroupService
{
    public GroupService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GroupService> logger) : base(unitOfWork, mapper,
        logger)
    {
    }

    public async Task<ApiResponse<GroupDto>> CreateGroupAsync(string description, string matrix)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        EntityEntry<Group> group = await repo.InsertAsync(new Group {
            Description = description,
            Matrix = matrix,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<GroupDto>(_mapper.Map<GroupDto>(group.Entity));
    }

    public async Task<ApiResponse<GroupDto>> UpdateGroupAsync(int id, string description, string matrix)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        Group? group = await repo.FindAsync(id);
        if (group == null)
        {
            return new ApiResponse<GroupDto>("Not found");
        }

        group.Description = description;
        group.Matrix = matrix;

        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<GroupDto>(_mapper.Map<GroupDto>(group));
    }

    public async Task<ApiResponse<GroupDto>> GetGroupAsync(int id)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        Group? group = await repo.GetFirstOrDefaultAsync(
            include: source => source.Include(p => p.Procedures),
            predicate: p => p.Id == id);
        if (group == null)
        {
            return new ApiResponse<GroupDto>("Not found");
        }

        var dto = _mapper.Map<GroupDto>(group);
        IList<Part> parts = await _unitOfWork.GetRepository<Part>().GetAllAsync();
        dto.Parts = GtCore.Filter(parts, p => p.Opitz, group.Matrix).Select(_mapper.Map<Part, PartDto>);

        return new ApiResponse<GroupDto>(dto);
    }

    public async Task<ApiResponse<List<GroupDto>>> GetGroupsAsync()
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        IList<Group> groups = await repo.GetAllAsync(
            include: source => source.Include(p => p.Procedures),
            orderBy: order => order.OrderBy(p => p.Description));

        IReadOnlyCollection<GroupDto> dtos = groups.Select(_mapper.Map<Group, GroupDto>).ToList();
        IList<Part> parts = await _unitOfWork.GetRepository<Part>().GetAllAsync();
        foreach (GroupDto dto in dtos)
        {
            dto.Parts = GtCore.Filter(parts, p => p.Opitz, dto.Matrix)
                .Select(_mapper.Map<Part, PartDto>);
        }

        return new ApiResponse<List<GroupDto>>(dtos.ToList());
    }

    public async Task<ApiResponse> DeleteGroupAsync(int id)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }

    public async Task<ApiResponse<List<GroupDto>>> SearchGroups(string opitz)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        IList<Group> groups = await repo.GetAllAsync(
            include: source => source.Include(p => p.Procedures),
            orderBy: order => order.OrderBy(p => p.Description));

        IReadOnlyCollection<GroupDto> dtos = groups.Where(g => GtCore.Match(opitz, g.Matrix))
            .Select(_mapper.Map<Group, GroupDto>).ToList();
        IList<Part> parts = await _unitOfWork.GetRepository<Part>().GetAllAsync();
        foreach (GroupDto dto in dtos)
        {
            dto.Parts = GtCore.Filter(parts, p => p.Opitz, dto.Matrix)
                .Select(_mapper.Map<Part, PartDto>);
        }

        return new ApiResponse<List<GroupDto>>(dtos.ToList());
    }
}