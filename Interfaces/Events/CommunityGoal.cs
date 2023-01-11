using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class CommunityGoal : JournalEvent
{
    [JsonProperty("CurrentGoals")]
    public required CommunityGoalRecord[] CurrentGoals { get; set; }
}