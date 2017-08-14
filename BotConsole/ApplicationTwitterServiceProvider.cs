using Ninject.Activation;
using TweetSharp;

namespace BotConsole
{
    /// <summary>
    /// Предоставляет TwitterService с данными авторизации
    /// </summary>
    public class ApplicationTwitterServiceProvider : Provider<TwitterService>
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;

        public ApplicationTwitterServiceProvider(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
        }

        protected override TwitterService CreateInstance(IContext context)
        {
            var service = new TwitterService();
            service.AuthenticateWith(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
            return service;
        }
    }
}