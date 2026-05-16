using UnityEngine;

// Gère la fermeture du popup de description attaché à la caméra
public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPopup;

    // Désactive le canvas popup quand le joueur clique sur Fermer
    public void FermerPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);
    }
}