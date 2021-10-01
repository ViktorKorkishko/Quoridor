using System.Globalization;

namespace Quoridor
{
    public class Cell
    {
        private Player PlayerOver = Player.None;
        
        private readonly Field _field;
        public Vector2 Position;
        
        // get => some field cell
        public Cell RightCell { get; private set; }
        public Cell LeftCell { get; private set; }
        public Cell UpperCell { get; private set; }
        public Cell LowerCell{ get; private set; }

        public Cell(Field field, Vector2 position)
        {
            _field = field;
            Position = position;
        }
    }
}