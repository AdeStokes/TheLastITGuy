using UnityEngine;
using TMPro;

public class TicketRowUI : MonoBehaviour
{
    public TextMeshProUGUI ticketNumberText;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI computerNameText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI descriptionText;

    public void Setup(Ticket ticket)
    {
        ticketNumberText.text = $"#{ticket.ticketNumber:000}";
        userNameText.text = ticket.userName;
        computerNameText.text = ticket.computerName;
        statusText.text = ticket.status.ToString();
        descriptionText.text = ticket.userDescription;
    }
}