using ClairesDaycareManager.Models;
using ClairesDaycareManager.Services;

DaycareDataService dataService = new();
List<Child> children = dataService.GetAllChildren();

AttendanceService attendanceService = new(children);
RatioService ratioService = new();
AllergyService allergyService = new(attendanceService);
PickupService pickupService = new(attendanceService);
DailySummaryService dailySummaryService = new(children);

bool running = true;

while (running)
{
    Console.Clear();
    DisplayHeader("Claire's Daycare Manager");

    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. View Daily Attendance List");
    Console.WriteLine("2. Check Child Attendance");
    Console.WriteLine("3. Verify Staff-to-Child Ratio");
    Console.WriteLine("4. Check Allergy Information");
    Console.WriteLine("5. Verify Pickup Permission");
    Console.WriteLine("6. Generate Daily Summary");
    Console.WriteLine("7. Display Important Daily Warnings");
    Console.WriteLine("8. Add a New Child Record");
    Console.WriteLine("9. View All Children");
    Console.WriteLine("10. Exit");
    Console.WriteLine();

    Console.Write("Enter your choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ViewDailyAttendanceList();
            break;
        case "2":
            CheckChildAttendance();
            break;
        case "3":
            VerifyStaffToChildRatio();
            break;
        case "4":
            CheckAllergyInformation();
            break;
        case "5":
            VerifyPickupPermission();
            break;
        case "6":
            GenerateDailySummary();
            break;
        case "7":
            DisplayImportantDailyWarnings();
            break;
        case "8":
            AddNewChildRecord();
            break;
        case "9":
            ViewAllChildren();
            break;
        case "10":
            running = false;
            Console.WriteLine("Thank you for using Claire's Daycare Manager.");
            break;
        default:
            Console.WriteLine("Invalid option. Please select a number from 1 to 10.");
            Pause();
            break;
    }
}

void ViewDailyAttendanceList()
{
    Console.Clear();
    DisplayHeader("Daily Attendance List");

    Console.WriteLine("Children scheduled to attend today:");
    Console.WriteLine();

    foreach (Child child in attendanceService.GetScheduledChildren())
    {
        Console.WriteLine($"ID: {child.Id} | Name: {child.Name} | Age: {child.Age} | Classroom: {child.Classroom}");
    }

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-1 - View daily attendance list");
    Pause();
}

void CheckChildAttendance()
{
    Console.Clear();
    DisplayHeader("Check Child Attendance");

    Console.Write("Enter child name or ID: ");
    string? input = Console.ReadLine();

    Console.WriteLine();
    Console.WriteLine(attendanceService.GetAttendanceStatus(input));

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-2 - Search for a child in the attendance list");
    Pause();
}

void VerifyStaffToChildRatio()
{
    Console.Clear();
    DisplayHeader("Verify Staff-to-Child Ratio");

    int childrenCount = ReadPositiveNumber("Enter number of children: ");
    int staffCount = ReadPositiveNumber("Enter number of staff members: ");

    int requiredStaff = ratioService.CalculateRequiredStaff(childrenCount);

    Console.WriteLine();
    Console.WriteLine($"Children: {childrenCount}");
    Console.WriteLine($"Staff members: {staffCount}");
    Console.WriteLine($"Required staff members using 1 staff per 4 children: {requiredStaff}");
    Console.WriteLine();
    Console.WriteLine(ratioService.GetRatioStatus(childrenCount, staffCount));

    Console.WriteLine();
    Console.WriteLine("Functional Requirements: FR-3 and FR-4 - Enter children/staff and display ratio status");
    Pause();
}

