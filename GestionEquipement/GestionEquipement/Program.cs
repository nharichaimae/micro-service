using GestionEquipement.Data;
using GestionEquipement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// partier de base de donne my sql 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// les Services 
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<SymfonyAuthService>();
builder.Services.AddScoped<PieceService>();

// CORS pour la connexion avec angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();


app.UseCors("AllowAngular");

//Middleware pour la verification de token 
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        var authService = context.RequestServices.GetRequiredService<SymfonyAuthService>();

        if (context.Request.Headers.TryGetValue("X-AUTH-TOKEN", out var token))
        {
            var userId = await authService.GetUserIdAsync(token);
            if (userId != null)
            {
                context.Items["UserId"] = userId.Value;
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = "Token invalide ou expiré"
                });
                return;
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Token manquant"
            });
            return;
        }
    }

    await next();
});


app.UseAuthorization();


app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pieces}/{action=Add}/{id?}"
);

app.Run();
