using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetMultiPipeline;

[Route("sample")]
public class SampleController : Controller
{
    private readonly IGreeter greeter;

    public SampleController(IGreeter greeter)
    {
        this.greeter = greeter;
    }

    [HttpGet, Route("greet")]
    public ActionResult Index()
    {
        return Ok(greeter.Greet());
    }

}
