using Mango.Web.Models;
using System.Threading.Tasks;

namespace Mango.Web.Service.IService
{
    // Represents the base service interface for sending HTTP requests.
    public interface IBaseService
    {
        // Sends an asynchronous HTTP request using the provided RequestDto and returns a ResponseDto.
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}

