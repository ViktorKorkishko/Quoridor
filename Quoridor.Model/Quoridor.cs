namespace Quoridor
{
    public enum Turn
    {
        FirstPlayer = 0,
        SecondPlayer = 1
    }

    public class Quoridor
    {
        public readonly Player FirstPlayer;
        public readonly Player SecondPlayer;

        public Turn CurrentPlayerTurn { get; private set; }

        public readonly Field Field;

        public Quoridor(int fieldHorizontalSize, int fieldVerticalSize, Player firstPlayer, Player secondPlayer)
        {
            Field = new Field(new Vector2(fieldHorizontalSize, fieldVerticalSize));

            FirstPlayer = firstPlayer;
            //FirstPlayer.Move(new Vector2(fieldVerticalSize / 2, 0));
            FirstPlayer.MoveTo(new Vector2(1, 1));
            
            SecondPlayer = secondPlayer;
            //SecondPlayer.Move(new Vector2(fieldVerticalSize / 2, Field.Size.x - 1));
            FirstPlayer.MoveTo(new Vector2(2, 2));
            
            CurrentPlayerTurn = Turn.FirstPlayer;

            SetupField();
        }

        public bool TryMovePlayer(Direction direction)
        {
            Player player;
            switch (direction)
            {
                case Direction.Up:
                    player = GetCurrentPlayer();
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                    player.Move(Vector2.Up);
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;
                    break;

                case Direction.Down:
                    player = GetCurrentPlayer();
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                    player.Move(Vector2.Down);
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;
                    break;
                
                case Direction.Left:
                    player = GetCurrentPlayer();
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                    player.Move(Vector2.Left);
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;
                    break;
                
                case Direction.Right:
                    player = GetCurrentPlayer();
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                    player.Move(Vector2.Right);
                    Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;
                    break;
            }

            return false;
        }

        public Player GetCurrentPlayer()
        {
            if (CurrentPlayerTurn == Turn.FirstPlayer)
            {
                return FirstPlayer;
            }

            return SecondPlayer;
        }

        public void SwitchPlayer()
        {
            if (CurrentPlayerTurn == Turn.FirstPlayer)
            {
                CurrentPlayerTurn = Turn.SecondPlayer;
                return;
            }

            CurrentPlayerTurn = Turn.FirstPlayer;
        }

        private void SetupField()
        {
            // int centerIndex;
            // if (Field.Size.x % 2 == 1)
            // {
            //     centerIndex = Field.Size.y / 2;
            // }
            // else
            // {
            //     centerIndex = Field.Size.y / 2 - 1;
            // }

            for (int x = 0; x < Field.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Field.Cells.GetLength(1); y++)
                {
                    var newCell = new Cell(Field, new Vector2(x, y));
                    Field.Cells[x, y] = newCell;

                    if (FirstPlayer.Position == new Vector2(x, y))
                    {
                        newCell.PlayerOver = FirstPlayer;
                    }
                    else if (SecondPlayer.Position == new Vector2(x, y))
                    {
                        newCell.PlayerOver = SecondPlayer;
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