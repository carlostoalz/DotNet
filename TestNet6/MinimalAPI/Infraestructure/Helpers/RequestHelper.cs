namespace Infraestructure.Helpers
{
    public static class RequestHelper
    {
        public static string BuildURLQueryParameters<T>(string url,T param)
        {
            int cont = 0;
            param.GetType().GetProperties().ToList().ForEach(p =>
            {
                if (cont == 0)
                    url = $"{ url }?{ p.Name }={ param.GetType().GetProperty(p.Name).GetValue(param) }";
                else
                    url = $"{ url }&{ p.Name }={ param.GetType().GetProperty(p.Name).GetValue(param) }";
                cont++;
            });
            return url;
        }

        public static IEnumerable<KeyValuePair<string, string>> BuildFormUrlEncodedData<T>(T postData)
        {
            List<KeyValuePair<string, string>> content = new();
            typeof(T).GetProperties().ToList().ForEach(p => content.Add(new(p.Name, postData.GetType().GetProperty(p.Name).GetValue(postData).ToString())));
            return content;
        }
    }
}
