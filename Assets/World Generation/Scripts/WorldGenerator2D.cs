using UnityEngine;

namespace WorldGeneration2D
{
    public class WorldGenerator2D : MonoBehaviour
    {
        [SerializeField]
        private Chunk chunkPrototype;
        [SerializeField]
        private ChunkSettings chunkSettings;

        private void Start()
        {
            var realChunkSize = chunkSettings.RealChunkSize;
            for (int j = -10; j <= 10; j++)
            {
                for (int i = -10; i <= 10; i++)
                {
                    var chunk = Instantiate(chunkPrototype, new Vector3(realChunkSize.x * i, realChunkSize.y * j, 0), Quaternion.identity, transform);
                    chunk.Init(chunkSettings, new Vector2Int(i, j));
                }
            }
        }
    }
}
