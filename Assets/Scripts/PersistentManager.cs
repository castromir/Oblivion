using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    public GameObject prefab;

    private GameObject persistentObjectInstance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Instanciar o objeto filho a partir do prefab
        if (prefab != null)
        {
            persistentObjectInstance = Instantiate(prefab);
            DontDestroyOnLoad(persistentObjectInstance);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject novoPai = GameObject.Find("Canvas");
        if (novoPai != null)
        {
            transform.SetParent(novoPai.transform);
        }
    }
}
