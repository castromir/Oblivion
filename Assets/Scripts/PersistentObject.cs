using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    private void Awake()
    {
        // Verifica se já existe uma instância deste objeto
        if (instance != null)
        {
            // Se existir, destrua o novo objeto para evitar duplicações
            Destroy(gameObject);
            return;
        }

        // Define a instância como esta e faz o objeto persistir entre cenas
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
