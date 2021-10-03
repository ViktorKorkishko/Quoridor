namespace Quoridor
{
    public class Quoridor
    {
        public readonly Player FirstPlayer;
        public readonly Player SecondPlayer;
        
        public Player CurrentPlayer { get; private set; }

        public readonly Field Field;

        public Quoridor(int fieldHorizontalSize, int fieldVerticalSize, Player firstPlayer, Player secondPlayer)
        {
            Field = new Field(new Vector2(fieldHorizontalSize, fieldVerticalSize));
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            CurrentPlayer = FirstPlayer;
            SetupField();
        }

        private bool TryMovePlayer(Direction direction)
        {
            // switch (direction)
            // {
            //     case Direction.Up:
            //         if ()
            //         {
            //         
            //         }
            //
            //         break;
            //     case Direction.Down:
            //
            //         break;
            //     case Direction.Right:
            //
            //         break;
            //     case Direction.Left:
            //
            //         break;
            //     default:
            //         
            //         break;
            // }
            return false;
        }

        public void SwitchPlayer()
        {
            if (CurrentPlayer == FirstPlayer)
            {
                CurrentPlayer = SecondPlayer;
            }
            else
            {
                CurrentPlayer = FirstPlayer;
            }
        }

        private void SetupField()
        {
            int centerIndex;
            if (Field.Size.x % 2 == 1)
            {
                centerIndex = Field.Size.y / 2;
            }
            else
            {
                centerIndex = Field.Size.y / 2;
            }

            for (int x = 0; x < Field.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Field.Cells.GetLength(1); y++)
                {
                    var newCell = new Cell(Field, new Vector2(Field.Size.x, Field.Size.y));
                    Field.Cells[x, y] = newCell;
                    if (y == centerIndex)
                    {
                        if (x == 0)
                        {
                            newCell.PlayerOver = FirstPlayer;
                        } 
                        else if (x == Field.Size.x - 1)
                        {
                            newCell.PlayerOver = SecondPlayer;
                        }
                    }
                }
            }
        }

        private bool IsOutOfRange(Vector2 position)
        {
            if (position.x < Field.Size.x && position.x >= 0)
            {
                if (position.y < Field.Size.y && position.y >= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}