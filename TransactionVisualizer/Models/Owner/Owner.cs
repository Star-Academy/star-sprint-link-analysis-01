using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models;

public class Owner
{
    [Key] public long Id { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }
}