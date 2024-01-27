using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.OpenApi.Models;
using SuperShop.Helper;
using SuperShop.Repository.Interface;
using SuperShop.Repository.Implementation;
using SuperShop.Services.Interface;
using SuperShop.Services.Implementation;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using SuperShop.Services.Interface.common;
using SuperShop.Services.Implementation.common;
using SuperShop.Services.Operation;
using SuperShop.Repository.Report;
using SuperShop.Services.Report;
using SuperShop.Services.Customer;
using SuperShop.Repository.Customer;

namespace SuperShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApplicationConstant.SecurityAPI = Configuration["Data:SecurityAPI"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            // for returning Pascalcase json
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    });

            services.AddMvc()
                    .AddControllersAsServices();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });
            //services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IOutletService, OutletService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IQtyTypeService, QtyTypeService>();
            services.AddScoped<IUploadedFileService, UploadedFileService>();

            //Repository
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IOutletRepository, OutletRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IQtyTypeRepository, QtyTypeRepository>();
            services.AddScoped<ICustomerTransactionRepository, CustomerTransactionRepository>();

            //Operation
            services.AddScoped<IOperationsService, OperationsService>();
            services.AddScoped<ITransactionService, TransactionService>();

            //Report
            services.AddScoped<ISalesReportService, SalesReportService>();
            services.AddScoped<IInventoryReportService, InventoryReportService>();

            //Customer
            services.AddScoped<ICustProductService, CustProductService>();
            services.AddScoped<ICustTransactionService, CustTransactionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
        }
    }
}
