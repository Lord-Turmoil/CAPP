using System.Net;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Modules.Core.Models;
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

    public async Task<ApiResponse<GroupDto>> GetGroupAsync(int id)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        Group? group = await repo.GetFirstOrDefaultAsync(
            predicate: p => p.Id == id,
            include: source => source.Include(p => p.Procedures)
        );
        if (group == null)
        {
            return new ApiResponse<GroupDto>("Not found");
        }

        return new ApiResponse<GroupDto>(_mapper.Map<GroupDto>(group));
    }

    public async Task<ApiResponse<List<GroupDto>>> GetGroupsAsync()
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        IList<Group> groups = await repo.GetAllAsync(include: source => source.Include(p => p.Procedures));

        return new ApiResponse<List<GroupDto>>(groups.Select(_mapper.Map<Group, GroupDto>).ToList());
    }

    public async Task<ApiResponse> DeleteGroupAsync(int id)
    {
        IRepository<Group> repo = _unitOfWork.GetRepository<Group>();
        repo.Delete(id);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse(HttpStatusCode.NoContent);
    }
}