using System.Globalization;
using static Program;

static class StageManager
{
    const char playerChar = 'P';
    const char boxChar = 'O';
    const char unbreakBlockChar = '#';
    const char breakableBlockChar = 'B';
    const char goalChar = 'G';
    const char itemChar = 'i';
    const char ButtonChar = 'u';

    static string[] stage = new string[3];
    public static int currentStage = 0;

    public static int numberOfUnit = 6;

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

        int boxCount = 0;
        int blockCount = 0;
        int goalCount = 0;  
        int itemCount = 0;
        int buttonCount = 0;
        
        foreach (char c in stage)
        {
            switch (c)
            {
                case boxChar:
                    boxCount++;
                    break;
                case unbreakBlockChar:
                case breakableBlockChar:
                    blockCount++;
                    break;
                case goalChar:
                    goalCount++;
                    break;
                case itemChar:
                    itemCount++;
                    break;
                case ButtonChar:
                    buttonCount++;
                    break;
            }
        }

        int x = 0, y = 0;

        box = new Box[boxCount];
        block = new Block[blockCount];
        goal = new Goal[goalCount];
        item = new Item[itemCount];
        button = new Button[buttonCount];

        int currentBoxcount = 0;
        int currentBlockCount = 0;
        int curentGoalCount = 0;
        int curentItemCount = 0;
        int currentButtonCount = 0;

        foreach (char c in stage)
        {
            switch (c)
            {
                case playerChar:
                    Program.player = new Player(x, y);
                    break;
                case boxChar:
                    Program.box[currentBoxcount++] = new Box(x, y);
                    break;
                case unbreakBlockChar:
                    Program.block[currentBlockCount++] = new Block(x, y, false);
                    break;
                case breakableBlockChar:
                    Program.block[currentBlockCount++] = new Block(x, y, true);
                    break;
                case goalChar:
                    Program.goal[curentGoalCount++] = new Goal(x, y);
                    break;
                case itemChar:
                    Program.item[curentItemCount++] = new Item(x, y);
                    break;
                case ButtonChar:
                    Program.button[currentButtonCount++] = new Button(x, y);
                    break;
                default:
                    break;
            }
            if(c == '\n')
            {
                x = 0;
                y++;
            }
            else
            {
                x++;
            }
        }
    }
}