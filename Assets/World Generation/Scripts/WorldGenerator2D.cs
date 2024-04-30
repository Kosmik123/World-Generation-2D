using UnityEngine;

namespace WorldGeneration2D
{
    public class WorldGenerator2D : MonoBehaviour
    {
        [SerializeField]
        private WorldGeneratorSettings settings;

        [SerializeField]
        private int seed;

        private void Start()
        {
            using (new RandomSeed(seed))
            {
                float perlinXOffest = Random.Range(-10000, 10000);
                float perlinYOffest = Random.Range(-10000, 10000);
                for (int j = -100; j < 100; j++)
                {
                    for (int i = -100; i < 100; i++)
                    {
                        var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        float height = Mathf.PerlinNoise(perlinXOffest + i / 10f, perlinYOffest + j / 10f);
                        tile.transform.position = new Vector3(i, j, 10 * height);
                    }
                }
            }
        }
    }
}
