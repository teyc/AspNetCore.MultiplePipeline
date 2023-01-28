using Microsoft.AspNetCore.Mvc;

[Route("sample")]
public class SampleController : Controller
{
    private readonly IGreeter greeter;

    public SampleController(IGreeter greeter)
    {
        this.greeter = greeter;
    }

    [HttpGet, Route("")]
    public ActionResult Index()
    {
        return Ok(greeter.Greet());
    }

}