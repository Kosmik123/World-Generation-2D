using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration2D
{
    public class ChunksWorldManager : MonoBehaviour
    {
        [SerializeField]
        private Chunk chunkPrototype;
        [SerializeField]
        private ChunkSettings chunkSettings;

        [SerializeField]
        private Camera observerCamera;

        private readonly Dictionary<Vector2Int, Chunk> chunksByCoord = new Dictionary<Vector2Int, Chunk>();
        private readonly List<Vector2Int> coordsVisibleLastFrame = new List<Vector2Int>();

        [SerializeField]
        private int chunksCount = 0;

        private void Reset()
        {
            observerCamera = Camera.main;
        }

        private void Start()
        {
            chunksCount = 0;
            UpdateChunks();
        }

        private void Update()
        {
            UpdateChunks();
        }

        private void UpdateChunks()
        {
            foreach (var coord in coordsVisibleLastFrame)
                if (chunksByCoord.TryGetValue(coord, out var chunk))
                    chunk.gameObject.SetActive(false);

            coordsVisibleLastFrame.Clear();

            float yExtent = observerCamera.orthographicSize;
            float xExtent = yExtent * observerCamera.aspect;

            var observerRelativePosition = observerCamera.transform.position - transform.position;
            var bottomLeft = WorldToCoord(new Vector2(
                Mathf.Floor(observerRelativePosition.x - xExtent),
                Mathf.Floor(observerRelativePosition.y - yExtent)));

            var topRight = WorldToCoord(new Vector2(
                Mathf.Ceil(observerRelativePosition.x + xExtent),
                Mathf.Ceil(observerRelativePosition.y + yExtent)));

            for (int j = bottomLeft.y - 1; j <= topRight.y + 1; j++)
            {
                for (int i = bottomLeft.x - 1; i <= topRight.x + 1; i++)
                {
                    var coord = new Vector2Int(i, j);
                    if (chunksByCoord.TryGetValue(coord, out var chunk) == false)
                    {
                        chunk = Instantiate(chunkPrototype, new Vector3(chunkSettings.RealChunkSize.x * i, chunkSettings.RealChunkSize.y * j, 0), Quaternion.identity, transform);
                        chunk.Init(chunkSettings, coord);
                        chunksByCoord.Add(coord, chunk);
                    }
                    coordsVisibleLastFrame.Add(coord);
                    chunk.gameObject.SetActive(true);
                }
            }
            chunksCount = chunksByCoord.Count;
        }

        public Vector2Int WorldToCoord(Vector2 relativePosition)
        {
            return Vector2Int.RoundToInt(relativePosition / chunkSettings.RealChunkSize);
        }
    }
}
