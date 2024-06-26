class Block
{
    public static int breakableBlockCount = 0;

    public readonly int positionX;
    public readonly int positionY;

    public bool isInPlayer = false;
    public bool isHidden = false;
    public readonly bool isbreakable;

    public string Icon = "□";
    public string playerInIcon = "▣";
    public string unbreakIcon = "■";

    public Block(int x, int y, bool breakable , bool isBroken = false)
    {
        positionX = x;
        positionY = y;
        isbreakable = breakable;
        this.isHidden = isBroken;
        isInPlayer = false;

        if(isBroken == false && isbreakable == true)
        {
            breakableBlockCount++;
        }
    }

    public void ShowBlock()
    {
        isHidden = false;
        breakableBlockCount++;
    }

    public void HideBlock()
    {
        isHidden = true;
        breakableBlockCount--;
    }

}