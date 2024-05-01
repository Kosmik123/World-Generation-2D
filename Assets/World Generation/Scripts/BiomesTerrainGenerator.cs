using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class BiomesTerrainGenerator : ForeachTileTerrainGenerator
    {
        [SerializeField]
        private MapValueProvider temperatureMapProvider;
        [SerializeField]
        private MapValueProvider humidityMapProvider;

        [SerializeField]
        private BiomeData[] biomes;

        protected override void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain)
        {
            var tilePosition = chunkTerrain.TerrainTilemap.CellToWorld(new Vector3Int(x, y));
            var climate = GetClimateData(tilePosition.x, tilePosition.y);

            var biome = GetBiome(climate.temperature, climate.humidity);
            chunkTerrain.TerrainTilemap.SetTile(new Vector3Int(x, y), biome.Tile);
        }

        public ClimateData GetClimateData(float x, float y)
        {
            var temperature = temperatureMapProvider.GetValue(x, y);
            var humidity = humidityMapProvider.GetValue(x, y);
            return new ClimateData(temperature, humidity);
        }

        private BiomeData GetBiome(float temperature, float humidity)
        {
            foreach (var biome in biomes)
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
