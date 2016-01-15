using Mobile_Application.Exceptions;
using Mobile_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Services
{
    class FavoriteService
    {
        public Boolean isUserConnected()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
        public async Task<Person[]> getFavoritesAsync(string email)
        {
            if (isUserConnected())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/getFavorite/?email=" + email);

                string json = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Person[]>(json);
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                string str = loader.GetString("noNetwork");
                throw new NoNetworkException(str);
            }
        }


        public async Task<bool> AddFavoriteAsync(string email, string emailFavorite)
        {
            if (isUserConnected())
            {
                HttpClient client = new HttpClient();

                //empty string used for the post argument
                var linkForPost = Newtonsoft.Json.JsonConvert.SerializeObject("");
                HttpContent content = new StringContent(linkForPost, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://keyregisterweb.azurewebsites.net/api/people/addFavorite/?email=" + email + "&emailFavorite=" + emailFavorite, content);

                return response.IsSuccessStatusCode;
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                string str = loader.GetString("noNetwork");
                throw new NoNetworkException(str);
            }
        }


        public async Task RemoveFavoriteAsync(string email, string emailFavorite)
        {
            if (isUserConnected())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync("http://keyregisterweb.azurewebsites.net/api/people/deleteFavorite/?email=" + email + "&emailFavorite=" + emailFavorite);

            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                string str = loader.GetString("noNetwork");
                throw new NoNetworkException(str);
            }
        }
    }
}
