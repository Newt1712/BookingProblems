// Calculate function
static int CalculateTotalBottles(int totalBottles, int exchangedRate)
{
    return totalBottles + (totalBottles - 1) / (exchangedRate - 1);
}

// Main function

while (true)
{
    Console.Write("Enter number of bottles (0 to 100000): ");
    string? input = Console.ReadLine();
    int num;
    int rate = 3;

    if (string.IsNullOrEmpty(input)) return;

    if (input.Equals("exit"))
    {
        Console.WriteLine("Bye ===>");
        return;
    }

    if (int.TryParse(input, out num) && num >= 0 && num <= 100000)
    {
        int totalDrinks = CalculateTotalBottles(num, rate);
        Console.WriteLine($"Total bottles you can drink: {totalDrinks}");
    }
    else
    {
        Console.WriteLine("Invalid input! Please enter a valid integer between 0 and 100000.");
    }
}
