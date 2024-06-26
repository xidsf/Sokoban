class Item
{
    public readonly int positionX;
    public readonly int positionY;
    public bool isEatable;
    public string itemIcon = "♪";

    public Item(int x, int y, bool isEatable = true)
    {
        positionX = x;
        positionY = y;
        this.isEatable = isEatable;
    }

    public void MakePlayerGhost(Player player)
    {
        if(isEatable == false)
        {
            return;
        }
        player.GetItme();
        isEatable = false;
    }

}