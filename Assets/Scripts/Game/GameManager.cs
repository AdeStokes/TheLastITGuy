using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Player Stats")]
    public int reputation = 0;
    public int budget = 0;
    [Header("HUD")]
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI ticketsText;
    private List<Ticket> activeTickets = new List<Ticket>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateHUD();
    }

    public void RegisterTicket(Ticket ticket)
    {
        if (ticket == null)
            return;

        if (!activeTickets.Contains(ticket))
        {
            activeTickets.Add(ticket);
        }

        UpdateHUD();
    }

    public void ResolveTicket(Ticket ticket)
    {
        if (ticket == null)
            return;

        reputation += ticket.reputationReward;
        budget += ticket.budgetReward;

        activeTickets.Remove(ticket);

        UpdateHUD();
    }

    public IReadOnlyList<Ticket> GetActiveTickets()
    {
        return activeTickets;
    }
    
    public string GetTicketList()
    {
        if (activeTickets.Count == 0)
        {
            return "No open tickets.";
        }

        StringBuilder builder = new StringBuilder();

        foreach (Ticket ticket in activeTickets)
        {
            builder.AppendLine(
                $"#{ticket.ticketNumber:000}  {ticket.userName}"
            );

            builder.AppendLine(
                $"PC: {ticket.computerName}"
            );

            builder.AppendLine(
                $"\"{ticket.userDescription}\""
            );

            builder.AppendLine();
        }

        return builder.ToString();
    }

    void UpdateHUD()
    {
        reputationText.text = $"Reputation: {reputation}";
        budgetText.text = $"IT Budget: £{budget}";
        ticketsText.text = $"Open Tickets: {activeTickets.Count}";
    }
}