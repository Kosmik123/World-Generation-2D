using UnityEngine;

namespace WorldGeneration2D
{
    public class WorldGenerator2D : MonoBehaviour
    {
        [SerializeField]
        private WorldGeneratorSettings settings;

        [SerializeField]
        private int seed;

        [SerializeField]
        private GameObject chunkPrototype;

        private void Start()
        {
            using (new RandomSeed(seed))
            {
                float perlinXOffest = Random.Range(-10000, 10000);
                float perlinYOffest = Random.Range(-10000, 10000);
                for (int j = -10; j <= 10; j++)
                {
                    for (int i = -10; i <= 10; i++)
                    {
                        var chunk = Instantiate(chunkPrototype, new Vector3(10 * i, 10 * j, 0), Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}
