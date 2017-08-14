using System;
using System.Configuration;
using System.Linq;
using Ninject;
using Ninject.Modules;
using TweetSharp;
using TwitterBot;

namespace BotConsole
{
    public class TwitterServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ApplicationTwitterServiceProvider>().ToMethod(x => GetTwitterServiceProviderWithCredentials()).InSingletonScope();
            this.Bind<ITwitterService>().ToProvider<ApplicationTwitterServiceProvider>().InSingletonScope();
            this.Bind<LetterFrequencyTwitterBot>().ToSelf().WithConstructorArgument(Kernel.Get<TwitterEngine>());
        }

        private static ApplicationTwitterServiceProvider GetTwitterServiceProviderWithCredentials()
        {
            var config = ConfigurationManager.AppSettings;
            if (config.AllKeys.Contains("consumerKey") && config.AllKeys.Contains("consumerSecret") &&
                config.AllKeys.Contains("accessToken") && config.AllKeys.Contains("accessTokenSecret"))
            {
                var provider = new ApplicationTwitterServiceProvider(
                    config["consumerKey"], 
                    config["consumerSecret"], 
                    config["accessToken"], 
                    config["accessTokenSecret"]);
                return provider;

            }
            throw new Exception("Заполните данные для подключения в файле ");
        }
    }
}