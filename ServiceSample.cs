namespace AspNetMultiPipeline;

public interface IGreeter
{
    string Greet();
}

public class EnglishGreeter : IGreeter
{
    public string Greet() { return "Hello!"; }
}