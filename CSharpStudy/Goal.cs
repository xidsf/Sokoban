class Goal : Unit
{
    public static int allGoalCount = 0;

    public Goal(int x, int y) : base(x, y, "○")
    {
        allGoalCount++;
    }
}