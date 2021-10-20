namespace Quoridor
{
    public struct DeprecatedPath
    {
        public readonly Vector2 FirstPoint;
        public readonly Vector2 SecondPoint;

        public DeprecatedPath(Vector2 firstPoint, Vector2 secondPoint)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
        }

        public override string ToString() => $"[{FirstPoint}, {SecondPoint}]";
    }
}