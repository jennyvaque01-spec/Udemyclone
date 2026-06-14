using UdemyClone.Application.Models.Responses;

namespace UdemyClone.Application.Helpers
{
    public static class ResponseHelper
    {
        public static GenericResponse<T> Create<T>(T data, string message = "Solicitud exitosa")
        {
            return new GenericResponse<T>
            {
                Data = data,
                Message = message
            };
        }
    }
}



