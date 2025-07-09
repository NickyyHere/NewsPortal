using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Interfaces;
using NewsPortal.Application.Mappings;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Infrastructure;
using NewsPortal.Infrastructure.Repositories;
using NewsPortal.Application.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace NewsPortal.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEFCoreRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISlugGenerator, SlugGenerator>();
            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ArticleMappingProfile>();
                cfg.AddProfile<CategoryMappingProfile>();
            });
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateArticleValidator>(ServiceLifetime.Transient);
            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NewsPortal API",
                    Version = "v1",
                    Description = "News Portal API - Recruitment task"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }

    }
}
