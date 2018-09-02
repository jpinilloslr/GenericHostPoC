using MyApplication.Models;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public interface IApplicationService
    {
        Task DoWork(ApplicationInput applicationInput);
    }
}
