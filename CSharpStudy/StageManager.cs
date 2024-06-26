static class StageManager
{
    const char unbreakBlockChar = '#';
    const char breakableBlockChar = 'B';
    const char boxChar = 'O';
    const char playerChar = 'P';
    const char ButtonChar = 'u';
    const char goalChar = 'G';
    const char itemChar = 'i';

    static string[] stage = new string[3];
    public static int currentStage = 0;

    static StageManager()
    {
        stage[0] = File.ReadAllText("stage1.txt");
        stage[1] = File.ReadAllText("stage2.txt");
        stage[2] = File.ReadAllText("stage3.txt");
    }

    public static void NextStage()
    {
        currentStage++;
        MakeStage(stage[currentStage]);
    }

    public static void ResetStage()
    {
        MakeStage(stage[currentStage]);
    }

    private static void MakeStage(string stage)
    {
        Block.breakableBlockCount = 0;
        if(currentStage == 3)
        {
            Button.pressableCount = 10;
        }
        else
        {
            Button.pressableCount = 5;
        }
        int blockCount = 0;
        int boxCount = 0;
        int goalCount = 0;

        foreach (char c in stage)
        {
            if(c == unbreakBlockChar || c == breakableBlockChar)
            {
                blockCount++;
            }
            else if(c == boxChar)
            {
                boxCount++;
            }
            else if (c == goalChar)
            {
                goalCount++;
            }
        }

        Program.box = new Box[boxCount];
        Program.block = new Block[blockCount];
        Program.goal = new Goal[goalCount];

        int x = 0, y = 0;
        int currentBoxIndex = 0;
        int currentGoalIndex = 0;
        int currentBlockIndex = 0;

        foreach (char c in stage)
        {
            switch (c)
            {
                case breakableBlockChar:
                    Program.block[currentBlockIndex++] = new Block(x, y, true);
                    x++;
                    break;

                case unbreakBlockChar:
                    Program.block[currentBlockIndex++] = new Block(x, y, false);
                    x++;
                    break;

                case boxChar:
                    Program.box[currentBoxIndex++] = new Box(x, y);
                    x++;
                    break;

                case goalChar:
                    Program.goal[currentGoalIndex++] = new Goal(x, y);
                    x++;
                    break;

                case itemChar:
                    Program.item = new Item(x, y);
                    x++;
                    break;

                case playerChar:
                    Program.player = new Player(x, y);
                    x++;
                    break;

                case ButtonChar:
                    Program.button = new Button(x, y);
                    x++;
                    break;

                case '\n':
                    x = 0;
                    y++;
                    break;

                default:
                    x++;
                    break;
            }
        }

    }


}