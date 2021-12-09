using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.Repositories;
using WebApp_Cadeteria.Models.Repositories.RepositoriesSQLite;

namespace WebApp_Cadeteria
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
            var ConnectionStrings = Configuration.GetConnectionString("Default");

            // CREACIÓN E INYECCIÓN DE BASE DE DATOS
            RepositorioCadeteSQLite repoCadetes =
                    new RepositorioCadeteSQLite(
                        Configuration.GetConnectionString("SqliteConection"),
                        NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorioPedidoSQLite repoPedidos =
                    new RepositorioPedidoSQLite(
                        Configuration.GetConnectionString("SqliteConection"),
                        NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorioUsuarioSQLite repoUsuarios =
                    new RepositorioUsuarioSQLite(
                        Configuration.GetConnectionString("SqliteConection"),
                        NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            RepositorioClienteSQLite repoClientes =
                    new RepositorioClienteSQLite(
                        Configuration.GetConnectionString("SqliteConection"),
                        NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger());

            DataContext data = new DataContext(repoCadetes, repoPedidos, repoUsuarios, repoClientes);
            services.AddSingleton(data);
            // CREACIÓN E INYECCIÓN DE BASE DE DATOS

            /*
            IRepositorioCadete repoCadetes = new RepositorioCadeteSQLite(ConnectionStrings);
            IRepositorioPedido repoPedidos = new RepositorioPedidoSQLite(ConnectionStrings);
            RepositorioUsuarioSQLite repoUsuarios = new RepositorioUsuarioSQLite(ConnectionStrings);
            RepositorioClienteSQLite repoClientes= new RepositorioClienteSQLite(ConnectionStrings);
            services.AddSingleton(repoCadetes);
            services.AddSingleton(repoPedidos);
            services.AddSingleton(repoUsuarios);
            services.AddSingleton(repoClientes);*/

            //Configuration.GetConnectionString("Default");
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(WebApp_Cadeteria.PerfilDeMapeo));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //services.AddSingleton(DB);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
