using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

// Gère chaque tableau du musée :
// - Affiche les informations de la ville (titre, pays)
// - Gère le popup de description et la lecture audio
// - Valide la visite du tableau quand le joueur interagit
public class TableauScript : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private string titreVille = "Ville";
    [SerializeField] private string pays = "Maroc";
    [SerializeField, TextArea] private string descriptionTexte = "Description...";

    [Header("Audio")]
    [SerializeField] private AudioClip descriptionAudio;

    [Header("UI Panneau tableau")]
    [SerializeField] private TextMeshProUGUI texteTitre;
    [SerializeField] private TextMeshProUGUI texteArtiste;

    private GameObject canvasPopup;
    private TextMeshProUGUI textePopup;
    private AudioSource audioSource;
    private bool dejaExamine = false;

    // Variable statique partagée entre tous les tableaux pour éviter plusieurs audios simultanés
    // (solution suggérée par Claude AI)
    private static AudioSource audioEnCours;

    // Référence à la musique d'ambiance pour baisser le volume pendant la voix off
    private AudioSource musiqueAmbiance;

    // Initialise l'AudioSource du tableau
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
        }
    }

    // Initialise les références et trouve le popup dans la scène
    void Start()
    {
        // Mettre à jour les textes du panneau avec les infos de la ville
        if (texteTitre != null)
            texteTitre.text = titreVille;

        if (texteArtiste != null)
            texteArtiste.text = pays;

        // Trouver le popup dans la scène même s'il est désactivé
        // Resources.FindObjectsOfTypeAll permet de trouver les objets inactifs
        // (solution suggérée par Claude AI)
        Canvas[] tousLesCanvas = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canvas in tousLesCanvas)
        {
            if (canvas.name == "CanvasPopup")
            {
                canvasPopup = canvas.gameObject;
                Transform textePopupTrans = canvasPopup.transform.Find("TextePopup");
                if (textePopupTrans != null)
                    textePopup = textePopupTrans.GetComponent<TextMeshProUGUI>();
                break;
            }
        }

        // S'assurer que le popup est désactivé au démarrage
        if (canvasPopup != null)
            canvasPopup.SetActive(false);

        // Trouver la musique d'ambiance par son nom dans la scène
        // (solution suggérée par Claude AI)
        GameObject objMusique = GameObject.Find("MusiqueAmbiance");
        if (objMusique != null)
            musiqueAmbiance = objMusique.GetComponent<AudioSource>();
    }

    // Affiche le popup de description quand le joueur clique sur le bouton Texte
    public void AfficherDescription()
    {
        VibrerControleur();
        ArreterAudio();

        // Activer le popup et afficher la description
        if (canvasPopup != null)
            canvasPopup.SetActive(true);

        if (textePopup != null)
            textePopup.text = descriptionTexte;

        // Afficher le titre de la ville dans le popup
        // Accès par nom pour éviter de référencer un objet désactivé
        // (solution suggérée par Claude AI)
        Transform titreTrans = canvasPopup.transform.Find("TexteTitrePopup");
        if (titreTrans != null)
        {
            TextMeshProUGUI titrePop = titreTrans.GetComponent<TextMeshProUGUI>();
            if (titrePop != null)
                titrePop.text = titreVille;
        }

        ValiderVisite();
    }

    // Joue la description audio quand le joueur clique sur le bouton Audio
    public void JouerAudio()
    {
        VibrerControleur();
        FermerPopup();
        BaisserMusique();

        // Arrêter l'audio du tableau précédent s'il joue encore
        // (solution suggérée par Claude AI)
        if (audioEnCours != null && audioEnCours.isPlaying)
            audioEnCours.Stop();

        if (descriptionAudio != null)
        {
            audioSource.clip = descriptionAudio;
            audioSource.Play();
            audioEnCours = audioSource;

            // Remonter la musique après la fin de la voix off
            // (solution suggérée par Claude AI)
            Invoke("RemontrerMusique", descriptionAudio.length);
        }

        ValiderVisite();
    }

    // Ferme le popup de description
    public void FermerPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);
    }

    // Marque le tableau comme examiné et notifie le MuseeManager
    // Ne s'exécute qu'une seule fois par tableau
    private void ValiderVisite()
    {
        if (!dejaExamine)
        {
            dejaExamine = true;

            // FindFirstObjectByType car les tableaux sont créés manuellement
            // (solution suggérée par Claude AI)
            MuseeManager museeManager = FindFirstObjectByType<MuseeManager>();
            if (museeManager != null)
                museeManager.TableauExamine();
        }
    }

    // Envoie une vibration aux contrôleurs au moment du clic
    // Pattern du cours exercice 4.1
    private void VibrerControleur()
    {
        XRBaseController[] controleurs = FindObjectsByType<XRBaseController>(FindObjectsSortMode.None);
        foreach (XRBaseController controleur in controleurs)
        {
            controleur.SendHapticImpulse(0.3f, 0.1f);
        }
    }

    // Baisse le volume de la musique d'ambiance pendant la voix off
    private void BaisserMusique()
    {
        if (musiqueAmbiance != null)
            musiqueAmbiance.volume = 0.05f;
    }

    // Remet le volume de la musique d'ambiance à la normale
    private void RemontrerMusique()
    {
        if (musiqueAmbiance != null)
            musiqueAmbiance.volume = 0.3f;
    }

    // Arrête l'audio en cours sur ce tableau
    private void ArreterAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}