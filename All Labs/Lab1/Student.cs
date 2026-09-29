namespace Lab1;

public class Student : Person
{
    public string StudentId { get; set; }

    // Constructor: passes shared fields to Person, then sets the student ID
    public Student(string studentId, string name, int age, string email)
        : base(name, age, email)
    {
        StudentId = studentId;
    }

    // Override: extends the Person output instead of rewriting it
    public override string ToString()
    {
        return StudentId + " - " + base.ToString();
    }
}