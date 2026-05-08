using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPopup;

    public void FermerPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);
    }
}