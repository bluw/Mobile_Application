﻿using Mobile_Application.Encryption;
using Mobile_Application.Exceptions;
using Mobile_Application.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Mobile_Application.Services
{
    class LoginService
    {
        
        public async Task<bool> VerifiyLoginAsync(string email, string password)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/people/searchPersonByEmail/?email=" + email);

                string json = await response.Content.ReadAsStringAsync();
                var userFromDB = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(json);

                string cryptedPassword = MyCrypt(password);

                if (cryptedPassword.Equals(userFromDB.Password))
                {
                    return true;
                }

                return false;
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                string str = loader.GetString("noNetwork");
                throw new NoNetworkException(str);
            }
        }


        private string MyCrypt(string password)
        { 
            string cryptPass = PasswordEncryption.cryptPwd(password);
            return cryptPass;
        }
    }
}
