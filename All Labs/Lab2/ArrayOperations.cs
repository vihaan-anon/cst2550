namespace Lab2;

static class Program
{
    // Entry point: tests each ArrayOperations method
    public static void Main()
    {
        ArrayOperations ops = new ArrayOperations();
        int[] numbers = { 10, 11, 1, 25, 7 };

        PrintArray("Original", numbers);

        ops.ReverseArray(numbers);
        PrintArray("Reversed", numbers);
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
}