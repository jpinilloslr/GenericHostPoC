using MyApplication.Models;
using MyApplication.Services.External;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IOutput _output;

        public ApplicationService(IOutput output)
        {
            _output = output;
        }

        public Task DoWork(ApplicationInput applicationInput)
        {
            if (string.IsNullOrEmpty(applicationInput.Name))
            {
                _output.Error("You should provide a name.");
                return Task.CompletedTask;
            }

            if (applicationInput.Age <= 0)
            {
                _output.Error("You should provide an age bigger that 0.");
                return Task.CompletedTask;
            }

            _output.Information($"Your name is {applicationInput.Name} and you are {applicationInput.Age} years old.");
            return Task.CompletedTask;
        }
    }
}
