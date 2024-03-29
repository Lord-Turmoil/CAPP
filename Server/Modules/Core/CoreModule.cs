﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Arch.EntityFrameworkCore.UnitOfWork;
using Server.Modules.Core.Models;
using Server.Modules.Core.Services;
using Server.Modules.Core.Services.Impl;
using Tonisoft.AspExtensions.Module;

namespace Server.Modules.Core;

public class CoreModule : BaseModule
{
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddCustomRepository<Part, PartRepository>()
            .AddCustomRepository<Group, GroupRepository>()
            .AddCustomRepository<Procedure, ProcedureRepository>();

        services.AddScoped<IPartService, PartService>()
            .AddScoped<IProcedureService, ProcedureService>()
            .AddScoped<IGroupService, GroupService>();

        return services;
    }
}