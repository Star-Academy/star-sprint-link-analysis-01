using System.Collections.Generic;

namespace TransactionVisualizer.Models.Graph;

public class CustomGraph<TVertex, TEdge> where TVertex : class where TEdge : class
{
    public List<TVertex> Vertex { get; }
    public List<Edge<TVertex, TEdge>> Edge { get; }

    public CustomGraph()
    {
        Vertex = new List<TVertex>();
        Edge = new List<Edge<TVertex, TEdge>>();
    }

    public void AddVertex(TVertex vertex)
    {
        Vertex.Add(vertex);
    }

    public void AddEdge(Edge<TVertex, TEdge> edge)
    {
        Edge.Add(edge);
    }
}