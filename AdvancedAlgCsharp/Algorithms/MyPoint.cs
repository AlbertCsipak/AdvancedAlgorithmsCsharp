namespace AdvancedAlgCsharp.Algorithms
{
    public class MyPoint
    {
        static int IDCounter = 0;
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public MyPoint(int x, int y)
        {
            ID = IDCounter++;
            X = x;
            Y = y;
        }
        public MyPoint()
        {
            ID = IDCounter++;
        }
        public MyPoint DeepCopy()
        {
            MyPoint p2 = new MyPoint();
            p2.X = X;
            p2.Y = Y;
            return p2;
        }
    };
}
