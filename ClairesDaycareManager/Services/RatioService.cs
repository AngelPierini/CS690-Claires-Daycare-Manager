using System;

namespace ClairesDaycareManager.Services;

public class RatioService
{
    public int CalculateRequiredStaff(int numberOfChildren)
    {
        return (int)Math.Ceiling(numberOfChildren / 4.0);
    }

    public bool IsRatioMet(int numberOfChildren, int numberOfStaff)
    {
        int requiredStaff = CalculateRequiredStaff(numberOfChildren);
        return numberOfStaff >= requiredStaff;
    }

    public string GetRatioStatus(int numberOfChildren, int numberOfStaff)
    {
        return IsRatioMet(numberOfChildren, numberOfStaff)
            ? "Result: The staff-to-child ratio is acceptable. The activity can begin."
            : "Warning: The staff-to-child ratio is not met. Please add more staff before starting the activity.";
    }
}
