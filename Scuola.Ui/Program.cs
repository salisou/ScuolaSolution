using Scuola.Ui.Components;
using Scuola.Ui.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Chiamta del APi
builder.Services.AddHttpClient("Scuola.Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7079/api");
});

// Uso dell'Entity Framework Core per Cominicare con i service 
builder.Services.AddScoped<StudenteService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
