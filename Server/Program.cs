// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Modules;
using Tonisoft.AspExtensions.Module;

namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddUnitOfWork<CappDbContext>();
        builder.Services.AddDbContext<CappDbContext>(options =>
            options.UseSqlite("Data Source=Database.db"));
        builder.Services.RegisterModules();

        var autoMapperConfig = new MapperConfiguration(config => { config.AddProfile(new AutoMapperProfile()); });
        builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}