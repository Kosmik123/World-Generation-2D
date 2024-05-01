using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class ChunkSettings : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Tiles count along each axis")]
        private Vector2Int chunkSize;
        public Vector2Int ChunkSize => chunkSize;

        [SerializeField]
        private Vector2 tileSize = Vector2.one;
        public Vector2 TileSize => tileSize;

        public Vector2 RealChunkSize => tileSize * chunkSize;
    }
}

