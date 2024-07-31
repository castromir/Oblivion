using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        ScaleBackground();
    }

    void ScaleBackground()
    {
        // Obter o componente SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Obter o tamanho do sprite
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Obter a câmera principal
        Camera mainCamera = Camera.main;

        // Calcular a altura e largura da tela em unidades do mundo
        float screenHeight = mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Calcular a escala necessária para preencher a tela
        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteSize.x;
        scale.y = screenHeight / spriteSize.y;
        transform.localScale = scale;
    }
}