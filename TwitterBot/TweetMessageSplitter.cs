using System.Collections.Generic;

namespace TwitterBot
{
    public static class TweetMessageSplitter
    {
        private const int MaxTweetLength = 140;

        public static IEnumerable<string> Split(string message)
        {
            if(message.Length <= MaxTweetLength)
                return new List<string>{message};
            var result = new List<string>();

            var startIndex = 0;
            while (message.Length >= startIndex + MaxTweetLength)
            {
                var chunk = message.Substring(startIndex, MaxTweetLength);
                result.Add(chunk);
                startIndex += MaxTweetLength;
            }

            return result;
        }

    }
}