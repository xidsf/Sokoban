static class ObjectManager
{
    public static Direction GetReverseDir(Direction dir)
    {
        Direction pushOutDirection;

        switch (dir)
        {
            case Direction.Left:
                pushOutDirection = Direction.Right;
                break;
            case Direction.Right:
                pushOutDirection = Direction.Left;
                break;
            case Direction.Up:
                pushOutDirection = Direction.Down;
                break;
            case Direction.Down:
                pushOutDirection = Direction.Up;
                break;
            default:
                pushOutDirection = Direction.Right;
                break;
        }
        return pushOutDirection;
    }

    public static bool UnitCollideCheck(Unit unit1, Unit unit2)
    {
        if(unit1 == null || unit2 == null)
        {
            return false;
        }
        if(unit1.positionX == unit2.positionX && unit1.positionY == unit2.positionY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}