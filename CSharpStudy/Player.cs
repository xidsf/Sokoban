class Player : Unit
{
    public bool isGhost { get; private set; }  = false;
    public Direction playerMoveDir { get; private set; } = Direction.Right;

    public Player(int positionX, int positionY) : base(positionX, positionY, "▼")
    {
        isGhost = false;
        playerMoveDir = Direction.Right;
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
        CheckOutOfBound();
    }

    private void CheckOutOfBound()
    {
        if (positionX < 0)
        {
            positionX = 0;
        }
        if (positionY < 0)
        {
            positionY = 0;
        }
    }

    public void GetAbility()
    {
        isGhost = true;
    }

}