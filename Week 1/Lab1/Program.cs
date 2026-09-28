namespace Lab1;

public static class Program
{
    private static StudentManager _manager = new StudentManager();

    // Entry point: loads sample data then runs the menu until the user exits
    public static void Main()
    {
        _manager.AddStudent(new Person("John Wick", 20, "jw123@live.mdx.ac.uk"));
        _manager.AddStudent(new Person("Pablo Escobar", 23, "pe456@live.mdx.ac.uk"));
        _manager.AddStudent(new Person("Someone random", 19, "sr789@live.mdx.ac.uk"));

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add new student  2. Display student information  3. Search for students  4. Average age of students  0. Exit");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            if (choice == "1") AddStudent();
            else if (choice == "2") _manager.DisplayAll();
            else if (choice == "3") SearchStudent();
            else if (choice == "4") Console.WriteLine("Average age: " + _manager.CalculateAverageAge().ToString("F1"));
            else if (choice == "0") return;
            else Console.WriteLine("Invalid option. Enter 0 to 4.");
        }
    }

    // Asks for each field and stops with a message as soon as one is wrong
    private static void AddStudent()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        if (!Person.IsValidName(name))
        {
            Console.WriteLine("Name cannot be blank.");
            return;
        }

        Console.Write("Age: ");
        int age;
        if (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Age must be a whole number.");
            return;
        }
        if (!Person.IsValidAge(age))
        {
            Console.WriteLine("Age cannot be " + age + ". It must be between 16 and 120.");
            return;
        }

        Console.Write("Email: ");
        string email = Console.ReadLine();
        if (!Person.IsValidEmail(email))
        {
            Console.WriteLine("Email must contain an @ symbol.");
            return;
        }

        Person student = new Person(name, age, email);
        _manager.AddStudent(student);
        Console.WriteLine("Added: " + student);
    }

    // Asks for a name and reports whether it was found
    private static void SearchStudent()
    {
        Console.Write("Name: ");
        Person found = _manager.SearchByName(Console.ReadLine());
        Console.WriteLine(found != null ? "Found: " + found : "No student with that name.");
    }
}