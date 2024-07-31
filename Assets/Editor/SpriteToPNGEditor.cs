using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteToPNGEditor : MonoBehaviour
{
    [MenuItem("Assets/Export Selected Sprite to PNG")]
    private static void ExportSpriteToPNG()
    {
        // Obter o objeto selecionado na janela do Project
        Object selectedObject = Selection.activeObject;

        // Verificar se o objeto selecionado é um Sprite
        if (selectedObject is Sprite)
        {
            ExportSprite((Sprite)selectedObject);
        }
        else
        {
            // Tentar obter o sprite do objeto selecionado
            Texture2D texture = selectedObject as Texture2D;
            if (texture != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(texture);
                Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                foreach (Object asset in assets)
                {
                    if (asset is Sprite)
                    {
                        ExportSprite((Sprite)asset);
                        return;
                    }
                }
            }

            Debug.LogWarning("Please select a sprite in the Project view.");
        }
    }

    private static void ExportSprite(Sprite sprite)
    {
        Texture2D texture = sprite.texture;

        // Criar uma nova Texture2D com as dimensões do sprite
        Texture2D newTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        newTexture.SetPixels(texture.GetPixels((int)sprite.textureRect.x,
                                               (int)sprite.textureRect.y,
                                               (int)sprite.textureRect.width,
                                               (int)sprite.textureRect.height));
        newTexture.Apply();

        // Codificar a textura para PNG
        byte[] bytes = newTexture.EncodeToPNG();
        string path = EditorUtility.SaveFilePanel("Save Sprite as PNG", "", sprite.name + ".png", "png");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllBytes(path, bytes);
            Debug.Log("Sprite exported to " + path);
        }
    }
}
