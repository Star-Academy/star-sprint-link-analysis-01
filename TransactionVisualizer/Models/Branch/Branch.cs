using System.ComponentModel.DataAnnotations;

namespace TransactionVisualizer.Models;

public class Branch
{
    
    [Key]
    public long Id { get; set; }
    public string Telephone { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }


}