using Microsoft.AspNetCore.Mvc;

namespace AspNetMultiPipeline;

public interface IGreeter
{
    string Greet();
}

public class EnglishGreeter : IGreeter
{
    public string Greet() { return "Hello!"; }
}

public class SpanishGreeter : IGreeter
{
    public string Greet()
    {
        return "Ola!";
    }
}


[Route("sample")]
public class SampleController : Controller
{
    private readonly IGreeter _greeter;

    public SampleController(IGreeter greeter)
    {
        this._greeter = greeter;
    }

    [HttpGet, Route("greet")]
    public ActionResult Index()
    {
        return Ok(_greeter.Greet());
    }

}