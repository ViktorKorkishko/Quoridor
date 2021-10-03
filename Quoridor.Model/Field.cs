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
    }
}