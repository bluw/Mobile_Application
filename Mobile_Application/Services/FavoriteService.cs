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

        public async Task<Person[]> getFavoritesAsync(string email)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/getFavorite/?email=" + email);

            string json = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Person[]>(json);
        }


        public async Task AddFavoriteAsync(string email, string emailFavorite)
        {
            HttpClient client = new HttpClient();

            //empty string used for the post argument
            var linkForPost = Newtonsoft.Json.JsonConvert.SerializeObject("");
            HttpContent content = new StringContent(linkForPost, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://keyregisterweb.azurewebsites.net/api/people/addFavorite/?email=" + email + "&emailFavorite=" + emailFavorite, content);
        }


        public async Task RemoveFavoriteAsync(string email, string emailFavorite)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync("http://keyregisterweb.azurewebsites.net/api/people/deleteFavorite/?email=" + email + "&emailFavorite=" + emailFavorite);
        }

    }
}
