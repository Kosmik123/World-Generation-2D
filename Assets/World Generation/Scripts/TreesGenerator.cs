using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class TreesGenerator : ForeachTileTerrainGenerator
    {
        [SerializeField]
        private GameObject[] treePrototypes;

        [SerializeField]
        private TileBase[] validTilesForTree;

        [SerializeField]
        private MapValueProvider treeMapValueProvider;

        protected override void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain)
        {
            Vector3Int coord = new Vector3Int(x, y);
            Vector3 worldPosition = chunkTerrain.TerrainTilemap.CellToWorld(coord);
            bool isTree = treeMapValueProvider.GetValue(worldPosition.x, worldPosition.y) > 0.5f;
            if (isTree)
            {
                if (System.Array.IndexOf(validTilesForTree, chunkTerrain.TerrainTilemap.GetTile(coord)) >= 0)
                {
                    var treePrototype = treePrototypes[Random.Range(0, treePrototypes.Length)];
                    var treePosition = (Vector2)chunkTerrain.TerrainTilemap.CellToWorld(coord) + (0.5f * chunkTerrain.Chunk.Settings.TileSize);
                    Instantiate(treePrototype, treePosition, Quaternion.identity, chunkTerrain.Chunk.transform);
                }
            }
        }
    }
}
