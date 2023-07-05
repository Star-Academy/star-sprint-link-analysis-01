using System.Collections.Generic;

namespace TransactionVisualizer.DataRepository;

public class DataGainResponse<TResponse>
{
    public bool Error { get; set; }
    public List<TResponse>? Items { get; set; }
}