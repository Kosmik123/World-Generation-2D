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

        protected override void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain)
        {
            bool isTree = Random.Range(0, 100) < 1;
            if (isTree)
            {
                Vector3Int coord = new Vector3Int(x, y);
                if (System.Array.IndexOf(validTilesForTree, chunkTerrain.TerrainTilemap.GetTile(coord)) >= 0)
                {
                    var treePrototype = treePrototypes[Random.Range(0, treePrototypes.Length)];
                    var treePosition = chunkTerrain.TerrainTilemap.CellToWorld(coord);
                    Instantiate(treePrototype, treePosition, Quaternion.identity, chunkTerrain.Chunk.transform);
                }
            }
        }
    }
}
