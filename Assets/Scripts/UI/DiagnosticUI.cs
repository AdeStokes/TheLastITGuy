using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiagnosticUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject diagnosticPanel;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI userText;
    public TextMeshProUGUI problemText;
    public TextMeshProUGUI resultText;
    public Button resolveButton;
    public TextMeshProUGUI resolveButtonText;
    [Header("Player")]
    public PlayerMovement playerMovement;
    private ComputerInteractable currentComputer;
    private Ticket currentTicket;
    private bool faultIdentified = false;

    void Start()
    {
        diagnosticPanel.SetActive(false);
    }

    public void OpenComputer(ComputerInteractable computer)
    {
        currentComputer = computer;
        currentTicket = computer.currentTicket;

        faultIdentified = false;

        titleText.text = computer.computerName;
        userText.text = "User: " + computer.userName;

        if (currentTicket != null)
        {
            problemText.text =
                $"Ticket #{currentTicket.ticketNumber}\n" +
                $"Reported Issue: {currentTicket.userDescription}";
        }
        else
        {
            problemText.text = "No active support ticket.";
        }

        resultText.text = "";

        resolveButton.interactable = false;
        resolveButtonText.text = "Run diagnostics first";

        diagnosticPanel.SetActive(true);

        playerMovement.controlsEnabled = false;
        GameStateManager.Instance.SetState(GameState.Diagnostic);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CheckNetwork()
    {
        if (currentTicket == null)
            return;

        if (currentTicket.fault == FaultType.NetworkCableDisconnected)
        {
            resultText.text =
                "Network Status: DISCONNECTED\n\n" +
                "Ethernet cable appears to be disconnected.";

            IdentifyFault("Reconnect Ethernet Cable");
        }
        else
        {
            resultText.text =
                "Network Status: CONNECTED\n\n" +
                "No network hardware problems detected.";
        }
    }

    public void RestartPC()
    {
        if (currentTicket == null)
            return;

        if (currentTicket.fault == FaultType.ComputerNeedsRestart)
        {
            resultText.text =
                "Restart completed.\n\n" +
                "System is now operating normally.";

            IdentifyFault("Complete Restart");
        }
        else
        {
            resultText.text =
                "Restart completed.\n\n" +
                "Reported problem is still present.";
        }
    }

    public void CheckPower()
    {
        if (currentTicket == null)
            return;

        if (currentTicket.fault == FaultType.PowerCableDisconnected)
        {
            resultText.text =
                "POWER FAILURE\n\n" +
                "No power detected at workstation.";

            IdentifyFault("Reconnect Power Cable");
        }
        else
        {
            resultText.text =
                "Power supply operating normally.";
        }
    }

    void IdentifyFault(string repairText)
    {
        faultIdentified = true;
        currentTicket.status = TicketStatus.Diagnosed;

        resolveButton.interactable = true;
        resolveButtonText.text = repairText;
    }

    public void AttemptRepair()
    {
        if (currentTicket == null || !faultIdentified)
            return;

        switch (currentTicket.fault)
        {
            case FaultType.NetworkCableDisconnected:
                resultText.text =
                    "Ethernet cable reconnected.\n\n" +
                    "Network connectivity restored.\n\n" +
                    "TICKET RESOLVED!";
                break;

            case FaultType.PowerCableDisconnected:
                resultText.text =
                    "Power cable reconnected.\n\n" +
                    "Workstation powered on successfully.\n\n" +
                    "TICKET RESOLVED!";
                break;

            case FaultType.ComputerNeedsRestart:
                resultText.text =
                    "System restarted successfully.\n\n" +
                    "TICKET RESOLVED!";
                break;
        }

        resolveButton.interactable = false;
        resolveButtonText.text = "Resolved";

        currentTicket.status = TicketStatus.Resolved;
        GameManager.Instance.ResolveTicket(currentTicket);

    }

    public void Close()
    {
        diagnosticPanel.SetActive(false);

        playerMovement.controlsEnabled = true;
        GameStateManager.Instance.SetState(GameState.Playing);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentComputer = null;
        currentTicket = null;
        faultIdentified = false;
    }
}