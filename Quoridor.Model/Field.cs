namespace Quoridor
{
    public class Field
    {
        public readonly Vector2 Size;
        public readonly Cell[,] Field1;

        public Field(Vector2 size)
        {
            Field1 = new Cell[size.x, size.y];
            Size = size;
        }
    }
}