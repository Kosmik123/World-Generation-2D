using UnityEngine;

namespace WorldGeneration2D
{
    public class WorldGenerator2D : MonoBehaviour
    {
        [SerializeField]
        private int seed;

        private void Start()
        {
            using (new RandomSeed(seed))
            {
                float perlinXOffest = Random.Range(-10000, 10000);
                float perlinYOffest = Random.Range(-10000, 10000);
                for (int j = 0; j < 10; j++)
                for (int i = 0; i < 10; i++)
                {
                    var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    float height = 10 * Mathf.PerlinNoise(perlinXOffest + i / 10f, perlinYOffest + j / 10f);
                    gameObject.transform.position = new Vector3(i, j, height);
                }
            }
        }
    }

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
