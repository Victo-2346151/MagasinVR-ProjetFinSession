using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

[RequireComponent(typeof(XRGrabInteractable))]
public class TableauScript : MonoBehaviour
{
    [Header("Informations du tableau")]
    [SerializeField] private string titrePeinture = "Sans titre";
    [SerializeField] private string nomArtiste = "Artiste inconnu";
    [SerializeField, TextArea] private string descriptionTexte = "Description du tableau...";

    [Header("Audio")]
    [SerializeField] private AudioClip descriptionAudio;

    [Header("UI Références")]
    [SerializeField] private GameObject panneauInfo;
    [SerializeField] private GameObject panneauDescription;
    [SerializeField] private TextMeshProUGUI texteContenu;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private bool dejaExamine = false;
    private bool descriptionVisible = false;
    private bool audioEnCours = false;

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
        if (panneauInfo != null)
            panneauInfo.SetActive(true);

        if (!dejaExamine)
        {
            dejaExamine = true;
            // TEMPORAIRE - décommenter quand MuseeManager sera créé
            // MuseeManager museeManager = FindFirstObjectByType<MuseeManager>();
            // if (museeManager != null)
            //     museeManager.TableauExamine();
        }
    }

    private void OnRelache(SelectExitEventArgs args)
    {
        if (panneauInfo != null)
            panneauInfo.SetActive(false);

        CacherDescription();
        ArreterAudio();
    }

    // Appelé par le bouton Texte
    public void AfficherDescription()
    {
        ArreterAudio();
        descriptionVisible = true;

        if (panneauDescription != null)
            panneauDescription.SetActive(true);

        if (texteContenu != null)
            texteContenu.text = descriptionTexte;
    }

    // Appelé par le bouton Audio
    public void JouerAudio()
    {
        CacherDescription();

        if (descriptionAudio != null)
        {
            audioEnCours = true;
            audioSource.clip = descriptionAudio;
            audioSource.Play();
        }
    }

    // Appelé par le bouton Fermer
    public void CacherDescription()
    {
        descriptionVisible = false;
        if (panneauDescription != null)
            panneauDescription.SetActive(false);
    }

    private void ArreterAudio()
    {
        if (audioEnCours)
        {
            audioSource.Stop();
            audioEnCours = false;
        }
    }
}