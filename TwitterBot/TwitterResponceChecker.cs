using System;
using System.Linq;
using TweetSharp;

namespace TwitterBot
{
    public static class TwitterResponceChecker
    {
        public static void CheckResponse(TwitterResponse response)
        {
            if (response.Errors != null)
            {
                var message = "Response error" + response.Errors.errors.Select(e => e.ToString())
                    .Aggregate((l, r) => l + Environment.NewLine + r);
                throw new Exception(message);
            }

        }
    }
}