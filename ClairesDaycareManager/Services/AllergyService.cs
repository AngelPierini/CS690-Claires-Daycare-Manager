using ClairesDaycareManager.Models;

namespace ClairesDaycareManager.Services;

public class AllergyService
{
    private readonly AttendanceService _attendanceService;

    public AllergyService(AttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    public Child? FindChildForAllergyCheck(string? input)
    {
        return _attendanceService.FindChild(input);
    }

    public bool HasAllergyWarning(Child child)
    {
        foreach (string allergy in child.Allergies)
        {
            if (!allergy.Equals("No known allergies", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
