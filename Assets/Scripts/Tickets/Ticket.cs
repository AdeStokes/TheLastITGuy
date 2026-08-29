using UnityEngine;

[System.Serializable]
public class Ticket
{
    public int ticketNumber;
    public string userName;
    public string computerName;
    [TextArea]
    public string userDescription;
    public FaultType fault;
    public int reputationReward = 10;
    public int budgetReward = 25;
    public TicketStatus status = TicketStatus.Open;
}

public enum FaultType
{
    NetworkCableDisconnected,
    PowerCableDisconnected,
    ComputerNeedsRestart
}

public enum TicketStatus
{
    Open,
    Diagnosed,
    Resolved
}