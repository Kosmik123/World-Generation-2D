using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration2D
{
    public class ChunksWorldManager : MonoBehaviour
    {
        [SerializeField]
        private Chunk chunkPrototype;
        [SerializeField]
        private ChunkSettings chunkSettings;

        private readonly Dictionary<Vector2Int, Chunk> chunksByCoord = new Dictionary<Vector2Int, Chunk>();

        private void Start()
        {
            var realChunkSize = chunkSettings.RealChunkSize;
            for (int j = -10; j <= 10; j++)
            {
                for (int i = -10; i <= 10; i++)
                {
                    var chunk = Instantiate(chunkPrototype, new Vector3(realChunkSize.x * i, realChunkSize.y * j, 0), Quaternion.identity, transform);
                    var chunkCoord = new Vector2Int(i, j);
                    chunk.Init(chunkSettings, chunkCoord);
                    chunksByCoord.Add(chunkCoord, chunk);
                }
            }
        }
    }
}
