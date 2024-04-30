using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private TerrainGenerationSettings terrainGenerationSettings;

        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private Tile groundTile;

        private void Start()
        {
            var coord = Vector2Int.RoundToInt(transform.position / 10);
            var startingTilePosition = -Vector2.one * 4.5f;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    var tilePosition = (Vector2)transform.position + startingTilePosition + new Vector2(i, j);
                    var height = terrainGenerationSettings.GetValue(tilePosition.x, tilePosition.y);
                    if (height > 0.5f)
                    {
                        terrainTilemap.SetTile(new Vector3Int(i - 5, j - 5), groundTile);
                    }
                }
            }
        }
    }
}
