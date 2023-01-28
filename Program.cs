using WebApiContrib.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(o => o.EnableEndpointRouting = false);

var app = builder.Build();
app.UseBranchWithServices("/es",
    s => { s.AddMvc(o => o.EnableEndpointRouting = false); s.AddTransient<IGreeter, SpanishGreeter>(); },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapControllerRoute(
                name: "default1",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        a.Run(async r => await r.Response.WriteAsync("es endpoint"));
    });

app.UseBranchWithServices("/en",
    s => { s.AddMvc(o => o.EnableEndpointRouting = false); s.AddTransient<IGreeter, EnglishGreeter>(); },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapControllerRoute(
                name: "default2",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        a.Run(async r => await r.Response.WriteAsync("en endpoint"));

    });

app.UseDeveloperExceptionPage();
app.Run();