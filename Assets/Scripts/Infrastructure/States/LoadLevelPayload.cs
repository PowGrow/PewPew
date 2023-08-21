namespace Pewpew.Infrastructure.States
{
    public struct LoadLevelPayload
    {
        public float BorderSize { get; private set; }
        public int AsteroidDensity { get; private set; }

        public LoadLevelPayload(float borderSize, int asteroidDensity)
        {
            BorderSize = borderSize;
            AsteroidDensity = asteroidDensity;
        }
    }
}
