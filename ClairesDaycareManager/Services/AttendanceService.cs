using System.Collections.Generic;
using System.Linq;
using ClairesDaycareManager.Models;

namespace ClairesDaycareManager.Services;

public class AttendanceService
{
    private readonly List<Child> _children;

    public AttendanceService(List<Child> children)
    {
        _children = children;
    }

    public List<Child> GetScheduledChildren()
    {
        return _children.Where(child => child.IsScheduledToday).ToList();
    }

    public Child? FindChild(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (int.TryParse(input, out int id))
        {
            return _children.FirstOrDefault(child => child.Id == id);
        }

        return _children.FirstOrDefault(child =>
            child.Name.Contains(input, System.StringComparison.OrdinalIgnoreCase));
    }

    public string GetAttendanceStatus(string? input)
    {
        Child? child = FindChild(input);

        if (child == null)
        {
            return "Child not found.";
        }

        return child.IsScheduledToday
            ? $"{child.Name} is scheduled to attend today."
            : $"{child.Name} is NOT scheduled to attend today.";
    }
}
