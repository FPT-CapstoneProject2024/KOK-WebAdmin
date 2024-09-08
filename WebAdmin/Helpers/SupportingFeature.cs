using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using WebAdmin.DTOModels;

namespace WebAdmin.Helpers
{
    public class SupportingFeature
    {
        private static SupportingFeature instance = null;
        private static readonly object InstanceClock = new object();

        public static SupportingFeature Instance
        {
            get
            {
                lock (InstanceClock)
                {
                    if (instance == null)
                    {
                        instance = new SupportingFeature();
                    }
                    return instance;
                }
            }
        }

        public string GenerateCode()
        {
            return Convert.ToString(new Random().Next(100000, 999999));
        }

        public static IEnumerable<string> GetNameOfProperties<T>()
        {
            return typeof(T).GetProperties().Select(p => p.Name);
        }

        public static IEnumerable<string> GetNameIncludedProperties<T>()
        {
            return typeof(T).GetProperties()
                .Where(x => x.PropertyType.IsGenericType)
                .Where(x => x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                .Select(p => p.Name);
        }

        public static Expression<Func<TEntity, TKey>> GetPropertyExpression<TEntity, TKey>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(object), "p");
            var property = Expression.Property(Expression.Convert(parameter, typeof(TEntity)), propertyName);
            var convert = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, TKey>>(convert, parameter);
        }

        public static Type GetTypeProperty<T>(string propertyName)
        {
            return typeof(T).GetProperties().First(p => p.Name.Equals(propertyName)).PropertyType;
        }

        public static IEnumerable<T> Sorting<T>(IEnumerable<T> searchResult, SortOrder sortType, string colName)
        {
            if (sortType == SortOrder.Ascending)
            {
                return searchResult.OrderBy(item => typeof(T).GetProperties().First(x => x.Name.Equals(colName, StringComparison.CurrentCultureIgnoreCase)).GetValue(item)).AsQueryable();
            }
            else if (sortType == SortOrder.Descending)
            {
                return searchResult.OrderByDescending(item => typeof(T).GetProperties().First(x => x.Name.Equals(colName, StringComparison.CurrentCultureIgnoreCase)).GetValue(item)).AsQueryable();
            }
            else
            {
                return searchResult;
            }
        }

        public string? GetDisplayNameForProperty(PropertyInfo property, string otherProperty)
        {
            IEnumerable<Attribute> attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
            foreach (Attribute attribute in attributes)
            {
                if (attribute is DisplayAttribute display)
                {
                    return display.GetName();
                }
            }

            return otherProperty;
        }

        public Dictionary<int, string> GetEnumName<TEnum>()
        {
            Dictionary<int, string> enumValues = new Dictionary<int, string>();

            foreach (int e in Enum.GetValues(typeof(TEnum)))
            {
                enumValues.Add(e, Enum.GetName(typeof(TEnum), e));
            }

            return enumValues;
        }

        public string GetDataFromCache(IMemoryCache cache, string cacheKey)
        {
            string data = "";
            try
            {
                data = String.Concat(cache.Get(cacheKey));
            }
            catch (Exception)
            {
                return null;
            }

            return data;
        }

        public bool SetDataToCache(IMemoryCache cache, string key, string value, int minutes)
        {
            try
            {
                cache.Set(key, value, new TimeSpan(0, minutes, 0));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RemoveDataFromCache(IMemoryCache cache, string cacheKey)
        {
            try
            {
                cache.Remove(cacheKey);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public async Task<(bool, string)> UploadImage(IHttpClientFactory clientFactory, IFormFile file, string ImgurClientId)
        {
            if (file == null || file.Length == 0)
            {
                return (false, "Vui lòng upload ảnh của bạn");
            }

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "image", file.FileName);

                var client = clientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Client-ID", ImgurClientId);

                var response = await client.PostAsync("https://api.imgur.com/3/image", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    string imageUrl = jsonResponse.data.link;
                    return (true,  imageUrl);
                }
                else
                {
                    return (false, "Upload failed!");
                }
            }
            return (false, "");
        }
    }
}
