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

    void Start()
    {
        helpdeskPanel.SetActive(false);
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
            row.Setup(ticket);
        }
    }
}