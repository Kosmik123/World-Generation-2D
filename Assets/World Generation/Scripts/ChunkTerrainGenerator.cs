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
        private BiomeData[] biomes;

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
                    var climate = GetClimateData(tilePosition.x, tilePosition.y);

                    var biome = GetBiome(climate.temperature, climate.humidity);
                    terrainTilemap.SetTile(new Vector3Int(tileCoordX, tileCoordY), biome.Tile);
                }
            }
        }

        public ClimateData GetClimateData(float x, float y)
        {
            var temperature = temperatureMapProvider.GetValue(x, y);
            var humidity = humidityMapProvider.GetValue(x, y);
            return new ClimateData(temperature, humidity);
        }

        private BiomeData GetBiome(float temperature, float humidity)
        {
            foreach(var biome in biomes) 
                if (biome.CheckCondition(Mathf.Clamp01(temperature), Mathf.Clamp01(humidity)))
                    return biome;
            
            return null;
        }
    }

    public readonly struct ClimateData
    {
        public readonly float temperature;
        public readonly float humidity;

        public ClimateData(float temperature, float humidity)
        {
            this.temperature = temperature;
            this.humidity = humidity;
        }
    }
}
