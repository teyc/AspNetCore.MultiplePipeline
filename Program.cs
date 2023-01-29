using AspNetMultiPipeline;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WebApiContrib.Core;

var applicationPart = new AssemblyPart(typeof(SampleController).Assembly);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<IGreeter, MalayGreeter>();

var app = builder.Build();

app.UseBranchWithServices("/es",
    s =>
    {
        s.AddRouting();
        
        // Explicit add AssemblyPart(thisAssembly) because 
        // for unknown reasons thisAssembly isn't picked up automatically
        s.AddControllers()
            .ConfigureApplicationPartManager(apm => { apm.ApplicationParts.Add(applicationPart); });
        s.AddMvc();
        s.AddTransient<IGreeter, SpanishGreeter>();
    },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapGet("debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
            endpoints.MapGet("greet", () => a.ApplicationServices.GetRequiredService<IGreeter>().Greet());
            endpoints.MapControllers();
        });
        a.Run(async r => await r.Response.WriteAsync("es endpoint"));
    });

app.UseBranchWithServices("/en",
    s =>
    {
        s.AddRouting();
        s.AddControllers().ConfigureApplicationPartManager(apm => { apm.ApplicationParts.Add(applicationPart); });
        s.AddMvc();
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

// app.UseDeveloperExceptionPage();
// app.UseRouting();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });
app.Run();