// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Tonisoft.AspExtensions.Module;

namespace Server.Modules.Core;

public class CoreModule : BaseModule
{
    public override IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services;
    }
}