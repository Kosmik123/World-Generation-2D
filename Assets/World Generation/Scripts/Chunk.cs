using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private GenerationSettings terrainGenerationSettings;

        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private TileBase[] tiles;

        [SerializeField]
        private Vector2Int coord;
        public Vector2Int Coord => coord;

        private ChunkSettings settings;

        public void Init(ChunkSettings chunkSettings, Vector2Int coord)
        {
            settings = chunkSettings;
            this.coord = coord;
        }

        private void Start()
        {
            Vector2Int halfChunkSize = settings.ChunkSize / 2;
            var startingTileLocalPosition = (-settings.RealChunkSize + settings.TileSize) / 2;
            for (int j = 0; j < settings.ChunkSize.y; j++)
            {
                for (int i = 0; i < settings.ChunkSize.x; i++)
                {
                    int tileCoordX = i - halfChunkSize.x;
                    int tileCoordY = j - halfChunkSize.y;

                    var tilePosition = coord * settings.RealChunkSize + startingTileLocalPosition + new Vector2(i, j);
                    var noiseValue = terrainGenerationSettings.GetValue(tilePosition.x, tilePosition.y);
                    int tileIndex = Mathf.Clamp(Mathf.FloorToInt(noiseValue * tiles.Length), 0, tiles.Length - 1);

                    terrainTilemap.SetTile(new Vector3Int(tileCoordX, tileCoordY), tiles[tileIndex]);
                }
            }
        }
    }
}
