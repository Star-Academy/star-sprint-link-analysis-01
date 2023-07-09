using System.Collections.Generic;
using System.Linq;

namespace TransactionVisualizer.Models.Graph
{
    public class CustomGraph<TVertex, TEdge> where TVertex : class where TEdge : class
    {
        public Dictionary<TVertex, List<Edge<TVertex, TEdge>>> adjacencyMatrix { get; }

        public CustomGraph()
        {
            adjacencyMatrix = new Dictionary<TVertex, List<Edge<TVertex, TEdge>>>();
        }

        public void AddEdge(Edge<TVertex, TEdge> edge)
        {
            if (adjacencyMatrix.TryGetValue(edge.Source, out var value))
            {
                value.Add(edge);
            }
            else
            {
                adjacencyMatrix.Add(edge.Source, new List<Edge<TVertex, TEdge>>());
                adjacencyMatrix[edge.Source].Add(edge);
            }

            if (!adjacencyMatrix.ContainsKey(edge.Destination))
            {
                adjacencyMatrix.Add(edge.Destination, new List<Edge<TVertex, TEdge>>());
            }
        }
        
        
    }
}