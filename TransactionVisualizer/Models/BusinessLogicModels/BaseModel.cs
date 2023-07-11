using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models;

public abstract class BaseModel
{
    [Key] public long Id { get; set; }
}