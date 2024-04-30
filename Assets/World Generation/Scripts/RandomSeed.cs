using UnityEngine;

namespace WorldGeneration2D
{
    public readonly struct RandomSeed : System.IDisposable
    {
        private readonly Random.State previousState;

        public RandomSeed(int seed)
        {
            previousState = Random.state;
            Random.InitState(seed);
        }

        public void Dispose()
        {
            Random.state = previousState;
        }
    }
}
