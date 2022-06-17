using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IES9012.UI.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<IES9012Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IES9012Context") ?? throw new InvalidOperationException("Connection string 'IES9012Context' not found.")));
//esta linea se agrego cuando hicimos las paginas web, esto es trabjar con inyeccion de dependencias
//en las paginas web ya no trabajo con la indexacion(? de objetos
//porque muchos usuarios lo vana a usar al mismo tiempo
//porque si no el objeto se va a crear en base al usuario y no en base al programa
//esto se hace en le servidor, todo en el procesador de servidor
//la inyeccion siginifica: si al objeto la voy a necesitar para que el objeto se vaya  a instanciar

builder.Services.AddDbContext<IES9012Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IES9012Context") ?? throw new InvalidOperationException("La cadena de conexion'IES9012Context' no se encuentra.")));
//servicio contexto de base de datos, servira en toda las paginas que necesite acceder a la base de datos
//es el objeto que se va a instanciar para cada usuario

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else { 
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context=services.GetRequiredService<IES9012Context>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

    app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
