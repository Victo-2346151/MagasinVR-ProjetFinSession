using UnityEngine;

public class SortieScript : MonoBehaviour
{
    private bool sortieActive = false;
    private GameManager gameManager;

    void Start()
    {
        // FindFirstObjectByType car la sortie est placée manuellement
        // (solution suggérée par Claude AI)
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void ActiverSortie()
    {
        sortieActive = true;
        GetComponent<Renderer>().material.color = Color.green;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!sortieActive) return;

        if (other.CompareTag("Player"))
        {
            gameManager.VisiteComplete();
        }
    }
}