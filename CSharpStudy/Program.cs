class Program
{
    public static Player player;
    public static Box[] box;
    public static Block[] block;
    public static Goal[] goal;
    public static Item[] item;
    public static Button[] button;

    static void Main(string[] args)
    {
        InitGameState();

        while (true)
        {
            Render();

            ConsoleKey key = Input();
            
            if(key == ConsoleKey.R)
            {
                StageManager.ResetStage();
                continue;
            }
            
            Direction inputDir = ConvertKeyToDirection(key);


            Action<Direction> moveAction;

            player.Move(inputDir);
            moveAction = player.Move;

            int collidedBoxIndex;
            if (TryGetCollidedUnitIndex(player, box, out collidedBoxIndex))
            {
                box[collidedBoxIndex].Move(inputDir);
                moveAction += box[collidedBoxIndex].Move;
                if(IsBoxCollided(collidedBoxIndex))
                {
                    moveAction.Invoke(ObjectManager.GetReverseDir(inputDir));
                }
            }
            else
            {
                if (IsPlayerCollisidedToBlock() == true)
                {
                    moveAction.Invoke(ObjectManager.GetReverseDir(inputDir));
                }
            }
            

            BoxGoalCollisionCheck();
            ItemCollisionCheck();
            ButtonCollisionCheck();

            if(Box.IsAllBoxesOnGoal())
            {
                StageManager.NextStage();
            }
            if(StageManager.currentStage > 3)
            {
                break;
            }
        }

        void InitGameState()
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Sokoban";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            StageManager.ResetStage();
        }

        void Render()
        {
            Console.Clear();

            RenderUnit(player);
            
            RenderUnit(block);
            RenderUnit(goal);
            RenderUnit(box);

            RenderUnit(item);
            RenderUnit(button);

            void RenderUnit(params Unit[] units)
            {
                if (units == null) return;
                foreach (Unit unit in units)
                {
                    unit.DrawIcon();
                }
            }
        }

        ConsoleKey Input()
        {
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();
            ConsoleKey key = inputKeyInfo.Key;
            return key;
        }

        Direction ConvertKeyToDirection(ConsoleKey key)
        {
            Direction dir;

            if (key == ConsoleKey.UpArrow)
            {
                dir = Direction.Up;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                dir = Direction.Down;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                dir = Direction.Left;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                dir = Direction.Right;
            }
            else
            {
                dir = Direction.Right;
            }

            return dir;
        }

        bool IsPlayerCollisidedToBlock()
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].isCollided(player))
                {
                    return true;
                }
            }

            return false;
        }

        bool IsBoxCollided(int movedBoxIndex)
        {
            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].isCollided(box[movedBoxIndex]))
                {
                    return true;
                }
            }

            for (int i = 0; i < box.Length; i++)
            {
                if (i == movedBoxIndex)
                {
                    continue;
                }
                if (box[i].isCollided(box[movedBoxIndex]))
                {
                    return true;
                }
            }

            return false;
        }

        void BoxGoalCollisionCheck()
        {
            for (int i = 0; i < box.Length; i++)
            {
                for (int j = 0; j < goal.Length; j++)
                {
                    if (box[i].isCollided(goal[j]))
                    {
                        box[i].SetBoxOnGoal(true);
                        break;
                    }
                    else
                    {
                        box[i].SetBoxOnGoal(false);
                    }

                }
            }
        }

        void ItemCollisionCheck()
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (ObjectManager.UnitCollideCheck(player, item[i]))
                {
                    item[i].GivePlayerAbility(player);
                }
            }
        }

        void ButtonCollisionCheck()
        {
            for (int i = 0; i < button.Length; i++)
            {
                if (ObjectManager.UnitCollideCheck(player, button[i]))
                {
                    button[i].BreakRandomBlock(ref block);
                }
            }
        }

        //out을 사용해서 충돌했을 때의 index를 확정적으로 얻었을 때만 되도록 했는데 더 복잡해진 것 같음
        bool TryGetCollidedUnitIndex(Unit unit, Unit[]? units, out int index)
        {
            if (units == null)
            {
                index = -1;
                return false;
            }

            for (int i = 0; i < units.Length; i++)
            {
                if (ObjectManager.UnitCollideCheck(player, units[i]))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;

        }
    }


}