using System.Collections.Generic;

namespace Statistics
{
    public interface IComputeStatistic<out TStatistic, in TMessage> 
        where TStatistic : class 
        where TMessage : class
    {
        TStatistic ComputeStatistic(IEnumerable<TMessage> messages);
    }
}