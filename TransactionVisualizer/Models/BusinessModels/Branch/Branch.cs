using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessModels;

public class Branch
{
    [Key]
    public long Id { get; set; }
    public string Telephone { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Branch branch && branch.Id == Id;
    }
    
}