namespace Lab2;

static class Program
{
    // Entry point: tests each ArrayOperations method
    public static void Main()
    {
        ArrayOperations ops = new ArrayOperations();
        int[] numbers = { 100, 11, 1, 20, 21, 3, 5 };

        PrintArray("Original", numbers);

        ops.ReverseArray(numbers);
        PrintArray("Reversed", numbers);

        Console.WriteLine("Maximum: " + ops.FindMaximum(numbers));

        Console.WriteLine("Is 11 in the array? " + ops.SearchElement(numbers, 11));
        Console.WriteLine("Is 99 in the array? " + ops.SearchElement(numbers, 99));

        ops.SortArray(numbers);
        PrintArray("Sorted", numbers);
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

public class ArrayOperations
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