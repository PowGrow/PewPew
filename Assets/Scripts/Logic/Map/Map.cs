namespace Pewpew.Logic.Map
{
    public struct Map
    {
        private int[,] _map;
        private int _radius;
        
        public int Radius
        {
            get => _radius;
        }

        public Map(int radius)
        {
            _map = new int[radius * 2, radius * 2];
            _radius = radius;
        }

        public int this[int x, int y]
        {
            get => GetValue(x,y);
            set => SetValue(x,y,value);
        }

        private int GetValue(int x, int y)
        {
            (int x, int y) values = ConvertValues(x, y);
            return _map[values.x, values.y];
        }

        private void SetValue(int x, int y, int value)
        {
            (int x, int y) values = ConvertValues(x,y);
            _map[values.x, values.y] = value;
        }

        private (int,int) ConvertValues(int x, int y)
        {
            x = _radius + x;
            y = _radius + y;
            return (x, y);
        }
    }
}
