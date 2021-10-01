namespace Quoridor
{
    public class Quoridor
    {
        public readonly Field Field;
        
        public Quoridor(int x, int y)
        {
            Field = new Field(new Vector2(x, y));
            SetupField();
        }

        private void SetupField()
        {
            //fill field
            for (int x = 0; x < Field.Field1.GetLength(0); x++)
            {
                for (int y = 0; y < Field.Field1.GetLength(1); y++)
                {
                    var field = Field.Field1;
                    Field.Field1[x, y] = new Cell(Field, new Vector2(Field.Size.x, Field.Size.y));
                }
            }
        }
    }
}