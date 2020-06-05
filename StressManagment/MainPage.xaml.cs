using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StressManagment
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        string _apiKey = "XMFXHc_XEQ2i-9T_Y_n_KC2q_Ek3PiiIQte0jJAw0b46";
        string _watsonUrl = "https://api.us-south.assistant.watson.cloud.ibm.com/instances/3946e7ab-ce52-40e7-96dc-47566261f6a5";
        string _assistantId = "2fb81788-042b-4df1-8a99-d3a6b6593270";
        public MainPage()
        {
            InitializeComponent();
        }



        public void CallWebAssistant()
        {
            var versionDate = "2020-06-05";// string.Format("YYYY-MM-DD",DateTime.Now.ToShortDateString());
            IamAuthenticator authenticator = new IamAuthenticator(
        apikey: _apiKey);
            var service = new AssistantService(versionDate, authenticator);
            service.SetServiceUrl(_watsonUrl);
            service.DisableSslVerification(true);
            var sessionResult = service.CreateSession(_assistantId);
            var sessionId = sessionResult.Result.SessionId;

            var result = service.Message(_assistantId, sessionId, input: new IBM.Watson.Assistant.v2.Model.MessageInput()
            {
                Text = "hello"
            });


            Console.WriteLine(result.Response);
        }


    }
}
