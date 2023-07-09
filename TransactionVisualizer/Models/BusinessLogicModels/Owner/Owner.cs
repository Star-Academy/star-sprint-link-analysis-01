using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessLogicModels;

public class Owner
{
    [Key] public long ID { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Owner owner && owner.ID == ID;
    }
}