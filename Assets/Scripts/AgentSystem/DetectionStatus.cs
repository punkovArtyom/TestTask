namespace AgentSystem
{
    /// <summary>
    /// DetectionStatus enum used in AgentsManager
    /// </summary>
    [System.Flags]
    public enum DetectionStatus
    {
        InCloseDistance = 1,
        InFOV = 2,
        Visible = 4
    }
}