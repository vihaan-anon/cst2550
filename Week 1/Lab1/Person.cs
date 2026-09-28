namespace Lab1;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    // Constructor: sets the three values when a Person is created
    public Person(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
    }

    // Validation: name must not be blank
    public static bool IsValidName(string name)
    {
        return name != null && name.Trim() != "";
    }

    // Validation: age must be between 16 and 120
    public static bool IsValidAge(int age)
    {
        return age >= 16 && age <= 60;
    }

    // Validation: email must contain an @ symbol
    public static bool IsValidEmail(string email)
    {
        return email != null && email.Contains("@");
    }

    // Override: replaces object.ToString() so printing shows the data
    public override string ToString()
    {
        return Name + ", age " + Age + ", " + Email;
    }
}