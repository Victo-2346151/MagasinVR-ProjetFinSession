using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

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

    // Variable statique partagée entre tous les tableaux
    // (solution suggérée par Claude AI)
    private static AudioSource audioEnCours;
    private AudioSource musiqueAmbiance;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
        }
    }

    void Start()
    {
        if (texteTitre != null)
            texteTitre.text = titreVille;

        if (texteArtiste != null)
            texteArtiste.text = pays;

        // Trouver le popup dans la scène même s'il est désactivé
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

        if (canvasPopup != null)
            canvasPopup.SetActive(false);

        // Trouver la musique d'ambiance
        
        GameObject objMusique = GameObject.Find("MusiqueAmbiance");
        if (objMusique != null)
            musiqueAmbiance = objMusique.GetComponent<AudioSource>();
    }

    public void AfficherDescription()
    {
        VibrerControleur();
        ArreterAudio();

        if (canvasPopup != null)
            canvasPopup.SetActive(true);

        if (textePopup != null)
            textePopup.text = descriptionTexte;

        // Afficher le titre dans le popup
        
        Transform titreTrans = canvasPopup.transform.Find("TexteTitrePopup");
        if (titreTrans != null)
        {
            TextMeshProUGUI titrePop = titreTrans.GetComponent<TextMeshProUGUI>();
            if (titrePop != null)
                titrePop.text = titreVille;
        }

        ValiderVisite();
    }

    public void JouerAudio()
    {
        VibrerControleur();
        FermerPopup();
        BaisserMusique();

        // Arrêter l'audio du tableau précédent s'il joue
        if (audioEnCours != null && audioEnCours.isPlaying)
            audioEnCours.Stop();

        if (descriptionAudio != null)
        {
            audioSource.clip = descriptionAudio;
            audioSource.Play();
            audioEnCours = audioSource;
            Invoke("RemontrerMusique", descriptionAudio.length);
        }

        ValiderVisite();
    }

    public void FermerPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);
    }

    private void ValiderVisite()
    {
        if (!dejaExamine)
        {
            dejaExamine = true;
            // FindFirstObjectByType car tableaux placés manuellement
          
            MuseeManager museeManager = FindFirstObjectByType<MuseeManager>();
            if (museeManager != null)
                museeManager.TableauExamine();
        }
    }

    private void VibrerControleur()
    {
        // Vibration au clic
        XRBaseController[] controleurs = FindObjectsByType<XRBaseController>(FindObjectsSortMode.None);
        foreach (XRBaseController controleur in controleurs)
        {
            controleur.SendHapticImpulse(0.3f, 0.1f);
        }
    }

    private void BaisserMusique()
    {
        if (musiqueAmbiance != null)
            musiqueAmbiance.volume = 0.05f;
    }

    private void RemontrerMusique()
    {
        if (musiqueAmbiance != null)
            musiqueAmbiance.volume = 0.3f;
    }

    private void ArreterAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}