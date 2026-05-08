using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]
public class TableauScript : MonoBehaviour
{
    [Header("Informations")]
    [SerializeField] private string titreVille = "Ville";
    [SerializeField] private string pays = "Maroc";
    [SerializeField, TextArea] private string descriptionTexte = "Description...";

    [Header("Audio")]
    [SerializeField] private AudioClip descriptionAudio;

    [Header("UI Panneau tableau")]
    [SerializeField] private GameObject panneauInfo;
    [SerializeField] private TextMeshProUGUI texteTitre;
    [SerializeField] private TextMeshProUGUI texteArtiste;

    [Header("UI Popup caméra")]
    // FindFirstObjectByType car le popup est sur la caméra, pas sur le tableau
    // (solution suggérée par Claude AI)
    private GameObject canvasPopup;
    private TextMeshProUGUI textePopup;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private bool dejaExamine = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
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

        // Cherche dans tous les objets incluant inactifs
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

        // Désactiver le popup au démarrage
        if (canvasPopup != null)
        {
            canvasPopup.SetActive(false);
            Debug.Log("CanvasPopup trouvé et désactivé !");
        }
        else
        {
            Debug.Log("CanvasPopup NON trouvé !");
        }
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelache);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelache);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Vibration au grab — pattern cours exercice 4.1
        XRBaseController controleur = args.interactorObject
            .transform.GetComponent<XRBaseController>();
        if (controleur != null)
            controleur.SendHapticImpulse(0.3f, 0.1f);

        if (panneauInfo != null)
            panneauInfo.SetActive(true);

        if (!dejaExamine)
        {
            dejaExamine = true;
            // FindFirstObjectByType car tableaux placés manuellement
            // (solution suggérée par Claude AI)
            MuseeManager museeManager = FindFirstObjectByType<MuseeManager>();
            if (museeManager != null)
                museeManager.TableauExamine();
        }
    }

    private void OnRelache(SelectExitEventArgs args)
    {
        if (panneauInfo != null)
            panneauInfo.SetActive(false);

        FermerPopup();
        ArreterAudio();
    }

    // Appelé par le bouton Texte
    public void AfficherDescription()
    {
        ArreterAudio();
        if (canvasPopup != null)
            canvasPopup.SetActive(true);
        else
        {
            Debug.Log("canvasPopup est NULL !");
            return;
        }

        if (textePopup != null)
        {
            textePopup.text = descriptionTexte;
            Debug.Log("textePopup trouvé : " + textePopup.name);
        }
        else
            Debug.Log("textePopup est NULL !");

        Transform titreTrans = canvasPopup.transform.Find("TexteTitrePopup");
        if (titreTrans != null)
        {
            TextMeshProUGUI titrePop = titreTrans.GetComponent<TextMeshProUGUI>();
            if (titrePop != null)
            {
                titrePop.text = titreVille;
                Debug.Log("Titre popup : " + titreVille);
            }
            else
                Debug.Log("TexteTitrePopup - composant TMP introuvable !");
        }
        else
            Debug.Log("TexteTitrePopup - objet introuvable !");
    }

    // Appelé par le bouton Audio
    public void JouerAudio()
    {
        FermerPopup();
        if (descriptionAudio != null)
        {
            audioSource.clip = descriptionAudio;
            audioSource.Play();
        }
    }

    // Appelé par le bouton Fermer du popup
    public void FermerPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);
    }

    private void ArreterAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}