using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HelpdeskUI : MonoBehaviour
{
    public GameObject helpdeskPanel;
    public TextMeshProUGUI ticketListText;
    public PlayerMovement playerMovement;
    public Transform ticketListContainer;
    public TicketRowUI ticketRowPrefab;
    private Ticket selectedTicket;
    public TextMeshProUGUI emptyStateText;
    public TextMeshProUGUI detailsTicketNumberText;
    public TextMeshProUGUI detailsUserNameText;
    public TextMeshProUGUI detailsComputerNameText;
    public TextMeshProUGUI detailsDescriptionText;
    public TextMeshProUGUI detailsStatusText;
    public GameObject ticketDetailsContent;

    void Start()
    {
        helpdeskPanel.SetActive(false);
        ticketDetailsContent.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (GameStateManager.Instance.CurrentState == GameState.Playing)
            {
                OpenHelpdesk();
            }
            else if (GameStateManager.Instance.CurrentState == GameState.Helpdesk)
            {
                CloseHelpdesk();
            }
        }
    }

    void OpenHelpdesk()
    {
        helpdeskPanel.SetActive(true);
        RefreshTickets();

        playerMovement.controlsEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameStateManager.Instance.SetState(GameState.Helpdesk);
    }

    void CloseHelpdesk()
    {

        helpdeskPanel.SetActive(false);

        playerMovement.controlsEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameStateManager.Instance.SetState(GameState.Playing);
    }

    void RefreshTickets()
    {
        foreach (Transform child in ticketListContainer)
        {
            Destroy(child.gameObject);
        }

        var tickets = GameManager.Instance.GetActiveTickets();

        foreach (Ticket ticket in tickets)
        {
            TicketRowUI row = Instantiate(ticketRowPrefab, ticketListContainer);
            row.Setup(ticket, this);
            row.SetSelected(ticket == selectedTicket);
        }
    }

    public void SelectTicket(Ticket ticket)
    {
        selectedTicket = ticket;
        emptyStateText.gameObject.SetActive(false);
        ticketDetailsContent.SetActive(true);

        detailsTicketNumberText.text = $"TICKET #{ticket.ticketNumber:000}";
        detailsUserNameText.text = ticket.userName;
        detailsComputerNameText.text = ticket.computerName;
        detailsDescriptionText.text = ticket.userDescription;
        detailsStatusText.text = ticket.status.ToString().ToUpper();

        foreach (Transform child in ticketListContainer)
        {
            TicketRowUI row = child.GetComponent<TicketRowUI>();

            if (row != null)
            {
                row.SetSelected(row.GetTicket() == selectedTicket);
            }
        }
    }
}