# The Last IT Guy - Project State

## Current Features

- First-person WASD movement
- Mouse look
- Crosshair
- Generic IInteractable system
- Computer interaction
- Diagnostic UI
- Ticket system
- Fault types
- Ticket states:
  - Open
  - Diagnosed
  - Resolved
- Reputation
- IT budget
- Helpdesk ticket queue
- GameStateManager
- NPC interaction
- Dialogue UI
- Data-driven NPCs using NPCData ScriptableObjects
- NPC dialogue changes based on ticket state
- NPCs use ITicketSource rather than depending directly on computers
- Multiple independent NPC/ticket chains supported

## Current NPCs

### Dave

Computer: PC-014

Ticket:
"The internet isn't working."

Fault:
NetworkCableDisconnected

Dialogue changes based on ticket state.

### Sarah

Computer: PC-015

Ticket:
"My computer won't turn on."

Fault:
PowerCableDisconnected

Rewards:
- Reputation: 15
- Budget: 30

Dialogue changes based on ticket state.

## Architecture Notes

- NPC identity and dialogue are stored in NPCData ScriptableObjects.
- NPCInteractable references an ITicketSource.
- ComputerInteractable implements ITicketSource.
- This allows NPC dialogue to react to tickets without NPCs depending directly on ComputerInteractable.
- Dave and Sarah have separate tickets and progress independently through Open, Diagnosed and Resolved states.

## Controls

WASD - Movement
Mouse - Look
E - Interact
Tab - Helpdesk
Esc - Close dialogue

## Next Planned Work

- Improve Helpdesk
- Improve office visuals
- Add additional NPCs and fault scenarios
- Review ticket/diagnostic workflow for scalability
