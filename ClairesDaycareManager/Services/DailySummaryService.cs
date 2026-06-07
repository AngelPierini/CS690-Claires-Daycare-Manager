using System.Collections.Generic;
using System.Linq;
using ClairesDaycareManager.Models;

namespace ClairesDaycareManager.Services;

public class DailySummaryService
{
    private readonly List<Child> _children;

    public DailySummaryService(List<Child> children)
    {
        _children = children;
    }

    public int GetScheduledChildrenCount()
    {
        return _children.Count(child => child.IsScheduledToday);
    }

    public int GetAllergyWarningCount()
    {
        return _children.Count(child =>
            child.Allergies.Any(allergy =>
                !allergy.Equals("No known allergies", System.StringComparison.OrdinalIgnoreCase)));
    }

    public List<string> GetImportantWarnings()
    {
        List<string> warnings = new();

        foreach (Child child in _children)
        {
            foreach (string allergy in child.Allergies)
            {
                if (!allergy.Equals("No known allergies", System.StringComparison.OrdinalIgnoreCase))
                {
                    warnings.Add($"{child.Name} has allergy warning: {allergy}");
                }
            }

            if (!child.IsScheduledToday)
            {
                warnings.Add($"{child.Name} is not scheduled to attend today.");
            }
        }

        return warnings;
    }
}
