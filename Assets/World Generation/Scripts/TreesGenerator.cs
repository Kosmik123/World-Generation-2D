using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class TreesGenerator : TerrainGenerator
    {
        [SerializeField]
        private GameObject[] treePrototypes;

        public override void ApplyTerrain(ChunkTerrain chunkTerrain)
        {
            var size = chunkTerrain.Chunk.Settings.ChunkSize;
            Vector2Int halfChunkSize = chunkTerrain.Chunk.Settings.ChunkSize / 2;
            for (int j = 0; j < size.y; j++)
            {
                for (int i = 0; i < size.x; i++)
                {
                    bool isTree = Random.Range(0, 100) < 1;
                    if (isTree)
                    {
                        int tileCoordX = i - halfChunkSize.x;
                        int tileCoordY = j - halfChunkSize.y;
                        var treePrototype = treePrototypes[Random.Range(0, treePrototypes.Length)];
                        var treePosition = chunkTerrain.TerrainTilemap.CellToWorld(new Vector3Int(tileCoordX, tileCoordY));
                        var tree = Instantiate(treePrototype, treePosition, Quaternion.identity, chunkTerrain.Chunk.transform);
                    }
                }
            }
        }
    }
}
