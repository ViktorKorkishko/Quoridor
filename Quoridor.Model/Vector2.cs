namespace Quoridor
{
    public struct Vector2
    {
        public readonly int x;
        public readonly int y;

        public static Vector2 Up => new Vector2(0, 1);
        public static Vector2 Down => new Vector2(0, -1);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Zero => new Vector2(0, 0);

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);

        public static bool operator ==(Vector2 a, Vector2 b) => a.x - b.x + a.y - b.y == 0;
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        public override string ToString() => $"({x}, {y})";
    }
}