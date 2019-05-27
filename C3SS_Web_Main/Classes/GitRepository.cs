using C3SS_Web_Main.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace C3SS_Web_Main.Classes
{
    public class GitRepository
    {

        //Github is unlikely to change urls in the short term so for easy of use all URI parts are hard coded here.
        //TODO: change the URLPARTS to be configurable
        #region URLPARTS
        private readonly string apiUrl = "https://api.github.com";
        private readonly string userController = "users";
        #endregion

        public SearchResultModel SearchForUser(string userName)
        {
            var searchResult = new SearchResultModel();
            searchResult.Success = false;
            try
            {
                
                if (string.IsNullOrWhiteSpace(userName))
                {
                    searchResult.Message = "Search string is required";
                    return searchResult;
                }
                if(userName.IndexOfAny(new char[] { '/', '?', '"','<','>','#','%','{','}','|','^','~','[',']','`' }) >= 0)
                {
                    searchResult.Message = "Search string contains illegal characters";                    
                    return searchResult;
                }              
                

                var response = HttpGet(string.Format("{0}/{1}/{2}", apiUrl, userController, userName));
                searchResult.Data = JsonConvert.DeserializeObject(response);
                searchResult.Success = true;

            }
            catch (WebException wex)
            {
                var resp = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                var errorData = JsonConvert.DeserializeObject<GitError>(resp);

                searchResult.Message = errorData.message;


            }
            catch(Exception ex)
            {
                searchResult.Message = "Unable to create searches at this time, if this persists please contact administration";

                //TODO log to a useful place log4net is good for file logging
            }

            return searchResult;
        }

        public string HttpGet(string Url)
        {
         
                WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Stream readStream = client.OpenRead(Url);
            StreamReader reader = new StreamReader(readStream);
            string retStr = reader.ReadToEnd();
            readStream.Close();
            reader.Close();

            return retStr;
        }


    }
}
