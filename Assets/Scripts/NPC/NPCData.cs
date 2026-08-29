using UnityEngine;

[CreateAssetMenu(
    fileName = "NewNPC",
    menuName = "The Last IT Guy/NPC Data"
)]
public class NPCData : ScriptableObject
{
    [Header("Identity")]
    public string npcName;

    [Header("Dialogue")]
    [TextArea(3, 8)]
    public string openDialogue;

    [TextArea(3, 8)]
    public string diagnosedDialogue;

    [TextArea(3, 8)]
    public string resolvedDialogue;
}