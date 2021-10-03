namespace Quoridor
{
    public struct Vector2
    {
        public readonly int x;
        public readonly int y;

        public Vector2 Up
        {
            get
            {
                return new Vector2(0, 1);
            }
        }

        public Vector2 Down
        {
            get { return new Vector2(0, -1); }
        }

        public Vector2 Left
        {
            get { return new Vector2(-1, 0); }
        }

        public Vector2 Right
        {
            get { return new Vector2(1, 0); }
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}