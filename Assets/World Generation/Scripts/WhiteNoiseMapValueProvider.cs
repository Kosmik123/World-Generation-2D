using System;
using UnityEngine;

namespace WorldGeneration2D
{
    [CreateAssetMenu]
    public class WhiteNoiseMapValueProvider : MapValueProvider
    {
        [SerializeField]
        private int seed;

        [SerializeField, Min(2)]
        private uint density;

        [SerializeField]
        private bool inverted = true;

        public override float GetValue(float x, float y)
        {
            return GetValue(x, y, seed, density, inverted);
        }

        public float GetValue(float x, float y, int? seed = null, uint? density = null, bool? inverted = null)
        {
            return WhiteNoiseMapValueProvider.GetValue(x, y, 
                seed.HasValue ? (int)seed : this.seed, 
                density.HasValue ? (uint)density : this.density,
                inverted.HasValue ? (bool)inverted : this.inverted);
        }

        public static float GetValue(float x, float y, int seed, uint density, bool inverted)
        {
            int xInt = Mathf.RoundToInt(x);
            int yInt = Mathf.RoundToInt(y);

            var value = Hash2D(seed, xInt, yInt) % density;
            if (value > 0)
                value = 1;

            if (inverted)
                value = 1 - value;

            return value;
        }

        // copied from FastNoise for C#
        private const int X_PRIME = 1619;
        private const int Y_PRIME = 31337;
        private static uint Hash2D(int seed, int x, int y)
        {
            uint hash = (uint)seed;
            hash ^= X_PRIME * (uint)x;
            hash ^= Y_PRIME * (uint)y;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }
    }
}