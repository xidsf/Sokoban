static class ObjectManager
{
    public static Action<Direction>? GetCollidedUnits(Player player, Block[] block)
    {
        Action<Direction>? action = null;

        if(player.isGhost == false)
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].isHidden == true)
                {
                    continue;
                }
                if (player.isCollide(block[i].positionX, block[i].positionY))
                {
                    action = player.Move;
                    return action;
                }
            }
        }
        else
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].isbreakable == false && player.isCollide(block[i].positionX, block[i].positionY))
                {
                    action = player.Move;
                    return action;
                }
            }
        }
        return action;
    }

    public static Action<Direction>? GetCollidedUnits(Player player, Box[] box, Block[] block, int boxIndex)
    {
        Action<Direction>? action = null;

        for (int i = 0; i < block.Length; i++)
        {
            if (block[i].isHidden == true)
            {
                continue;
            }
            if (box[boxIndex].isCollide(block[i].positionX, block[i].positionY))
            {
                action = player.Move;
                action += box[boxIndex].Move;
                return action;
            }
        }
        for (int i = 0; i < box.Length; i++)
        {
            if(i == boxIndex)
            {
                continue;
            }
            if (box[boxIndex].isCollide(box[i].positionX, box[i].positionY))
            {
                action = player.Move;
                action += box[boxIndex].Move;
                return action;
            }
        }
        return action;
    }

    public static bool TryGetInteractedBoxIndex(Player player, Box[] box, out int interactedBoxIndex)
    {
        for (int i = 0; i < box.Length; i++)
        {
            if (player.isCollide(box[i]))
            {
                interactedBoxIndex = i;
                return true;
            }
        }
        interactedBoxIndex = -1;
        return false;
    }


    public static void PlayerInBlockCheck(Player player, Block[] block)
    {
        if (player.isGhost == true)
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (player.isCollide(block[i].positionX, block[i].positionY))
                {
                    block[i].isInPlayer = true;
                }
                else
                {
                    block[i].isInPlayer = false;
                }
            }
        }
    }

    public static void ItemCollisionCheck(Player player, Item item)
    {
        if(item == null)
        {
            return;
        }
        if (player.isCollide(item.positionX, item.positionY))
        {
            item.MakePlayerGhost(player);
        }
    }

    public static void ButtonCollisionCheck(Player player, Button button, Block[] block)
    {
        if(button == null)
        {
            return;
        }
        if (player.isCollide(button.positionX, button.positionY))
        {
            button.HideRandomBlock(block);
        }
    }

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

    public static bool CheckAllBoxOnGoal(Box[] box, Goal[] goal)
    {
        int boxOnGoalCount = 0;
        for (int i = 0; i < goal.Length; i++)
        {
            for (int j = 0; j < box.Length; j++)
            {
                if (box[j].isCollide(goal[i].positionX, goal[i].positionY))
                {
                    boxOnGoalCount++;
                }
            }
        }
        if (boxOnGoalCount == goal.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}