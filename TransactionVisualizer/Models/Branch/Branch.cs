namespace TransactionVisualizer.Models;

public class Branch
{
    public string Telephone { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public Branch(string telephone, string name, string address)
    {
        Telephone = telephone;
        Name = name;
        Address = address;
    }
}