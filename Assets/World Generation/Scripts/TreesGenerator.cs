using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class TreesGenerator : ForeachTileTerrainGenerator
    {
        [SerializeField]
        private GameObject[] treePrototypes;

        protected override void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain)
        {
            bool isTree = Random.Range(0, 100) < 1;
            if (isTree)
            {
                var treePrototype = treePrototypes[Random.Range(0, treePrototypes.Length)];
                var treePosition = chunkTerrain.TerrainTilemap.CellToWorld(new Vector3Int(x, y));
                Instantiate(treePrototype, treePosition, Quaternion.identity, chunkTerrain.Chunk.transform);
            }
        }
    }
}