void CheckAllergyInformation()
{
    Console.Clear();
    DisplayHeader("Check Allergy Information");

    Console.Write("Enter child name or ID: ");
    string? input = Console.ReadLine();

    Child? child = allergyService.FindChildForAllergyCheck(input);

    Console.WriteLine();

    if (child == null)
    {
        Console.WriteLine("Child not found.");
    }
    else
    {
        Console.WriteLine($"Child: {child.Name}");
        Console.WriteLine("Allergy Information:");

        foreach (string allergy in child.Allergies)
        {
            if (allergy.Equals("No known allergies", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- {allergy}");
            }
            else
            {
                Console.WriteLine($"- WARNING: {allergy}");
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine("Functional Requirements: FR-5 and FR-6 - Store and display allergy warnings");
    Pause();
}

void VerifyPickupPermission()
{
    Console.Clear();
    DisplayHeader("Verify Pickup Permission");

    Console.Write("Enter child name or ID: ");
    string? childInput = Console.ReadLine();

    Child? child = pickupService.FindChildForPickup(childInput);

    if (child == null)
    {
        Console.WriteLine();
        Console.WriteLine("Child not found.");
        Pause();
        return;
    }

    Console.Write("Enter pickup person's name: ");
    string? pickupName = Console.ReadLine();

    Console.WriteLine();

    if (pickupService.IsPickupApproved(child, pickupName))
    {
        Console.WriteLine($"Result: {pickupName} is approved to pick up {child.Name}.");
    }
    else
    {
        Console.WriteLine($"Warning: {pickupName} is NOT approved to pick up {child.Name}.");
        Console.WriteLine("Do not release the child until authorization is confirmed.");
    }

    Console.WriteLine();
    Console.WriteLine("Functional Requirements: FR-7 and FR-8 - Store contacts and verify pickup authorization");
    Pause();
}

void GenerateDailySummary()
{
    Console.Clear();
    DisplayHeader("Daily Summary");

    Console.WriteLine($"Total children in system: {children.Count}");
    Console.WriteLine($"Children scheduled today: {dailySummaryService.GetScheduledChildrenCount()}");
    Console.WriteLine($"Allergy warnings found: {dailySummaryService.GetAllergyWarningCount()}");

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-9 - Generate daily summary");
    Pause();
}

void DisplayImportantDailyWarnings()
{
    Console.Clear();
    DisplayHeader("Important Daily Warnings");

    List<string> warnings = dailySummaryService.GetImportantWarnings();

    if (warnings.Count == 0)
    {
        Console.WriteLine("No important warnings found for today.");
    }
    else
    {
        foreach (string warning in warnings)
        {
            Console.WriteLine($"- {warning}");
        }
    }

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-10 - Display important daily warnings");
    Pause();
}

void AddNewChildRecord()
{
    Console.Clear();
    DisplayHeader("Add a New Child Record");

    int id = ReadPositiveNumber("Enter child ID: ");

    Console.Write("Enter child full name: ");
    string name = Console.ReadLine() ?? "";

    int age = ReadPositiveNumber("Enter child age: ");

    Console.Write("Enter classroom: ");
    string classroom = Console.ReadLine() ?? "";

    Console.Write("Is the child scheduled today? (yes/no): ");
    string scheduledInput = Console.ReadLine() ?? "";
    bool isScheduledToday = scheduledInput.Equals("yes", StringComparison.OrdinalIgnoreCase);

    Console.Write("Enter allergy information or type none: ");
    string allergyInput = Console.ReadLine() ?? "";
    List<string> allergies = allergyInput.Equals("none", StringComparison.OrdinalIgnoreCase)
        ? new List<string> { "No known allergies" }
        : new List<string> { allergyInput };

    Console.Write("Enter approved pickup contact name: ");
    string pickupContact = Console.ReadLine() ?? "";

    Child newChild = new()
    {
        Id = id,
        Name = name,
        Age = age,
        Classroom = classroom,
        IsScheduledToday = isScheduledToday,
        Allergies = allergies,
        ApprovedPickupContacts = new List<string> { pickupContact }
    };

    dataService.AddChild(newChild);

    Console.WriteLine();
    Console.WriteLine($"{newChild.Name} was added to the daycare system.");

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-11 - Add a new child record");
    Pause();
}

void ViewAllChildren()
{
    Console.Clear();
    DisplayHeader("All Children");

    foreach (Child child in children)
    {
        string scheduledStatus = child.IsScheduledToday ? "Scheduled Today" : "Not Scheduled Today";
        Console.WriteLine($"ID: {child.Id} | Name: {child.Name} | Age: {child.Age} | Classroom: {child.Classroom} | {scheduledStatus}");
    }

    Console.WriteLine();
    Console.WriteLine("Functional Requirement: FR-12 - View all children");
    Pause();
}

int ReadPositiveNumber(string message)
{
    int number;

    while (true)
    {
        Console.Write(message);
        string? input = Console.ReadLine();

        if (int.TryParse(input, out number) && number >= 0)
        {
            return number;
        }

        Console.WriteLine("Please enter a valid number.");
    }
}

void DisplayHeader(string title)
{
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine(title);
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine();
}

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to return to the main menu.");
    Console.ReadLine();
}
