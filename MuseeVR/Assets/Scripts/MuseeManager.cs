using UnityEngine;
using TMPro;

public class MuseeManager : MonoBehaviour
{
    [Header("Paramètres")]
    [SerializeField] private int nombreTotalTableaux = 9;

    [Header("Références")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI texteProgression;
    [SerializeField] private SortieScript sortie;

    private int nombreTableauxExamines = 0;

    public void TableauExamine()
    {
        nombreTableauxExamines++;
        AfficherProgression();

        if (nombreTableauxExamines >= nombreTotalTableaux)
        {
            // Activer la porte de sortie
            if (sortie != null)
                sortie.ActiverSortie();

            // Afficher le canvas visite complète directement
            if (gameManager != null)
                gameManager.VisiteComplete();
        }
    }

    private void AfficherProgression()
    {
        if (texteProgression != null)
        {
            texteProgression.text = nombreTableauxExamines + "/"
                + nombreTotalTableaux + " villes visitées";
        }
    }
}