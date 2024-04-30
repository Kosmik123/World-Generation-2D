using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private GenerationSettings terrainGenerationSettings;

        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private Tile[] tiles;

        private void Start()
        {
            var coord = Vector2Int.RoundToInt(transform.position / 10);
            var startingTilePosition = -Vector2.one * 4.5f;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    var tilePosition = (Vector2)transform.position + startingTilePosition + new Vector2(i, j);
                    var noiseValue = terrainGenerationSettings.GetValue(tilePosition.x, tilePosition.y);
                    int tileIndex = Mathf.Clamp(Mathf.FloorToInt(noiseValue * tiles.Length), 0, tiles.Length - 1);

                    terrainTilemap.SetTile(new Vector3Int(i - 5, j - 5), tiles[tileIndex]);
                }
            }
        }
    }
}
