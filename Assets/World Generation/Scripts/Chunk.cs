using UnityEngine;

namespace WorldGeneration2D
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int coord;
        public Vector2Int Coord => coord;

        private ChunkSettings settings;
        public ChunkSettings Settings => settings;

        public void Init(ChunkSettings chunkSettings, Vector2Int coord)
        {
            settings = chunkSettings;
            this.coord = coord;
        }
    }
}
