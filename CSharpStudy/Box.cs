class Box : Unit
{
    public bool isOnGoal = false;
    public string boxOnGoalIcon = "●";

    static int boxOnGoalCount = 0;

    public Box(int x, int y) : base(x, y, "◎")
    {
        isOnGoal = false;
    }

    public void SetBoxOnGoal(bool flag)
    {
        if(flag == isOnGoal)
        {
            return;
        }
        if(flag == true)
        {
            isOnGoal = true;
            boxOnGoalCount++;
        }
        else
        {
            isOnGoal = false;
            boxOnGoalCount--;
        }
    }

    public void Move(Direction dir)
    {
        if (dir == Direction.Up)
        {
            positionY = positionY - 1;
        }
        else if (dir == Direction.Down)
        {
            positionY = positionY + 1;
        }
        else if (dir == Direction.Right)
        {
            positionX = positionX + 1;
        }
        else if (dir == Direction.Left)
        {
            positionX = positionX - 1;
        }

        if (positionX < 0)
        {
            positionX = 0;
        }
        if (positionY < 0)
        {
            positionY = 0;
        }
    }
    
    public bool isCollided(Unit unit)
    {
        if(unit.positionX == positionX && unit.positionY == positionY)
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
        if (isOnGoal)
        {
            DrawIcon(boxOnGoalIcon);
        }
        else
        {
            base.DrawIcon();
        }
    }

    public static bool IsAllBoxesOnGoal()
    {
        if(boxOnGoalCount == Goal.allGoalCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}