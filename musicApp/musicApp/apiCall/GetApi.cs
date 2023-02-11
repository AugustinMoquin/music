using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Data;
using Microsoft.SqlServer.Server;

namespace musicApp.apiCall
{
    internal static class GetApi
    {
        public static RootPixa GetPixabay()
        {
            WebRequest resquest = HttpWebRequest.Create("https://pixabay.com/api/?key=33423123-b42832bf6ee20d22ac1dc20f2&category=music&image_type=photo&pretty=true&min_width=900&min_height=600&per_page=100");
            WebResponse response = resquest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string pixabay_json = reader.ReadToEnd();
            RootPixa pixabay = Newtonsoft.Json.JsonConvert.DeserializeObject<RootPixa>(pixabay_json);

            return pixabay;
        }

        public static async Task<RootDisc> GetDiscogs(string parametre)
        {
            // Initialization.  
            RootDisc responseObj = new RootDisc();
            var client = new HttpClient();

            // Setting Base address.  
            client.BaseAddress = new Uri("https://api.discogs.com/");

            // Setting content type.  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // HTTP GET  
            HttpResponseMessage response = await client.GetAsync("database/search?key=VsuLTyjYmHdQAdebSyLD&secret=AxSajuVrIJgxbQPgckKYKNfJWOHLWoZM" + parametre).ConfigureAwait(false);

            // Verification  
            if (response.IsSuccessStatusCode)
            {
                // Reading Response.  
                string result = response.Content.ReadAsStringAsync().Result;
                responseObj = JsonConvert.DeserializeObject<RootDisc>(result);
            }
            return responseObj;
        }

        /*public static async Task<RootDisc> GetDiscogs(string parametre)
        {
            //string parametre = "&q=Nirvana&"
            //&q = Nirvana & format = Vinyl
            string url = "https://api.discogs.com/database/search?";
            string key = "key=VsuLTyjYmHdQAdebSyLD";
            string secret = "secret=AxSajuVrIJgxbQPgckKYKNfJWOHLWoZM";
            WebRequest resquest = HttpWebRequest.Create(url + "&" + key + "&" + secret + "&" + parametre);
            WebResponse response = resquest.GetResponseAsync().Result;
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string discogs_json = reader.ReadToEnd();
            RootDisc discogs = Newtonsoft.Json.JsonConvert.DeserializeObject<RootDisc>(discogs_json);

            return discogs;
         
        }*/
    }
}
