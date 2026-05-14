using UnityEngine;

public class SortieScript : MonoBehaviour
{
    private bool sortieActive = false;
    private GameManager gameManager;
    private BoxCollider boxCollider;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        boxCollider = GetComponent<BoxCollider>();

        // Au départ la porte bloque
        boxCollider.isTrigger = false;
    }

    public void ActiverSortie()
    {
        sortieActive = true;
        // La porte devient traversable et verte
        boxCollider.isTrigger = true;
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