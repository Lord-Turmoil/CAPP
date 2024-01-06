// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Arch.EntityFrameworkCore.UnitOfWork;
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