using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public Camera playerCamera;
    public float interactionDistance = 3f;

    [Header("UI")]
    public TextMeshProUGUI interactionPrompt;

    private IInteractable currentInteractable;

    void Start()
    {
        interactionPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GameStateManager.Instance.IsPlaying())
            return;

        CheckForInteraction();

        if (currentInteractable != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteraction()
    {
        currentInteractable = null;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                interactionPrompt.text =
                    interactable.GetInteractionText();

                interactionPrompt.gameObject.SetActive(true);

                return;
            }
        }

        interactionPrompt.gameObject.SetActive(false);
    }
}