public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. " +
            "Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            int breatheInSeconds = Math.Min(4, GetRemainingSeconds(endTime));

            if (breatheInSeconds == 0)
            {
                break;
            }

            Console.Write("Breathe in... ");
            DisplayCountDown(breatheInSeconds);
            Console.WriteLine();

            int breatheOutSeconds = Math.Min(6, GetRemainingSeconds(endTime));

            if (breatheOutSeconds == 0)
            {
                break;
            }

            Console.Write("Breathe out... ");
            DisplayCountDown(breatheOutSeconds);
            Console.WriteLine();
        }
    }
}
