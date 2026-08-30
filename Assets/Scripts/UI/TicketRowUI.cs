using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TicketRowUI : MonoBehaviour
{
    public TextMeshProUGUI ticketNumberText;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI computerNameText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI descriptionText;
    public Image statusBadge;

    public void Setup(Ticket ticket)
    {
        ticketNumberText.text = $"#{ticket.ticketNumber:000}";
        userNameText.text = ticket.userName;
        computerNameText.text = ticket.computerName;
        descriptionText.text = ticket.userDescription;

        UpdateStatus(ticket.status);
    }

    void UpdateStatus(TicketStatus status)
    {
        statusText.text = status.ToString().ToUpper();

        switch (status)
        {
            case TicketStatus.Open:
                statusBadge.color = new Color(0.25f, 0.65f, 0.30f);
                break;

            case TicketStatus.Diagnosed:
                statusBadge.color = new Color(0.95f, 0.65f, 0.15f);
                break;

            case TicketStatus.Resolved:
                statusBadge.color = new Color(0.45f, 0.45f, 0.45f);
                break;
        }

        statusText.color = Color.white;
    }
}