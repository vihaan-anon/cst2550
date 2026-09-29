namespace Lab1;

public class StudentManager
{
    private List<Person> _students = new List<Person>();

    // Adds one student to the list
    public void AddStudent(Person student)
    {
        _students.Add(student);
    }

    // Prints every student, using the ToString override
    public void DisplayAll()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students yet.");
        }

        foreach (Person s in _students)
        {
            Console.WriteLine(s);
        }
    }

    // Searches by name and returns the match, or null if there is none
    public Person SearchByName(string name)
    {
        foreach (Person s in _students)
        {
            if (s.Name == name)
            {
                return s;
            }
        }
        return null;
    }

    // Works out the mean age, returning 0 when the list is empty
    public double CalculateAverageAge()
    {
        if (_students.Count == 0)
        {
            return 0;
        }

        int total = 0;
        foreach (Person s in _students)
        {
            total = total + s.Age;
        }
        return (double)total / _students.Count;
    }
}