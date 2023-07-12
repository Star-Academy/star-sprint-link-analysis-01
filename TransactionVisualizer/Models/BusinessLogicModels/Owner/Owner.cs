using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Owner;

public class Owner
{
    [Key] public long Id { get; init; }
    public string Name { get; init; }
    public string FamilyName { get; init; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}