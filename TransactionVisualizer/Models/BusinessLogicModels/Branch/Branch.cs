using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessModels;

public class Branch
{
    [Key] public long Id { get; set; }
    public string Telephone { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }

    public override bool Equals(object? obj)
    {
        var branch = obj as Branch;

        return branch != null && branch.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}