using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [Header("NPC Data")]
    public NPCData npcData;

    [Header("Ticket")]
    public ComputerInteractable computer;

    [Header("UI")]
    public DialogueUI dialogueUI;

    public string GetInteractionText()
    {
        if (npcData == null)
            return "[E] Talk";

        return $"[E] Talk to {npcData.npcName}";
    }

    public void Interact()
    {
        dialogueUI.OpenDialogue(this);
    }

    public string GetNPCName()
    {
        if (npcData == null)
            return "Unknown";

        return npcData.npcName;
    }

    public string GetDialogue()
    {
        if (npcData == null)
            return "NPC data has not been configured.";

        if (computer == null || computer.currentTicket == null)
            return "Everything seems to be working.";

        switch (computer.currentTicket.status)
        {
            case TicketStatus.Open:
                return npcData.openDialogue;

            case TicketStatus.Diagnosed:
                return npcData.diagnosedDialogue;

            case TicketStatus.Resolved:
                return npcData.resolvedDialogue;

            default:
                return "...";
        }
    }
}