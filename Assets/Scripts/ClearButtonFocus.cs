using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearButtonFocus : MonoBehaviour
{
    public Button yourButton;

    private void Start()
    {
        yourButton.onClick.AddListener(ClearFocus);
    }

    private void ClearFocus()
    {
        // Remove focus from the button
        EventSystem.current.SetSelectedGameObject(null);
    }
}