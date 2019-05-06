using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VOAprototype.Models;

namespace VOAprototype.Classification
{
    public static class Classifier
    {

        private static readonly Uri URI = new Uri("http://gmubbgpython.azurewebsites.net");

        public static void Init(VOAprototypeContext context)
        {
            Reset().Wait();
            var itfunctions = from itf in context.ITFunction select itf;

            foreach (ITFunction itfunction in itfunctions)
            {
                Add(itfunction.Name, itfunction.Unigram).Wait();
            }
            Train();
        }

        private static async Task<string> GetRequest(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = URI;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<string> PostRequest(string url, string body)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = URI;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            StringContent bodyContent = new StringContent(body);
            HttpResponseMessage response = await client.PostAsync(url, bodyContent);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> Filter(string description)
        {
            return await PostRequest("/filter", description);
        }

        public static async Task<string> Reset()
        {
            Debug.WriteLine("RESET");
            return await GetRequest("/reset");
        }

        public static async Task<string> Add(string name, string unigram)
        {
            Debug.WriteLine("ADD: " + name + ";");
            string result = null;
            if (name != null && unigram != null)
            {
                result = await PostRequest("/map/" + name.Replace(" ", "_").Replace("/", ""), unigram);
            }
            return result;
        }

        public static async Task<string> Wiki(string wikiname)
        {
            string result = null;
            result = await PostRequest("/wikipedia", wikiname);
            return result;
        }

        public static async Task<string> Train()
        {
            Debug.WriteLine("TRAIN");
            return await GetRequest("/train");
        }

        public static async Task<string> Learn(string name, string description)
        {
            string result = null;
            if (name != null && description != null)
            {
                result = await PostRequest("/learn/" + name.Replace(" ", "_").Replace("/", ""), description);
            }
            return result;
        }

        public static async Task<List<string>> Classify(string application, string description)
        {
            Debug.WriteLine("CLASSIFY: " + application + "; ");
            string result = null;
            if (application != null && description != null)
            {
                result = await PostRequest("/classify/" + application.Replace(" ", "_").Replace("/",""), description);
            }
            return result.Replace("[", string.Empty)
                         .Replace("]", string.Empty)
                         .Replace("'", string.Empty)
                         .Split(";")
                         .ToList<string>();
        }
    }
}
