using UnityEngine;
using TMPro;

// Gère la progression de la visite
// Suit le nombre de tableaux examinés et notifie le GameManager quand la visite est complète
public class MuseeManager : MonoBehaviour
{
    [Header("Paramètres")]
    [SerializeField] private int nombreTotalTableaux = 9;

    [Header("Références")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI texteProgression;
    [SerializeField] private SortieScript sortie;

    private int nombreTableauxExamines = 0;

    // Appelée par TableauScript quand un tableau est examiné pour la première fois
    public void TableauExamine()
    {
        nombreTableauxExamines++;
        AfficherProgression();

        // Vérifier si tous les tableaux ont été examinés
        if (nombreTableauxExamines >= nombreTotalTableaux)
        {
            // Activer la porte de sortie
            if (sortie != null)
                sortie.ActiverSortie();

            // Notifier le GameManager pour afficher le canvas de fin
            if (gameManager != null)
                gameManager.VisiteComplete();
        }
    }

    // Met à jour le texte de progression dans le HUD
    private void AfficherProgression()
    {
        if (texteProgression != null)
        {
            texteProgression.text = nombreTableauxExamines + "/"
                + nombreTotalTableaux + " villes visitées";
        }
    }
}