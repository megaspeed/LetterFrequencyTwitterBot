using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot
{
    public class TwitterEngine
    {
        private readonly TwitterService _twitterService;

        public TwitterEngine(ITwitterService twitterService)
        {
            _twitterService = twitterService as TwitterService;
        }

        public IEnumerable<TwitterMessage> LoadMessage(string account, int count)
        {
            var messages = new List<TwitterMessage>();
               var asyncOperation = _twitterService.ListTweetsOnUserTimeline(
                new ListTweetsOnUserTimelineOptions()
                {
                    Count = count,
                    ScreenName = account
                }, (tweet, response) =>
                {
                    if(tweet != null)
                        messages = tweet.Select(m => new TwitterMessage() { Text = m.Text }).ToList();
                }
            );
            asyncOperation.AsyncWaitHandle.WaitOne();

            TwitterResponceChecker.CheckResponse(_twitterService.Response);
            return messages;
        }

        public async Task SendTwitterMessageAsync(IEnumerable<string> messageText)
        {
            foreach (var message in messageText.Reverse())
            {
                await SendMessageAsync(message);
            }
        }

        private async Task<TwitterAsyncResult<TwitterStatus>> SendMessageAsync(string messageText)
        {
            var result = await _twitterService.SendTweetAsync(new SendTweetOptions() { Status = messageText });
            TwitterResponceChecker.CheckResponse(_twitterService.Response);
            return result;
        }
    }
}