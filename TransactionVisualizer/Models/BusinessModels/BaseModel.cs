using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models.BusinessModels;

public abstract class BaseModel
{
    [Key] public long Id { get; set; }
}