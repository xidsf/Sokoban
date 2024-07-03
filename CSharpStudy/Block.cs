class Block : Unit
{
    public static int breakableBlockCount = 0;

    public bool isInPlayer;
    public bool isBroken;
    public readonly bool isbreakable;

    public string playerInIcon;
    public string unbreakIcon;

    public Block(int x, int y, bool breakable) : base(x, y, "□")
    {
        positionX = x;
        positionY = y;
        isbreakable = breakable;
        isBroken = false;
        isInPlayer = false;

        playerInIcon = "▣";
        unbreakIcon = "■";

        if (isbreakable == true)
        {
            breakableBlockCount++;
        }
    }

    public void BreakBlock()
    {
        isBroken = true;
        breakableBlockCount--;
    }

    public bool isCollided(Unit unit)
    {
        if(isBroken == true)
        {
            return false;
        }
        if (unit.positionX == positionX && unit.positionY == positionY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void DrawIcon()
    {
        if(isBroken == true)
        {
            return;
        }
        if(isbreakable == false)
        {
            DrawIcon(unbreakIcon);
        }
        else if(isInPlayer == true)
        {
            DrawIcon(playerInIcon);
        }
        else
        {
            base.DrawIcon();
        }
        
    }

}