using System.Collections.Generic;
using ClairesDaycareManager.Models;
using ClairesDaycareManager.Services;
using Xunit;

namespace ClairesDaycareManager.Tests;

public class ServiceTests
{
    private List<Child> GetSampleChildren()
    {
        return new List<Child>
        {
            new Child
            {
                Id = 1,
                Name = "Emma Johnson",
                Age = 4,
                Classroom = "Pre-K",
                IsScheduledToday = true,
                Allergies = new List<string> { "No known allergies" },
                ApprovedPickupContacts = new List<string> { "Anna Johnson" }
            },
            new Child
            {
                Id = 2,
                Name = "Liam Smith",
                Age = 3,
                Classroom = "Toddler",
                IsScheduledToday = true,
                Allergies = new List<string> { "Peanut allergy" },
                ApprovedPickupContacts = new List<string> { "Sarah Smith" }
            },
            new Child
            {
                Id = 3,
                Name = "Noah Wilson",
                Age = 4,
                Classroom = "Pre-K",
                IsScheduledToday = false,
                Allergies = new List<string> { "Egg allergy" },
                ApprovedPickupContacts = new List<string> { "Emily Wilson" }
            }
        };
    }

    [Fact]
    public void AttendanceService_ReturnsOnlyScheduledChildren()
    {
        List<Child> children = GetSampleChildren();
        AttendanceService service = new(children);

        List<Child> scheduledChildren = service.GetScheduledChildren();

        Assert.Equal(2, scheduledChildren.Count);
        Assert.All(scheduledChildren, child => Assert.True(child.IsScheduledToday));
    }

    [Fact]
    public void AttendanceService_FindsChildByName()
    {
        List<Child> children = GetSampleChildren();
        AttendanceService service = new(children);

        Child? child = service.FindChild("Liam Smith");

        Assert.NotNull(child);
        Assert.Equal("Liam Smith", child.Name);
    }

    [Fact]
    public void RatioService_CalculatesRequiredStaff()
    {
        RatioService service = new();

        int requiredStaff = service.CalculateRequiredStaff(8);

        Assert.Equal(2, requiredStaff);
    }

    [Fact]
    public void RatioService_ReturnsFalseWhenRatioIsNotMet()
    {
        RatioService service = new();

        bool result = service.IsRatioMet(8, 1);

        Assert.False(result);
    }

    [Fact]
    public void PickupService_ReturnsTrueForApprovedPickupContact()
    {
        List<Child> children = GetSampleChildren();
        AttendanceService attendanceService = new(children);
        PickupService pickupService = new(attendanceService);

        Child? child = pickupService.FindChildForPickup("Emma Johnson");

        Assert.NotNull(child);
        Assert.True(pickupService.IsPickupApproved(child, "Anna Johnson"));
    }

    [Fact]
    public void DailySummaryService_ReturnsImportantWarnings()
    {
        List<Child> children = GetSampleChildren();
        DailySummaryService service = new(children);

        List<string> warnings = service.GetImportantWarnings();

        Assert.Contains(warnings, warning => warning.Contains("Peanut allergy"));
        Assert.Contains(warnings, warning => warning.Contains("not scheduled"));
    }
}
