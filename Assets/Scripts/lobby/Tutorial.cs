using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text moveInstruction;
    public Text teleportInstruction;
    public Text attackInstruction;

    private bool moveComplete = false;
    private bool teleportComplete = false;

    void Start()
    {
        // Inicialmente, mostrar apenas a instrução de movimento
        moveInstruction.gameObject.SetActive(true);
        teleportInstruction.gameObject.SetActive(false);
        attackInstruction.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!moveComplete && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            moveComplete = true;
            moveInstruction.gameObject.SetActive(false);
            teleportInstruction.gameObject.SetActive(true);
        }

        if (moveComplete && !teleportComplete && Input.GetKeyDown(KeyCode.Space))
        {
            teleportComplete = true;
            teleportInstruction.gameObject.SetActive(false);
            attackInstruction.gameObject.SetActive(true);
        }

        if (teleportComplete && Input.GetMouseButtonDown(0))
        {
            attackInstruction.gameObject.SetActive(false);
            Debug.Log("Tutorial Completo!");
        }
    }
}
