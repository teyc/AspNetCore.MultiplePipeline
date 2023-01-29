using AspNetMultiPipeline;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WebApiContrib.Core;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseBranchWithServices("/es",
    s =>
    {
        s.AddControllers().ConfigureApplicationPartManager(
            apm => apm.ApplicationParts.Add(new AssemblyPart(typeof(SampleController).Assembly)));
        s.AddRouting();
        s.AddTransient<IGreeter, SpanishGreeter>();
    },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapControllers();
        });
    });

app.UseBranchWithServices("/en",
    s =>
    {
        s.AddControllers().ConfigureApplicationPartManager(
            apm => apm.ApplicationParts.Add(new AssemblyPart(typeof(SampleController).Assembly)));
        s.AddRouting();
        s.AddTransient<IGreeter, EnglishGreeter>();
    },
    a =>
    {
        a.UseRouting();
        a.UseEndpoints(endpoints =>
        {
            //Define endpoint routes here
            endpoints.MapControllers();
        });
    });

app.Run();