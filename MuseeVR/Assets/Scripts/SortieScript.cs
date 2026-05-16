using UnityEngine;

// Gère la porte de sortie du musée
// La porte est bloquante au départ et devient traversable quand tous les tableaux sont examinés
public class SortieScript : MonoBehaviour
{
    private bool sortieActive = false;
    private GameManager gameManager;
    private BoxCollider boxCollider;

    // Initialise la porte en mode bloquant
    void Start()
    {
        // FindFirstObjectByType car la sortie est placée manuellement
        // (solution suggérée par Claude AI)
        gameManager = FindFirstObjectByType<GameManager>();
        boxCollider = GetComponent<BoxCollider>();

        // Au départ la porte bloque le passage
        boxCollider.isTrigger = false;
    }

    // Appelée par MuseeManager quand tous les tableaux sont examinés
    public void ActiverSortie()
    {
        sortieActive = true;

        // La porte devient traversable et change de couleur
      
        boxCollider.isTrigger = true;
        GetComponent<Renderer>().material.color = Color.green;
    }

    // Détecte quand le joueur passe la porte de sortie
    void OnTriggerEnter(Collider other)
    {
        if (!sortieActive) return;

        if (other.CompareTag("Player"))
        {
            gameManager.VisiteComplete();
        }
    }
}