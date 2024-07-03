abstract class Unit
{
    public int positionX { get; protected set; } = 0;
    public int positionY { get; protected set; } = 0;
    protected string icon = string.Empty;
    

    public Unit(int positionX, int positionY, string icon)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.icon = icon;
    }

    virtual public void DrawIcon()
    {
        Console.SetCursorPosition(positionX, positionY);
        Console.Write(icon);
    }

    protected void DrawIcon(string otherIcon)
    {
        Console.SetCursorPosition(positionX, positionY);
        Console.Write(otherIcon);
    }

}