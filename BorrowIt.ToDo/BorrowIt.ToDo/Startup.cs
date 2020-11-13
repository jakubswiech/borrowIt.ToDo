using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using BorrowIt.Common.Extensions;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Common.Infrastructure.Implementations;
using BorrowIt.Common.Infrastructure.IoC;
using BorrowIt.Common.Mongo.IoC;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.IoC;
using BorrowIt.ToDo.Application.ToDoLists;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Application.ToDoLists.Mappings;
using BorrowIt.ToDo.Application.ToDoLists.Queries;
using BorrowIt.ToDo.Application.ToDoLists.ReadModels;
using BorrowIt.ToDo.Application.Users.Handlers;
using BorrowIt.ToDo.Domain.Model.Users;
using BorrowIt.ToDo.Infrastructure.Entities.ToDoLists;
using BorrowIt.ToDo.Infrastructure.Inboud.Messages;
using BorrowIt.ToDo.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BorrowIt.ToDo
{
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
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(ctx =>
            {

                var security = new OpenApiSecurityRequirement { { new OpenApiSecurityScheme() { Name = "Bearer" }, new string[] { } } };

                ctx.SwaggerDoc("v1", new OpenApiInfo() { Title = "BorrowIt.Auth", Version = "v1" });

                ctx.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                ctx.AddSecurityRequirement(security);
            });
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            
            var builder = new ContainerBuilder();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RawRabbitModule(Configuration));
            builder.RegisterModule(new MongoDbModule(Configuration, "mongoDb"));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRepositories<UserRepository>()
                .AddGenericRepository(typeof(GenericMongoRepository<,>));
            builder.RegisterAssemblyTypes(typeof(UserChangedEventHandler).Assembly)
                .AsClosedTypesOf(typeof(IMessageHandler<>));
            builder.AddSerilog();
            builder.AddServices<IToDoListsService>();
            builder.RegisterAssemblyTypes(typeof(CreateToDoListCommand).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.Register(ctx =>
            {
                var assemblies = new List<Assembly>
                {
                    typeof(ToDoListMappingProfile).Assembly,
                    typeof(ToDoListCommandMappingProfile).Assembly
                };

                var mapperConfig = new MapperConfiguration(x =>
                    x.AddProfiles(assemblies));

                return mapperConfig.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(ToDoListQuery).Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(typeof(IToDoListReadModel).Assembly).Where(x => x.Name.EndsWith("ReadModel"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(IUserFactory).Assembly).Where(x => x.Name.EndsWith("Factory"))
                .AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            app.UseApiExceptionMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Borrowit.ToDo");
            });
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseRabbitMq()
                .SubscribeMessage<UserChangedEvent>()
                .SubscribeMessage<UserRemovedEvent>();
        }
    }
}