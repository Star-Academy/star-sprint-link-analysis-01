using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessLogicModels;

public class Branch
{
    [Key] public long Id { get; set; }
    public string Telephone { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

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