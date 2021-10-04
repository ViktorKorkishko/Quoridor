namespace Quoridor
{
    public class Player
    {
        public readonly string Name;
        public Vector2 Position { get; private set; }

        public Player() : this("Player") { }

        public Player(string name)
        {
            Name = name;
        }

        public void MoveTo(Vector2 newPosition)
        {
            Position = newPosition;
        }

        internal void Move(Vector2 direction)
        {
            Position += direction;
        }
    }
}