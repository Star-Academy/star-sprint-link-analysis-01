using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessLogicModels;

public class Owner
{
    [Key] public long Id { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }

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