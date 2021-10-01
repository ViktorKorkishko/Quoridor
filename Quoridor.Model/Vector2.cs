namespace Quoridor
{
    public struct Vector2
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}