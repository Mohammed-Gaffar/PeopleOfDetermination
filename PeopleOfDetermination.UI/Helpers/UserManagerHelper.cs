using Newtonsoft.Json;
using System.Net;

namespace PeopleOfDetermination.UI.Helpers
{
    public class UserManagerHelper
    {
        private readonly IConfiguration _configuration;

        public UserManagerHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public KFUUserModel GetUserDirectoryEntry(string username)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["BaseUrl"]);
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BaseUrl"));

                //client.DefaultRequestHeaders.Add("Authorization", WebConfigurationManager.AppSettings["Token"]);
                client.DefaultRequestHeaders.Add("Authorization", _configuration.GetValue<string>("Token"));

                var response = client.GetAsync("APIServices/api/GetADUserInfo?" + "UserName=" + username + "");

                if (response.Result.IsSuccessStatusCode && response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = response.Result.Content.ReadAsStringAsync();
                    KFUUserModel _kfuUserModel = JsonConvert.DeserializeObject<KFUUserModel>(result.Result);
                    return _kfuUserModel;
                }
                else
                {
                    return new KFUUserModel();
                }
            }
        }
        public class KFUUserModel
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string NameEn { get; set; }
            public object DOB { get; set; }
            public string Nationality { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string NationalID { get; set; }
            public string OU { get; set; }
            public string EmpType { get; set; }
            public string EmployeeID { get; set; }
            public string JobTitle { get; set; }
        }
    }
}