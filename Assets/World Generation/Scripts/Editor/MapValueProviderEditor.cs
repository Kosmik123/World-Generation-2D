using UnityEditor;
using UnityEngine;

namespace WorldGeneration2D
{
    [CustomEditor(typeof(MapValueProvider), editorForChildClasses: true)]
    public class MapValueProviderEditor : Editor
    {
        private const int resolution = 100;

        private Texture2D _previewTexture;
        public Texture2D PreviewTexture
        {
            get
            {
                if (_previewTexture == null)
                    _previewTexture = CreateTexture();
                return _previewTexture;
            }
        }

        private static Texture2D CreateTexture()
        {
            return new Texture2D(resolution, resolution, TextureFormat.RGBA32, false, true)
            {
                wrapMode = TextureWrapMode.Clamp
            };
        }

        private readonly Color32[] colors = new Color32[resolution * resolution];

        public override bool HasPreviewGUI() => true;

        private void OnEnable()
        {
            PopulatePreviewTexture();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck() || _previewTexture == null)
            {
                PopulatePreviewTexture();
            }
        }

        private void PopulatePreviewTexture()
        {
            var provider = target as MapValueProvider;
            float halfResolution = resolution / 2;
            for (int j = 0; j < resolution; j++)
            {
                for (int i = 0; i < resolution; i++)
                {
                    // one pixel in preview = one unit in world
                    var pixelValue = provider.GetValue(i - halfResolution, j - halfResolution);
                    colors[j * resolution + i] = Color.Lerp(Color.black, Color.white, pixelValue);
                }
            }

            PreviewTexture.SetPixels32(colors);
            PreviewTexture.Apply();
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            base.OnPreviewGUI(r, background);
            var textureRect = r;
            EditorGUI.DrawPreviewTexture(textureRect, PreviewTexture);
        }
    }
}
