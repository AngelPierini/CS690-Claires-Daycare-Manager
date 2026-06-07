using System.Collections.Generic;

namespace ClairesDaycareManager.Models;

public class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Classroom { get; set; } = "";
    public bool IsScheduledToday { get; set; }
    public List<string> Allergies { get; set; } = new();
    public List<string> ApprovedPickupContacts { get; set; } = new();
}
