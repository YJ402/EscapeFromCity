using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        HidePrompt();
    }
    public void ShowPrompt(IInteractable interactableItem)
    {
        textMeshProUGUI.text = interactableItem.GetPromptInfo().name + "\n" + interactableItem.GetPromptInfo().desc;
        textMeshProUGUI.enabled = true;
    }

    public void HidePrompt()
    {
        textMeshProUGUI.enabled = false;
    }
}
