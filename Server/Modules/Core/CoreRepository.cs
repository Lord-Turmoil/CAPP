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

public class ProcedureRepository : Repository<Procedure>
{
    public ProcedureRepository(CappDbContext dbContext) : base(dbContext)
    {
    }
}

public class StepRepository : Repository<Step>
{
    public StepRepository(CappDbContext dbContext) : base(dbContext)
    {
    }
}