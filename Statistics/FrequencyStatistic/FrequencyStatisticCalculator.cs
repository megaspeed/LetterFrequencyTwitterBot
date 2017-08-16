using System;
using System.Collections.Generic;
using System.Linq;

namespace Statistics
{
    public class FrequencyStatisticCalculator : IComputeStatistic<FrequencyStatistic, string>
    {
        public FrequencyStatistic ComputeStatistic(IEnumerable<string> messages)
        {
            var allChars = string.Join("", messages).Where(Char.IsLetter).Select(Char.ToLower).ToArray();
            var frequencyStatistic = new FrequencyStatistic();

            if (allChars.Length == 0)
                return frequencyStatistic;

            var uniqChars = allChars.Distinct();
            foreach (var uniqChar in uniqChars)
            {
                var stat = allChars.Count(ch => ch == uniqChar);
                frequencyStatistic.Add(uniqChar, (double)stat/allChars.Length);
            }

            return frequencyStatistic;
        }
    }
}