using UnityEngine;
using UnityEngine.Serialization;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [Header("NPC Data")]
    public NPCData npcData;

    [Header("Ticket")]
    [FormerlySerializedAs("computer")]
    public MonoBehaviour ticketSource;

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

        ITicketSource source = ticketSource as ITicketSource;

        if (source == null)
            return "Everything seems to be working.";

        Ticket ticket = source.GetTicket();

        if (ticket == null)
            return "Everything seems to be working.";

        switch (ticket.status)
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