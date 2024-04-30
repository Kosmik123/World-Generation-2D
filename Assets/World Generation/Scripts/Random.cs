namespace WorldGeneration2D
{
    public readonly struct Random : System.IDisposable
    {
        private readonly UnityEngine.Random.State previousState;

        public static Random Seed(int seed) => new Random(seed);

        private Random(int seed)
        {
            previousState = UnityEngine.Random.state;
            UnityEngine.Random.InitState(seed);
        }

        public void Dispose()
        {
            UnityEngine.Random.state = previousState;
        }
    }
}
