class Button : Unit
{
    public static int pressableCount = 5;
    public bool isInteractable;

    int[] weightTable = { 10, 30, 60 };
    int[] resultTable = { 2, 1, 0 };
    int totalWeight = 0;

    Random random = new Random();

    public Button(int x, int y) : base(x, y, "◈")
    {
        for (int i = 0; i < weightTable.Length; i++)
        {
            totalWeight += weightTable[i];
        }
        isInteractable = true;
        if (StageManager.currentStage == 3) pressableCount = 10;
        else pressableCount = 5;
    }

    public void BreakRandomBlock(ref Block[] blocks)
    {
        if(isInteractable == false)
        {
            return;
        }
        if (Block.breakableBlockCount == 0)
        {
            return;
        }

        int randomNum = random.Next(1, totalWeight + 1);
        int weight = 0;
        int result = 0;
        for (int i = 0; i < weightTable.Length; i++)
        {
            weight += weightTable[i];
            if (weight >= randomNum)
            {
                result = resultTable[i];
                break;
            }
        }

        int loopCount = 0;
        while(loopCount < result)
        {
            randomNum = random.Next(0, blocks.Length);
            if (blocks[randomNum].isBroken == false && blocks[randomNum].isbreakable == true)
            {
                blocks[randomNum].BreakBlock();
                loopCount++;
            }
        }

        pressableCount--;
        if (pressableCount <= 0) isInteractable = false;
    }

    public override void DrawIcon()
    {
        if(isInteractable == false)
        {
            return;
        }
        base.DrawIcon();
    }
}