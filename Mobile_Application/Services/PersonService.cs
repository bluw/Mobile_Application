using Mobile_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Services
{
    class PersonService
    { 
        public async Task AddPersonAsync(Person newPerson) // controle: mettre les premieres lettre en maj 
        {
            HttpClient client = new HttpClient();

            var newPersonJSON = Newtonsoft.Json.JsonConvert.SerializeObject(newPerson);
            HttpContent content = new StringContent(newPersonJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://keyregisterweb.azurewebsites.net/api/people/addPerson", content);
        }

        public async Task UpdatePersonAsync(Person newPerson)
        {
            HttpClient client = new HttpClient();

            var newPersonJSON = Newtonsoft.Json.JsonConvert.SerializeObject(newPerson);
            HttpContent content = new StringContent(newPersonJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("http://keyregisterweb.azurewebsites.net/api/people/addPerson", content);
        }


        public async Task<Person> getDetailsPersonAsync(string email)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/searchPersonByEmail/?email=" + email);

            string json = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(json);
        }


        public async Task<Person[]> searchPersonByCompanyAsync(string nameCompany) // controle: mettre la premiere lettre en maj 
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/searchPersonByCompany/?nameCompany=" + nameCompany);

            string json = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Person[]>(json);
        }


        public async Task<Person[]> searchPersonByNameAsync(string name) // controle: mettre la premiere lettre en maj 
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/searchPersonByName/?lastName=" + name);

            string json = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Person[]>(json);
        }

    }
}
