using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI closeHintText;

    public PlayerMovement playerMovement;

    private bool isOpen = false;
    private bool openingKeyReleased = false;
    private bool closeKeyPressed = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!isOpen || Keyboard.current == null)
            return;

        // Escape can always close the dialogue immediately
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseDialogue();
            return;
        }

        // First wait for the E that opened the dialogue to be released
        if (!openingKeyReleased)
        {
            if (Keyboard.current.eKey.wasReleasedThisFrame)
            {
                openingKeyReleased = true;
            }

            return;
        }

        // Detect the NEXT E press
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            closeKeyPressed = true;
        }

        // Close when that second E press is released
        if (closeKeyPressed &&
            Keyboard.current.eKey.wasReleasedThisFrame)
        {
            CloseDialogue();
        }
    }

    public void OpenDialogue(NPCInteractable npc)
    {
        nameText.text = npc.GetNPCName();
        dialogueText.text = npc.GetDialogue();
        closeHintText.text = "[E] Continue";

        dialoguePanel.SetActive(true);

        isOpen = true;
        openingKeyReleased = false;
        closeKeyPressed = false;

        playerMovement.controlsEnabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameStateManager.Instance.SetState(GameState.Dialogue);
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);

        isOpen = false;
        openingKeyReleased = false;
        closeKeyPressed = false;

        playerMovement.controlsEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameStateManager.Instance.SetState(GameState.Playing);
    }
}