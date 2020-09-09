using AspNetCoreGraphQL.AutoMapper;
using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.Data;
using AspNetCoreGraphQL.Entities;
using AspNetCoreGraphQL.GraphQL;
using AspNetCoreGraphQL.GraphQL.Queries;
using AspNetCoreGraphQL.GraphQL.Types;
using AspNetCoreGraphQL.Helper;
using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreGraphQL
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

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //Context
            services.AddSingleton<EFContext>(); // EfUserGroupDal Testler Ýçin, Normalde EfEntityRepositoryBase üzerinden naðlanýyoruz.

            //Jwt Token
            /*
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "graphQLApi";
            });
            */

            //IOC
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IValidationRule, AuthValidationRule>();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IUserDal, EfUserDal>();
            services.AddTransient<IUserGroupService, UserGroupManager>();
            services.AddTransient<IUserGroupDal, EfUserGroupDal>();

            //AutoMapper
            services.AddSingleton(new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper());

            //GraphQL
            services.AddSingleton<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ISchema, UserSchema>();
            services.AddTransient<RootQuery>();
            services.AddTransient<UsersType>();
            services.AddTransient<UserGroupType>();
            services.AddTransient<UserDTOType>();
            //services.AddScoped<UserGroupQuery>();
            //services.AddScoped<UserQuery>();

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
               .AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGraphQL<UserSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
