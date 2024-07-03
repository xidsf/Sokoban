using System.Numerics;

class Item : Unit
{
    public bool isEatable;

    public Item(int x, int y, bool isEatable = true) : base(x, y, "♪")
    {
        this.isEatable = isEatable;
    }


    public void GivePlayerAbility(Player player)
    {
        if (isEatable == false)
        {
            return;
        }
        player.GetAbility();
        isEatable = false;
    }

    public override void DrawIcon()
    {
        if(isEatable == false) 
        {
            return; 
        }
        base.DrawIcon();
    }

}