using System.Collections.Generic;
using ClairesDaycareManager.Models;

namespace ClairesDaycareManager.Services;

public class DaycareDataService
{
    private readonly List<Child> _children = new()
    {
        new Child
        {
            Id = 1,
            Name = "Emma Johnson",
            Age = 4,
            Classroom = "Pre-K",
            IsScheduledToday = true,
            Allergies = new List<string> { "No known allergies" },
            ApprovedPickupContacts = new List<string> { "Anna Johnson", "Robert Johnson" }
        },
        new Child
        {
            Id = 2,
            Name = "Liam Smith",
            Age = 3,
            Classroom = "Toddler",
            IsScheduledToday = true,
            Allergies = new List<string> { "Peanut allergy" },
            ApprovedPickupContacts = new List<string> { "Sarah Smith", "Michael Smith" }
        },
        new Child
        {
            Id = 3,
            Name = "Sophia Brown",
            Age = 5,
            Classroom = "Pre-K",
            IsScheduledToday = true,
            Allergies = new List<string> { "Milk allergy" },
            ApprovedPickupContacts = new List<string> { "Mark Brown", "Lisa Brown" }
        },
        new Child
        {
            Id = 4,
            Name = "Noah Wilson",
            Age = 4,
            Classroom = "Pre-K",
            IsScheduledToday = false,
            Allergies = new List<string> { "Egg allergy" },
            ApprovedPickupContacts = new List<string> { "Emily Wilson" }
        }
    };

    public List<Child> GetAllChildren()
    {
        return _children;
    }

    public void AddChild(Child child)
    {
        _children.Add(child);
    }
}
