using UnityEngine;
using WorldGeneration2D;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField]
    private MapValueProvider mapProvider1;
    [SerializeField]
    private MapValueProvider mapProvider2;
    [SerializeField]
    private MapValueProvider mapProvider3;

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private Texture2D visualizationTexture;

    private const int resolution = 100;

    private void Start()
    {
        CreateVisualization();
        spriteRenderer.sprite = Sprite.Create(visualizationTexture, new Rect(0,0, resolution, resolution), new Vector2(0.5f, 0.5f));
    }

    private void CreateVisualization()
    {
        spriteRenderer.transform.localScale = 10 * Vector3.one;

        var position = transform.position;
        visualizationTexture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false, true);
        visualizationTexture.wrapMode = TextureWrapMode.Clamp;
        var pixels = new Color32[resolution * resolution];


        for (int j = 0; j < resolution; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                var x = position.x + i / 10f;
                var y = position.y + j / 10f;
                float value1 = mapProvider1 ? mapProvider1.GetValue(x, y) : 0.5f;
                float value2 = mapProvider2 ? mapProvider2.GetValue(x, y) : 0.5f;
                float value3 = mapProvider3 ? mapProvider3.GetValue(x, y) : 0.5f;

                pixels[j * resolution + i] = new Color(value1, value2, value3, 1);
            }
        }

        visualizationTexture.SetPixels32(pixels);
        visualizationTexture.Apply();
    }
}
