﻿namespace BookmarksAPI
{
    using AutoMapper;
    using BookmarksAPI.Models;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using DataWorkShop.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookmarksDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BookmarksService"), b => b.MigrationsAssembly("DataWorkShop")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookmarksDbContext>();

                context.Database.Migrate();

                if (env.IsDevelopment())
                {
                    // Seed the database.
                    context.EnsureSeedData();
                }
            }

            app.UseMvc();

            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Category, CategoryViewModel>()
                .ForSourceMember(sm => sm.RowVersion, opt => opt.Ignore())
                .ForSourceMember(sm => sm.Parent, opt => opt.Ignore());
            });
        }
    }
}
