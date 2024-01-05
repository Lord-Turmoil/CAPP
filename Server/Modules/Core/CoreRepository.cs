using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Core.Models;

namespace Server.Modules.Core;

public class PartRepository : Repository<Part>
{
    public PartRepository(CappDbContext dbContext) : base(dbContext)
    {
    }
}

public class GroupRepository : Repository<Group>
{
    public GroupRepository(CappDbContext dbContext) : base(dbContext)
    {
    }
}

public class ProcedureRepository : Repository<Procedure>
{
    public ProcedureRepository(CappDbContext dbContext) : base(dbContext)
    {
    }
}