using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject;
using Statistics;

namespace TwitterBot
{
    public class LetterFrequencyTwitterBot
    {
        private const int TwittCountForStatCompute = 5;

        private readonly TwitterEngine _engine;

        public LetterFrequencyTwitterBot(TwitterEngine engine)
        {
            this._engine = engine;
        }

        public async Task<string> ProcessTweetsAsync(string twitterAccountName)
        {
            var messages = _engine.LoadMessage(twitterAccountName, TwittCountForStatCompute);

            var statisticCalculator = new FrequencyStatisticCalculator();
            var statistic = statisticCalculator.ComputeStatistic(messages.Select(m => m.Text));

            var statInfo = JsonConvert.SerializeObject(statistic);
            var messageText =
                $"«{twitterAccountName}, статистика для последних {TwittCountForStatCompute} твитов: {statInfo}».";
            var messageSequence = TweetMessageSplitter.Split(messageText);
            await _engine.SendTwitterMessageAsync(messageSequence);
            return messageText;
        }

    }
}