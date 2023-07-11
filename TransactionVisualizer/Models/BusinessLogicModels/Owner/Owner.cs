using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessModels;

public class Owner
{
    [Key] public long Id { get; init; }
    public string Name { get; init; }
    public string FamilyName { get; init; }

    public override bool Equals(object? obj)
    {
        var owner = obj as Owner;

        return owner != null && owner.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}