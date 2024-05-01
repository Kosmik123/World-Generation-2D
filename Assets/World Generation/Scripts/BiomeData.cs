using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class BiomeData : ScriptableObject
    {
        [SerializeField]
        private TileBase tile;
        public TileBase Tile => tile;

        [SerializeField, Range(0, 1)]
        private float minTemperature = 0;
        [SerializeField, Range(0, 1)]
        private float maxTemperature = 1;
        [SerializeField, Range(0, 1)]
        private float minHumidity = 0;
        [SerializeField, Range(0, 1)]
        private float maxHumidity = 1;

        public bool CheckCondition(float temperature01, float humidity01)
        {
            return minTemperature <= temperature01 && temperature01 <= maxTemperature 
                && minHumidity <= humidity01 && humidity01 <= maxHumidity;
        }
    }
}
