using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace vTools_Auth.src
{
    class SessionHandler
    {
        private Session sess;
        private RestClient client;
        public SessionHandler(Session session)
        {
            this.sess = session;
            this.client = new RestClient("https://valorantcc.000webhostapp.com/vii-chan/");
        }

        public async Task<String> SaveSession()
        {
            RestRequest request = new RestRequest("api.php", Method.Post);
            request.AddJsonBody(this.sess);
            RestResponse response = await this.client.ExecuteAsync(request);
            String content = response.Content;
            if (content.Contains("error")) return "An error has occured.";
            if (content.Contains("updated")) return "Session has been updated! You can now use Vii-chan again!";
            if (content.Contains("set")) return "Session has been listed! You can now use Vii-chan!";
            return ":V";
        }
    }
}
