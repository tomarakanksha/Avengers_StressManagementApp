// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace StressManagementBot
{
    public class StressManagementBot : ActivityHandler
    {


        string _apiKey = "6bg6hZ1GF2Z-2EQaL1xDmp2ZAm5k1VG77BGxeWMQnyU0";
        string _watsonUrl = "https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/f0e8fb68-e82e-489f-96cf-0c996ed4d390";
        string _assistantId = "8cc45599-f247-474c-84c9-8fbe70f2cc6d";


        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var responseText =turnContext.Activity.Text;
            var replyText = CallWebAssistant(responseText);
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken); ;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {

                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
        public string CallWebAssistant( string textInput)
        {
            var versionDate = "2020-06-05";// string.Format("YYYY-MM-DD",DateTime.Now.ToShortDateString());
            IamAuthenticator authenticator = new IamAuthenticator(
        apikey: _apiKey);
            var service = new AssistantService(versionDate, authenticator);
            service.SetServiceUrl(_watsonUrl);
            service.DisableSslVerification(true);
            var sessionResult = service.CreateSession(_assistantId);
            var sessionId = sessionResult.Result.SessionId;
            var messageInput = new MessageInput() { Text = textInput };
           
            var result = service.Message(_assistantId, sessionId, input: messageInput);
           var generic = result.Result.Output.Generic;

            var response = GetWatsonResponse(generic);
            return response;

        }

        public string GetWatsonResponse(List<RuntimeResponseGeneric> list)
        {
            string response = "";
            foreach (var item in list)
            {
                if (item.ResponseType == "text")
                {
                    response = item.Text;
                }
            }
            return response;

        }


    }
}
