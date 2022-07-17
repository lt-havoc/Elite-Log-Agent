using System;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class CommunityGoalRecord
{
    [JsonProperty("CGID")]
    public long Cgid { get; set; }

    [JsonProperty("Title")]
    public required string Title { get; set; }

    [JsonProperty("SystemName")]
    public required string SystemName { get; set; }

    [JsonProperty("MarketName")]
    public required string MarketName { get; set; }

    [JsonProperty("Expiry")]
    public DateTime Expiry { get; set; }

    [JsonProperty("IsComplete")]
    public bool IsComplete { get; set; }

    [JsonProperty("CurrentTotal")]
    public long CurrentTotal { get; set; }

    [JsonProperty("PlayerContribution")]
    public long PlayerContribution { get; set; }

    [JsonProperty("NumContributors")]
    public long NumContributors { get; set; }

    [JsonProperty("TopRankSize")]
    public long TopRankSize { get; set; }

    [JsonProperty("PlayerInTopRank")]
    public bool PlayerInTopRank { get; set; }

    [JsonProperty("TierReached")]
    public string? TierReached { get; set; }

    [JsonProperty("PlayerPercentileBand")]
    public long PlayerPercentileBand { get; set; }

    [JsonProperty("Bonus")]
    public long Bonus { get; set; }

    [JsonProperty("TopTier")]
    public required TopTier TopTier { get; set; }
}