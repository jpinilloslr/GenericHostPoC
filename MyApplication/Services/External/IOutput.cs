namespace MyApplication.Services.External
{
    public interface IOutput
    {
        void Information(string message);
        void Warning(string message);
        void Error(string message);
    }
}