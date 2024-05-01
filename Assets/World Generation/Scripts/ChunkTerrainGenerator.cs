using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    [RequireComponent(typeof(Chunk))]
    public class ChunkTerrainGenerator : MonoBehaviour
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
        private MapValueProvider temperatureMapProvider;
        [SerializeField]
        private MapValueProvider humidityMapProvider;

        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private TileBase[] tiles;

        private void Start()
        {
            GenerateTerrain();
        }

        public void GenerateTerrain()
        {
            var settings = Chunk.Settings;
            Vector2Int halfChunkSize = settings.ChunkSize / 2;
            var startingTileLocalPosition = (-settings.RealChunkSize + settings.TileSize) / 2;
            for (int j = 0; j < settings.ChunkSize.y; j++)
            {
                for (int i = 0; i < settings.ChunkSize.x; i++)
                {
                    int tileCoordX = i - halfChunkSize.x;
                    int tileCoordY = j - halfChunkSize.y;

                    var tilePosition = Chunk.Coord * settings.RealChunkSize + startingTileLocalPosition + new Vector2(i, j);
                    
                    var temperature = temperatureMapProvider.GetValue(tilePosition.x, tilePosition.y);
                    var humidity = temperatureMapProvider.GetValue(tilePosition.x, tilePosition.y);

                    int tileIndex = Mathf.Clamp(Mathf.FloorToInt((humidity + temperature) / 2 * tiles.Length), 0, tiles.Length - 1);

                    terrainTilemap.SetTile(new Vector3Int(tileCoordX, tileCoordY), tiles[tileIndex]);
                }
            }
        }
    }
}
