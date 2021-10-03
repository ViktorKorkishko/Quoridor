namespace Quoridor
{
    public class Player
    {
        public readonly string Name;
        public Vector2 Position { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void Move(Vector2 position)
        {
            Position = position;
        }
    }
}