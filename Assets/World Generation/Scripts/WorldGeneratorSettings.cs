using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class WorldGeneratorSettings : ScriptableObject
    {
        [SerializeField]
        private Vector2Int chunkSize;
    }
}
