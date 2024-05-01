using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class RiversTerrainGenerator : ForeachTileTerrainGenerator
    {
        [SerializeField]
        private MapValueProvider riversMapProvider;

        [SerializeField]
        private TileBase riverTile;

        protected override void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain)
        {
            var tilePosition = chunkTerrain.TerrainTilemap.CellToWorld(new Vector3Int(x, y));
            float riverValue = riversMapProvider.GetValue(tilePosition.x, tilePosition.y);
            if (riverValue > 0.5f)
            {
                var tileCoord = new Vector3Int(x, y);
                chunkTerrain.TerrainTilemap.SetTile(tileCoord, riverTile);
                chunkTerrain.TerrainTilemap.SetColor(tileCoord, Color.white);
            }
        }
    }
}
