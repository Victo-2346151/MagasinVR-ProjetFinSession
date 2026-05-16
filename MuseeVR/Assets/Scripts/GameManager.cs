using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Gère la boucle de jeu complète : Menu -  EnJeu - VisiteComplete

public class GameManager : MonoBehaviour
{
    public enum EtatJeu { Menu, EnJeu, VisiteComplete }

    [Header("Canvas")]
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasVisiteComplete;

    [Header("Références")]
    [SerializeField] private MuseeManager museeManager;

    private EtatJeu etatActuel;

    // Démarre sur le menu au lancement
    void Start()
    {
        ChangerEtat(EtatJeu.Menu);
    }

    // Active/désactive les Canvas selon l'état du jeu
   
    public void ChangerEtat(EtatJeu nouvelEtat)
    {
        etatActuel = nouvelEtat;
        canvasMenu.SetActive(etatActuel == EtatJeu.Menu);
        canvasHUD.SetActive(etatActuel == EtatJeu.EnJeu);
        canvasVisiteComplete.SetActive(etatActuel == EtatJeu.VisiteComplete);
    }

    // Appelée par le bouton Commencer la visite
    public void CommencerVisite()
    {
        ChangerEtat(EtatJeu.EnJeu);
    }

    // Appelée par MuseeManager quand tous les tableaux sont examinés
    // Délai de 2 secondes avant l'affichage du canvas de fin
    public void VisiteComplete()
    {
        // Invoke pour délai avant affichage du canvas de fin
        // (solution suggérée par Claude AI)
        Invoke("AfficherCanvasFin", 2f);
    }

    // Affiche le canvas de fin après le délai
    private void AfficherCanvasFin()
    {
        ChangerEtat(EtatJeu.VisiteComplete);
    }

    // Recharge la scène pour recommencer la visite
    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quitte l'application
    public void Quitter()
    {
        Application.Quit();
    }
}