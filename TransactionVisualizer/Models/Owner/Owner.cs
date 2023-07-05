using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TransactionVisualizer.Models;

public class Owner
{
    [Key]
    public long ID { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }

   
}