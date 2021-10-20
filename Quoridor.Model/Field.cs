namespace Quoridor
{
    public class Field
    {
        public readonly Vector2 Size;
        public readonly Cell[,] Cells;

        public Field(Vector2 size)
        {
            Cells = new Cell[size.x, size.y];
            Size = size;
        }

        public bool IsOutOfRange(Vector2 position)
        {
            if (position.x >= 0 && position.x < Size.x)
            {
                if (position.y >= 0 && position.y < Size.y)
                {
                    return false;
                }
            }

            return true;
        }
        
        public bool IsOutOfRange(Cell cell)
        {
            if (cell.Position.x >= 0 && cell.Position.x < Size.x)
            {
                if (cell.Position.y >= 0 && cell.Position.y < Size.y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}