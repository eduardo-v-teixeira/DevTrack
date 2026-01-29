var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container (Controllers + Views)
builder.Services.AddControllersWithViews();

// Entity Framework Core + SQLite — ativar na Fase 2
// builder.Services.AddDbContext<DevTrackContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
