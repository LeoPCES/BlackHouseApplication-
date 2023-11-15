using BlackHouseApplication.Context;
using BlackHouseApplication.Controllers;
using BlackHouseApplication.Models;
using BlackHouseApplication.Repositories;
using BlackHouseApplication.Repositories.Interfaces;
using BlackHouseApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace BlackHouseApplication;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método é usado para configurar os serviços que a aplicação usará.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<AppDbContext>(options => // adicionando o banco de dados no serviço
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //identity
        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
        services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>(); // Injecao de dependencia do serviço de Admin 


        // Adicionando a politica Admin
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        services.AddMemoryCache(); // configurando o cache
        services.AddSession(); // configurando a session

    }

    // Este método é usado para configurar o pipeline HTTP.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // primeiro cria os perfis
        seedUserRoleInitial.SeedRoles();
        // depois cria o perfil Admin e injeta o perfil Admin no banco de dados
        seedUserRoleInitial.SeedUsers();

        app.UseSession();


        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {;

            // config com nome generico de área que funciona com qualquer área que não estiver definido explicitamente
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
