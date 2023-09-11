using API.Helpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header) 
        {
            // Since we not in a controller we need to explicity mention this
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Adding the header which will be passed when calling this method
            response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));

            // Because this is a custom header we going need to do something to explicityly allow CORS Policy 
            // Otherwise, our client won't be to access the information inside this header

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
