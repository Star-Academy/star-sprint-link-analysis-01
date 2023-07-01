namespace TransactionVisualizer.Models;

public class Owner
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }

    public Owner(long id, string name, string familyName)
    {
        ID = id;
        Name = name;
        FamilyName = familyName;
    }
}