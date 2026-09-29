namespace Lab2;

static class Program
{
    // Entry point: runs task 1 then task 2
    public static void Main()
    {
        Task1();
        Task2();
    }

    // Task 1: tests each ArrayOperations method
    public static void Task1()
    {
        Console.WriteLine("TASK 1: ARRAYS");
        ArraysandLists ops = new ArraysandLists();
        int[] numbers = { 10, 11, 1, 25, 7 };

        PrintArray("Original", numbers);

        ops.ReverseArray(numbers);
        PrintArray("Reversed", numbers);

        Console.WriteLine("Maximum: " + ops.FindMaximum(numbers));

        Console.WriteLine("Is 11 in the array? " + ops.SearchElement(numbers, 11));
        Console.WriteLine("Is 99 in the array? " + ops.SearchElement(numbers, 99));

        ops.SortArray(numbers);
        PrintArray("Sorted", numbers);
        Console.WriteLine();
    }

    // Task 2: tests each StudentList method
    public static void Task2()
    {
        Console.WriteLine("TASK 2: STUDENT LIST");
        StudentList list = new StudentList();
        list.AddStudent(new Student("M001", "Sarah Jones", 20));
        list.AddStudent(new Student("M002", "Tom Smith", 23));
        list.AddStudent(new Student("M003", "Emma Brown", 20));

        Console.WriteLine("Find M002: " + list.FindStudent("M002"));

        Console.WriteLine("Students aged 20:");
        foreach (Student s in list.GetStudentsByAge(20))
        {
            Console.WriteLine(s);
        }

        list.RemoveStudent("M002");
        if (list.FindStudent("M002") == null)
        {
            Console.WriteLine("M002 has been removed");
        }
    }

    // Prints a label and then every number on one line
    public static void PrintArray(string label, int[] arr)
    {
        Console.Write(label + ": ");
        foreach (int number in arr)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();
    }
}

public class ArraysandLists
{
    // Swaps the ends and moves inward until the two sides meet
    public void ReverseArray(int[] arr)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left < right)
        {
            int temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;

            left++;
            right--;
        }
    }

    // Starts with the first number and keeps whichever is bigger
    public int FindMaximum(int[] arr)
    {
        int max = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
            }
        }
        return max;
    }

    // Checks each number and returns true as soon as it matches
    public bool SearchElement(int[] arr, int target)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == target)
            {
                return true;
            }
        }
        return false;
    }

    // Bubble sort: swaps neighbours that are in the wrong order
    public void SortArray(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - 1 - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }
}

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    // Constructor: sets all three values when a Student is created
    public Student(string id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    // Override: prints the student's details instead of the class name
    public override string ToString()
    {
        return Id + " - " + Name + ", age " + Age;
    }
}

public class StudentList
{
    private List<Student> _students = new List<Student>();

    // Adds a student to the end of the list
    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    // Finds the student with this ID and removes them if they exist
    public void RemoveStudent(string id)
    {
        Student? found = FindStudent(id);
        if (found != null)
        {
            _students.Remove(found);
        }
    }

    // Checks each student and returns the one with this ID, or null if none
    public Student? FindStudent(string id)
    {
        foreach (Student s in _students)
        {
            if (s.Id == id)
            {
                return s;
            }
        }
        return null;
    }

    // Builds a new list of every student with this age
    public List<Student> GetStudentsByAge(int age)
    {
        List<Student> result = new List<Student>();
        foreach (Student s in _students)
        {
            if (s.Age == age)
            {
                result.Add(s);
            }
        }
        return result;
    }
}