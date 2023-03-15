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
using System.Runtime.Remoting.Messaging;
using RestSharpHelper.OAuth1;
using DiscogsClient;
using DiscogsClient.Data.Query;
using DiscogsClient.Internal;
using DiscogsClient.Data.Result;

namespace musicApp.apiCall
{
    internal static class GetApi
    {
        public static RootPixa GetPixabay()
        {
            WebRequest resquest = HttpWebRequest.Create("https://pixabay.com/api/?key=33423123-b42832bf6ee20d22ac1dc20f2&category=music&image_type=photo&pretty=true&min_width=900&min_height=600&per_page=100&colors=black");
            WebResponse response = resquest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string pixabay_json = reader.ReadToEnd();
            RootPixa pixabay = JsonConvert.DeserializeObject<RootPixa>(pixabay_json);

            return pixabay;
        }

        public static async Task<DiscogsSearchResults> GetDiscogs(Parametre parametre)
        {
            string token = "hYUJPfyinwOXxHxIRCaNUmdGhKAHxBmMHtFUuxiw";
            var tokenInformation = new TokenAuthenticationInformation(token);
            {
                //Create discogs client using the authentication
                var discogsClient = new DiscogsClient.DiscogsClient(tokenInformation);

                var discogsSearch = new DiscogsSearch()
                {
                    artist = parametre.artist,
                    format = parametre.format,
                };
                var res = await SearchDis(discogsClient, discogsSearch).ConfigureAwait(false);
                //return res.Result;
                return res;
            }
        }

        private async static Task<DiscogsSearchResults> SearchDis(DiscogsClient.DiscogsClient dc, DiscogsSearch ds)
        {
            var res = await dc.SearchAsync(ds).ConfigureAwait(false);
            return res;
        }
    }
}
