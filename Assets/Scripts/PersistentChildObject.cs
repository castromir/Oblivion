using UnityEngine;

public class PersistentChildObject : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = gameObject;
        DontDestroyOnLoad(gameObject);

        // Remove o objeto da hierarquia pai
        transform.SetParent(null);
    }

    private void OnLevelWasLoaded(int level)
    {
        // Reorganize o objeto na hierarquia, se necessário
        // Por exemplo, encontre um novo pai com base em um critério na nova cena
        GameObject novoPai = GameObject.Find("Canvas");
        if (novoPai != null)
        {
            transform.SetParent(novoPai.transform);
        }
    }
}
