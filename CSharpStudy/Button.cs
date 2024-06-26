class Button
{
    public static int pressableCount = 5;

    public readonly int positionX;
    public readonly int positionY;
    public string itemIcon = "◈";
    public bool isInteractable;

    int[] weightTable = { 10, 30, 60 };
    int[] resultTable = { 2, 1, 0 };
    int totalWeight = 0;

    Random random = new Random();

    public Button(int x, int y)
    {
        positionX = x;
        positionY = y;
        for (int i = 0; i < weightTable.Length; i++)
        {
            totalWeight += weightTable[i];
        }
        isInteractable = true;
    }

    public void HideRandomBlock(Block[] blocks)
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
            if(weight >= randomNum)
            {
                result = resultTable[i];
                break;
            }
        }

        int loopCount = 0;
        while(loopCount < result)
        {
            randomNum = random.Next(0, blocks.Length);
            if (blocks[randomNum].isHidden == false && blocks[randomNum].isbreakable == true)
            {
                blocks[randomNum].HideBlock();
                loopCount++;
            }
        }

        pressableCount--;
        if (pressableCount <= 0) isInteractable = false;

    }


}