using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using TransactionVisualizer.Models.Graph;

public class AdjacencyMatrixConverter<TVertex, TEdge> : JsonConverter<Dictionary<TVertex, List<Edge<TVertex, TEdge>>>>
    where TVertex : class
    where TEdge : class
{
    public override Dictionary<TVertex, List<Edge<TVertex, TEdge>>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Load the JSON document
        using JsonDocument document = JsonDocument.ParseValue(ref reader);

        // Extract the adjacencyMatrix object from the JSON document
        JsonElement adjacencyMatrixElement = document.RootElement.GetProperty("adjacencyMatrix");

        // Create a dictionary to hold the deserialized adjacency matrix
        var adjacencyMatrix = new Dictionary<TVertex, List<Edge<TVertex, TEdge>>>();

        // Deserialize each vertex and its associated edges
        foreach (JsonProperty vertexProperty in adjacencyMatrixElement.EnumerateObject())
        {
            TVertex vertex = JsonSerializer.Deserialize<TVertex>(vertexProperty.Name, options);
            JsonElement edgesElement = vertexProperty.Value;

            // Deserialize the list of edges for the current vertex
            List<Edge<TVertex, TEdge>> edges = new List<Edge<TVertex, TEdge>>();
            foreach (JsonElement edgeElement in edgesElement.EnumerateArray())
            {
                Edge<TVertex, TEdge> edge = JsonSerializer.Deserialize<Edge<TVertex, TEdge>>(edgeElement.GetRawText(), options);
                edges.Add(edge);
            }

            adjacencyMatrix.Add(vertex, edges);
        }

        return adjacencyMatrix;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<TVertex, List<Edge<TVertex, TEdge>>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("adjacencyMatrix");
        writer.WriteStartObject();

        foreach (KeyValuePair<TVertex, List<Edge<TVertex, TEdge>>> pair in value)
        {
            writer.WritePropertyName(pair.Key.ToString());
            writer.WriteStartArray();

            foreach (Edge<TVertex, TEdge> edge in pair.Value)
            {
                JsonSerializer.Serialize(writer, edge, options);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}
