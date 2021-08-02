using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyPokedex.Api.Infrastructure;
using MyPokedex.Core;
using MyPokedex.Core.Config;
using MyPokedex.Core.External.FunTranslations;
using MyPokedex.Core.PokeApi;
using MyPokedex.Infrastructure.FunTranslations;
using MyPokedex.Infrastructure.PokeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPokedex.Api
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

            services.AddResponseCaching();
            services.AddControllers();
            services.AddHttpClient();
            services.AddTransient<IMyPokedexService, MyPokedexService>();
            services.AddTransient<IFunTranslationService, FunTranslationService>();
            services.AddTransient<IPokeApiSettings, PokeApiSettings>();
            services.AddTransient<IPokeApiService, PokeApiService>();
            services.Configure<TranslatedPokemonOptions>(Configuration.GetSection(
                                        TranslatedPokemonOptions.TranslatedPokemon));
            services.Configure<FunTranslationOptions>(Configuration.GetSection("External:FunTranslation"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCaching();

            // Cache GET Requests with Status OK for up to 10 Days.
            // This App only publishes two idempotent GET operations so we can cache the result for a long time
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(30)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
