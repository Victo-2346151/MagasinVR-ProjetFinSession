using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    void Start()
    {
        ChangerEtat(EtatJeu.Menu);
    }

    public void ChangerEtat(EtatJeu nouvelEtat)
    {
        etatActuel = nouvelEtat;
        canvasMenu.SetActive(etatActuel == EtatJeu.Menu);
        canvasHUD.SetActive(etatActuel == EtatJeu.EnJeu);
        canvasVisiteComplete.SetActive(etatActuel == EtatJeu.VisiteComplete);
    }

    public void CommencerVisite()
    {
        ChangerEtat(EtatJeu.EnJeu);
    }

    public void VisiteComplete()
    {
        ChangerEtat(EtatJeu.VisiteComplete);
    }

    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quitter()
    {
        Application.Quit();
    }
}