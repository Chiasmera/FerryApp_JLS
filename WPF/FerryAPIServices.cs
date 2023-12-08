using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace WPF
{
    internal class FerryAPIServices
    {
        /// <summary>
        /// Retrieves and deserializes a string from the FerryAPI
        /// </summary>
        /// <typeparam name="T">A Data Transfer Object supported by the FerryAPI</typeparam>
        /// <param name="URLendpoint">string representation of an Endpoint for the API</param>
        /// <returns>A deserialized object of the type T, returned by the call to the API</returns>
        public static T APIGet<T>(string URLendpoint)
        {
            HttpClient client = new HttpClient();

            try
            {
                Task<string> task = client.GetStringAsync(URLendpoint);
                string body = task.Result;

                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true,
                };
                return JsonSerializer.Deserialize<T>(body, option);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Posts to the specified FerryAPI endpoint and returns the deserialized response
        /// </summary>
        /// <typeparam name="T">A Data Transfer Object supported by the FerryAPI</typeparam>
        /// <param name="URLendpoint">string representation of an Endpoint for the API</param>
        /// <param name="ferryAPIObject">A Data Transfer Object to post, of a type supported by the FerryAPI </param>
        /// <returns>A deserialized object of the type T, returned by the call to the API</returns>
        public static T APIPost<T>(string URLendpoint, T ferryAPIObject)
        {

            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> task = client.PostAsJsonAsync<T>(URLendpoint, ferryAPIObject);
                HttpResponseMessage result = task.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IncludeFields = true,
                    };
                    return JsonSerializer.Deserialize<T>(result.Content.ReadAsStream(), option);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// HttpPut's to the specified FerryAPI endpoint and returns the deserialized response 
        /// </summary>
        /// <typeparam name="T">A Data Transfer Object supported by the FerryAPI</typeparam>
        /// <param name="URLendpoint">string representation of an Endpoint for the API</param>
        /// <param name="ferryAPIObject">A Data Transfer Object to post, of a type supported by the FerryAPI</param>
        /// <returns>A deserialized object of the type T, returned by the call to the API</returns>
        public static T APIPut<T>(string URLendpoint, T ferryAPIObject)
        {

            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> task = client.PutAsJsonAsync<T>(URLendpoint, ferryAPIObject);
                HttpResponseMessage result = task.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IncludeFields = true,
                    };
                    return JsonSerializer.Deserialize<T>(result.Content.ReadAsStream(), option);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
