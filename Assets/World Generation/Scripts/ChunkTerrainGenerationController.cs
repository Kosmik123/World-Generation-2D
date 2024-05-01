using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    public abstract class TerrainGenerator : ScriptableObject
    {
        public abstract void ApplyTerrain(ChunkTerrain chunkTerrain);
    }

    public class ChunkTerrain
    {
        public Chunk Chunk { get; }
        public Tilemap TerrainTilemap { get; }

        public ChunkTerrain(Chunk chunk, Tilemap terrainTilemap)
        {
            Chunk = chunk;
            TerrainTilemap = terrainTilemap;
        }
    }

    [RequireComponent(typeof(Chunk))]
    public class ChunkTerrainGenerationController : MonoBehaviour
    {
        private Chunk _chunk;
        public Chunk Chunk
        {
            get
            {
                if (_chunk == null)
                    _chunk = GetComponent<Chunk>();
                return _chunk;
            }
        }

        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private TerrainGenerator[] terrainGenerators;

        private ChunkTerrain chunkTerrain;

        private void Reset()
        {
            terrainTilemap = GetComponentInChildren<Tilemap>();
        }

        private void Awake()
        {
            chunkTerrain = new ChunkTerrain(Chunk, terrainTilemap);    
        }

        private void Start()
        {
            GenerateTerrain();
        }

        private void GenerateTerrain()
        {
            foreach (var generator in terrainGenerators)
            {
                generator.ApplyTerrain(chunkTerrain);
            }
        }
    }
}
