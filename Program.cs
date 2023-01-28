using AspNetMultiPipeline;
using WebApiContrib.Core;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseBranchWithServices("/es",
    s =>
    {
        s.AddRouting();
        s.AddControllers();
        s.AddTransient<SampleRouteTransformer>();
        s.AddTransient<IGreeter, SpanishGreeter>();
    },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapGet("greet", () => a.ApplicationServices.GetRequiredService<IGreeter>().Greet());
            endpoints.MapDefaultControllerRoute();
            endpoints.MapDynamicControllerRoute<SampleRouteTransformer>("sample");
        });
        a.Run(async r => await r.Response.WriteAsync("es endpoint"));
    });

app.UseBranchWithServices("/en",
    s =>
    {
        s.AddRouting();
        s.AddControllers();
        s.AddTransient<SampleRouteTransformer>();
        s.AddTransient<IGreeter, EnglishGreeter>();
    },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapGet("greet", () => a.ApplicationServices.GetRequiredService<IGreeter>().Greet());
            endpoints.MapControllers();
        });
        a.Run(async r => await r.Response.WriteAsync("en endpoint"));

    });

app.UseDeveloperExceptionPage();
app.Run();