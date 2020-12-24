using System;
using System.Collections.Generic;
using System.Web.Configuration;
using Umbraco.Core.Composing;
using SlackBotMessages;
using SlackBotMessages.Models;

namespace CleanBlog.Core.Composing
{
    public class ApplicationComposer:ComponentComposer<ApplicationComponent>,IUserComposer
    {

    }

    public class ApplicationComponent : IComponent
    {
        public void Initialize()
        {
            try
            {
                var client = new SbmClient(WebConfigurationManager.AppSettings["SlackBotMessagesWebHookUrl"]);

                var message = new Message
                {
                    Username = "Test User",
                    Text = "Website Started - Hello from Code Test",
                    //IconUrl = "https://s3-us-west-2.amazonaws.com/slack-files2/bot_icons/2019-06-17/669285832007_48.png"
                    IconEmoji = Emoji.Alien,
                    Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                         Fallback="Clean Blog website Started",
                         Color="good",
                         Fields=new List<Field>
                         {
                             new Field()
                             {
                                 Value="My first Umbraco Website has started"
                             }
                         }
                    }

                }
                };

                client.Send(message);
            }
            catch(Exception ex)
            {
                Current.Logger.Error(typeof(ApplicationComponent), ex, "Unable to send slack message to notify site starting up. ");
            }
            }
            
        public void Terminate()
        {

        }
    }
}
