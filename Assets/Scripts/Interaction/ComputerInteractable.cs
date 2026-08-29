using UnityEngine;

public class ComputerInteractable : MonoBehaviour, IInteractable, ITicketSource
{
    public string computerName = "PC-014";
    public string userName = "Dave";

    public DiagnosticUI diagnosticUI;

    [Header("Current Ticket")]
    public Ticket currentTicket;

    public string GetInteractionText()
    {
        if (currentTicket != null &&
            currentTicket.status != TicketStatus.Resolved)
        {
            return $"[E] Inspect {computerName}";
        }

        return $"[E] Use {computerName}";
    }

    public void Interact()
    {
        diagnosticUI.OpenComputer(this);
    }

    public Ticket GetTicket()
    {
        return currentTicket;
    }

    void Start()
    {
        if (currentTicket != null)
        {
            GameManager.Instance.RegisterTicket(currentTicket);
        }
    }
}