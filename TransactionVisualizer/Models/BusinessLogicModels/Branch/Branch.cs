using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.Branch;

public class Branch
{
    [Key] public long Id { get; set; }
    public string Telephone { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}