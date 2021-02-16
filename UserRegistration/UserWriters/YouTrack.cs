using YouTrackSharp;
using YouTrackSharp.Users;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;

namespace UserRegistration.UserWriters
{
    class YouTrack : IUserWriter
    {
        public void UserAdd(/*string username, string fullname, string email, string password*//*, string groupName*/)
        {
            RestClient client = new RestClient("https://rapebot.myjetbrains.com/hub/api/rest/");
            JwtAuthenticator authentication = new JwtAuthenticator("asdasdasdsadasd");
            client.Authenticator = authentication;

            RestRequest request = new RestRequest("users", Method.PUT);
            request.AddParameter("login", "steel1337");
            client.Execute(request);
        }
    }
}
