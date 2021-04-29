namespace Frontend.ViewModels
{
    public readonly struct ViewablePosition
    {
        public int X { get;}
        public int Y { get;}

        public ViewablePosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}