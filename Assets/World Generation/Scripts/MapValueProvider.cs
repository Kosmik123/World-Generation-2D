using UnityEngine;

namespace WorldGeneration2D
{
    public abstract class MapValueProvider : ScriptableObject
    {
        public abstract float GetValue(float x, float y);
    }
}