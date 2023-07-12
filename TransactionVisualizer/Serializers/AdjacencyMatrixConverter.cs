using System.Text.Json;
using System.Text.Json.Serialization;
using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Serializers;

public class AdjacencyMatrixConverter<TVertex, TEdge> : JsonConverter<Dictionary<TVertex, List<Edge<TVertex, TEdge>>>>
    where TVertex : class
    where TEdge : class
{
    public override Dictionary<TVertex, List<Edge<TVertex, TEdge>>> Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        // Load the JSON document
        using var document = JsonDocument.ParseValue(ref reader);

        // Extract the adjacencyMatrix object from the JSON document
        var adjacencyMatrixElement = document.RootElement.GetProperty("adjacencyMatrix");

        // Create a dictionary to hold the deserialized adjacency matrix
        var adjacencyMatrix = new Dictionary<TVertex, List<Edge<TVertex, TEdge>>>();

        // Deserialize each vertex and its associated edges
        foreach (var vertexProperty in adjacencyMatrixElement.EnumerateObject())
        {
            var vertex = JsonSerializer.Deserialize<TVertex>(vertexProperty.Name, options);
            var edgesElement = vertexProperty.Value;

            // Deserialize the list of edges for the current vertex
            var edges = new List<Edge<TVertex, TEdge>>();
            foreach (var edgeElement in edgesElement.EnumerateArray())
            {
                var edge = JsonSerializer.Deserialize<Edge<TVertex, TEdge>>(edgeElement.GetRawText(), options)!;

                edges.Add(edge);
            }

            adjacencyMatrix.Add(vertex!, edges);
        }

        return adjacencyMatrix;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<TVertex, List<Edge<TVertex, TEdge>>> value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("adjacencyMatrix");
        writer.WriteStartObject();

        foreach (var pair in value)
        {
            writer.WritePropertyName(pair.Key.ToString()!);
            writer.WriteStartArray();

            foreach (var edge in pair.Value)
            {
                JsonSerializer.Serialize(writer, edge, options);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}