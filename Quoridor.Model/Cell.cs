namespace Quoridor
{
    public class Cell
    {
        public Player PlayerOver { get; set; }

        private readonly Field _field;
        public Vector2 Position;

        public Cell RightCell
        {
            get
            {
                var rightCellPosition = new Vector2(Position.x, Position.y + 1);
                if (!_field.IsOutOfRange(rightCellPosition))
                {
                    return _field.Cells[rightCellPosition.x, rightCellPosition.y];
                }

                return null;
            }
        }

        public Cell LeftCell
        {
            get
            {
                var leftCellPosition = new Vector2(Position.x, Position.y - 1);
                if (!_field.IsOutOfRange(leftCellPosition))
                {
                    return _field.Cells[leftCellPosition.x, leftCellPosition.y];
                }

                return null;
            }
        }

        public Cell UpperCell
        {
            get
            {
                var upperCellPosition = new Vector2(Position.x - 1, Position.y);
                if (!_field.IsOutOfRange(upperCellPosition))
                {
                    return _field.Cells[upperCellPosition.x, upperCellPosition.y];
                }

                return null;
            }
        }

        public Cell LowerCell
        {
            get
            {
                var lowerCellPosition = new Vector2(Position.x + 1, Position.y);
                if (!_field.IsOutOfRange(lowerCellPosition))
                {
                    return _field.Cells[lowerCellPosition.x, lowerCellPosition.y];
                }

                return null;
            }
        }

        public Cell(Field field, Vector2 position)
        {
            _field = field;
            Position = position;
        }
    }
}