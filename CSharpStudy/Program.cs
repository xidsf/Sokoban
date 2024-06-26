class Program
{
    public static Player player;
    public static Box[] box;
    public static Block[] block;
    public static Goal[] goal;
    public static Item item;
    public static Button button;

    static void Main(string[] args)
    {
        InitGameState();

        while (true)
        {
            Render();
            ConsoleKey key = Input();
            Direction inputDir;

            if(key != ConsoleKey.R)
            {
                inputDir = ConvertToDirection(key);
            }
            else
            {
                StageManager.ResetStage();
                continue;
            }

            Action<Direction>? pushOutUnits;

            player.Move(inputDir);
            if(ObjectManager.TryGetInteractedBoxIndex(player, box, out int interactedBoxIndex))
            {
                box[interactedBoxIndex].Move(inputDir);
                pushOutUnits = ObjectManager.GetCollidedUnits(player, box, block, interactedBoxIndex) ;
            }
            else
            {
                pushOutUnits = ObjectManager.GetCollidedUnits(player, block);
            }
            
            if(pushOutUnits != null)
            {
                pushOutUnits.Invoke(ObjectManager.GetReverseDir(inputDir));
            }

            ObjectManager.PlayerInBlockCheck(player, block);
            ObjectManager.ItemCollisionCheck(player, item);
            ObjectManager.ButtonCollisionCheck(player, button, block);

            if (ObjectManager.CheckAllBoxOnGoal(box, goal))
            {
                if(StageManager.currentStage >= 3)
                {
                    break;
                }
                StageManager.NextStage();
                
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

            DrawObject(player.positionX, player.positionY, player.playerIcon);

            for (int i = 0; i < block.Length; i++)
            {
                if (block[i].isHidden)
                {
                    continue;
                }
                else if (block[i].isbreakable == false)
                {
                    DrawObject(block[i].positionX, block[i].positionY, block[i].unbreakIcon);
                }
                else if (block[i].isInPlayer)
                {
                    DrawObject(block[i].positionX, block[i].positionY, block[i].playerInIcon);
                }
                else
                {
                    DrawObject(block[i].positionX, block[i].positionY, block[i].Icon);
                }
            }

            if(item != null)
            {
                if (!player.isGhost)
                {
                    DrawObject(item.positionX, item.positionY, item.itemIcon);
                }
            }

            if(button != null)
            {
                if (button.isInteractable)
                {
                    DrawObject(button.positionX, button.positionY, button.itemIcon);
                }
            }

            for (int i = 0; i < goal.Length; i++)
            {
                DrawObject(goal[i].positionX, goal[i].positionY, goal[i].Icon);
            }

            for (int i = 0; i < box.Length; i++)
            {
                for (int j = 0; j < goal.Length; j++)
                {
                    if (box[i].isOnGoal)
                    {
                        DrawObject(box[i].positionX, box[i].positionY, box[i].boxOnIcon);
                        break;
                    }
                    else
                    {
                        DrawObject(box[i].positionX, box[i].positionY, box[i].Icon);
                    }
                }
            }

            static void DrawObject(int x, int y, string icon)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(icon);
            }
        }

        ConsoleKey Input()
        {
            ConsoleKeyInfo inputKeyInfo = Console.ReadKey();
            ConsoleKey key = inputKeyInfo.Key;
            return key;
        }

        Direction ConvertToDirection(ConsoleKey key)
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

    }
}