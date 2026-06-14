using UdemyClone.Shared.Helpers;

namespace UdemyClone.Application.Models.Responses
{
    public class GenericResponse<T>
    {
        public string Message { get; set; } = "";
        public DateTime TimeStamp { get; } = DateTimeHelper.UtcNow();
        public T Data { get; set; } = default!;

    }
}
