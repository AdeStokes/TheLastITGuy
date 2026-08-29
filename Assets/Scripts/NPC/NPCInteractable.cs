using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public string npcName = "Dave";

    [Header("Ticket")]
    public ComputerInteractable computer;

    [Header("Dialogue")]
    [TextArea]
    public string openDialogue =
        "Finally! I've been waiting ages.\n\n" +
        "The internet's broken.\n" +
        "I haven't touched anything.";

    [TextArea]
    public string diagnosedDialogue =
        "Did you figure out what's wrong?";

    [TextArea]
    public string resolvedDialogue =
        "The cable was unplugged?\n\n" +
        "That's weird. I definitely didn't touch it.";

    public DialogueUI dialogueUI;

    public string GetInteractionText()
    {
        return $"[E] Talk to {npcName}";
    }

    public void Interact()
    {
        dialogueUI.OpenDialogue(this);
    }

    public string GetDialogue()
    {
        if (computer == null || computer.currentTicket == null)
            return "Everything seems to be working.";

        switch (computer.currentTicket.status)
        {
            case TicketStatus.Open:
                return openDialogue;

            case TicketStatus.Diagnosed:
                return diagnosedDialogue;

            case TicketStatus.Resolved:
                return resolvedDialogue;

            default:
                return "...";
        }
    }
}