using System.Globalization;

namespace Quoridor
{
    public class Cell
    {
        public Player PlayerOver { get; set; }

        private readonly Field _field;
        public Vector2 Position;

        public Cell RightCell => _field.Cells[Position.x + 1, Position.y];
        public Cell LeftCell => _field.Cells[Position.x - 1, Position.y];
        public Cell UpperCell => _field.Cells[Position.x, Position.y + 1];
        public Cell LowerCell => _field.Cells[Position.x, Position.y - 1];

        public Cell(Field field, Vector2 position)
        {
            _field = field;
            Position = position;
        }
    }
}