using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class RiversTerrainGenerator : TerrainGenerator
    {
        [SerializeField]
        private MapValueProvider riversMapProvider;

        [SerializeField]
        private TileBase riverTile;

        public override void ApplyTerrain(ChunkTerrain chunkTerrain)
        {
            var settings = chunkTerrain.Chunk.Settings;
            Vector2Int halfChunkSize = settings.ChunkSize / 2;
            var startingTileLocalPosition = (-settings.RealChunkSize + settings.TileSize) / 2;
            for (int j = 0; j < settings.ChunkSize.y; j++)
            {
                for (int i = 0; i < settings.ChunkSize.x; i++)
                {
                    int tileCoordX = i - halfChunkSize.x;
                    int tileCoordY = j - halfChunkSize.y;

                    var tilePosition = chunkTerrain.Chunk.Coord * settings.RealChunkSize + startingTileLocalPosition + new Vector2(i, j);

                    float riverValue = riversMapProvider.GetValue(tilePosition.x, tilePosition.y);
                    if (riverValue > 0.5f)
                    {
                        var tileCoord = new Vector3Int(tileCoordX, tileCoordY);
                        chunkTerrain.TerrainTilemap.SetTile(tileCoord, riverTile);
                        chunkTerrain.TerrainTilemap.SetColor(tileCoord, Color.white);
                    }
                }
            }
        }
    }
}
