using System.Linq;
using ClairesDaycareManager.Models;

namespace ClairesDaycareManager.Services;

public class PickupService
{
    private readonly AttendanceService _attendanceService;

    public PickupService(AttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    public Child? FindChildForPickup(string? input)
    {
        return _attendanceService.FindChild(input);
    }

    public bool IsPickupApproved(Child child, string? pickupName)
    {
        if (string.IsNullOrWhiteSpace(pickupName))
        {
            return false;
        }

        return child.ApprovedPickupContacts.Any(contact =>
            contact.Equals(pickupName, System.StringComparison.OrdinalIgnoreCase));
    }
}
