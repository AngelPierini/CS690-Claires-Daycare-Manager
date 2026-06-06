using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Child> children = new List<Child>
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

    static void Main()
    {
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
            Console.WriteLine("6. Exit");
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
                    running = false;
                    Console.WriteLine("Thank you for using Claire's Daycare Manager.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a number from 1 to 6.");
                    Pause();
                    break;
            }
        }
    }

    static void ViewDailyAttendanceList()
    {
        Console.Clear();
        DisplayHeader("Daily Attendance List");

        Console.WriteLine("Children scheduled to attend today:");
        Console.WriteLine();

        foreach (Child child in children.Where(c => c.IsScheduledToday))
        {
            Console.WriteLine($"ID: {child.Id} | Name: {child.Name} | Age: {child.Age} | Classroom: {child.Classroom}");
        }

        Console.WriteLine();
        Console.WriteLine("Functional Requirement: FR-1 - View daily attendance list");
        Pause();
    }

    static void CheckChildAttendance()
    {
        Console.Clear();
        DisplayHeader("Check Child Attendance");

        Console.Write("Enter child name or ID: ");
        string? input = Console.ReadLine();

        Child? child = FindChild(input);

        Console.WriteLine();

        if (child == null)
        {
            Console.WriteLine("Child not found.");
        }
        else if (child.IsScheduledToday)
        {
            Console.WriteLine($"{child.Name} is scheduled to attend today.");
        }
        else
        {
            Console.WriteLine($"{child.Name} is NOT scheduled to attend today.");
        }

        Console.WriteLine();
        Console.WriteLine("Functional Requirement: FR-2 - Search for a child in the attendance list");
        Pause();
    }

    static void VerifyStaffToChildRatio()
    {
        Console.Clear();
        DisplayHeader("Verify Staff-to-Child Ratio");

        int childrenCount = ReadPositiveNumber("Enter number of children: ");
        int staffCount = ReadPositiveNumber("Enter number of staff members: ");

        Console.WriteLine();

        int requiredStaff = (int)Math.Ceiling(childrenCount / 4.0);

        Console.WriteLine($"Children: {childrenCount}");
        Console.WriteLine($"Staff members: {staffCount}");
        Console.WriteLine($"Required staff members using 1 staff per 4 children: {requiredStaff}");
        Console.WriteLine();

        if (staffCount >= requiredStaff)
        {
            Console.WriteLine("Result: The staff-to-child ratio is acceptable. The activity can begin.");
        }
        else
        {
            Console.WriteLine("Warning: The staff-to-child ratio is not met. Please add more staff before starting the activity.");
        }

        Console.WriteLine();
        Console.WriteLine("Functional Requirements: FR-3 and FR-4 - Enter children/staff and display ratio status");
        Pause();
    }

    static void CheckAllergyInformation()
    {
        Console.Clear();
        DisplayHeader("Check Allergy Information");

        Console.Write("Enter child name or ID: ");
        string? input = Console.ReadLine();

        Child? child = FindChild(input);

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

    static void VerifyPickupPermission()
    {
        Console.Clear();
        DisplayHeader("Verify Pickup Permission");

        Console.Write("Enter child name or ID: ");
        string? childInput = Console.ReadLine();

        Child? child = FindChild(childInput);

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

        bool isApproved = child.ApprovedPickupContacts.Any(contact =>
            contact.Equals(pickupName, StringComparison.OrdinalIgnoreCase));

        if (isApproved)
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

    static Child? FindChild(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (int.TryParse(input, out int id))
        {
            return children.FirstOrDefault(c => c.Id == id);
        }

        return children.FirstOrDefault(c =>
            c.Name.Contains(input, StringComparison.OrdinalIgnoreCase));
    }

    static int ReadPositiveNumber(string message)
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

    static void DisplayHeader(string title)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine(title);
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine();
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
}

class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Classroom { get; set; } = "";
    public bool IsScheduledToday { get; set; }
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> ApprovedPickupContacts { get; set; } = new List<string>();
}
