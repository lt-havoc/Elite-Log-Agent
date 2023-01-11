using System.Collections.Generic;
using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Statistics : JournalEvent
{
    [JsonProperty("Bank_Account")]
    public required Dictionary<string, long> BankAccount { get; set; }

    [JsonProperty("Combat")]
    public required CombatStatistics Combat { get; set; }

    [JsonProperty("Crime")]
    public required CrimeStatistics Crime { get; set; }

    [JsonProperty("Smuggling")]
    public required SmugglingStatistics Smuggling { get; set; }

    [JsonProperty("Trading")]
    public required TradingStatistics Trading { get; set; }

    [JsonProperty("Mining")]
    public required MiningStatistics Mining { get; set; }

    [JsonProperty("Exploration")]
    public required ExplorationStatictics Exploration { get; set; }

    [JsonProperty("Passengers")]
    public required PassengersStatistics Passengers { get; set; }

    [JsonProperty("Search_And_Rescue")]
    public required SearchAndRescueStatistics SearchAndRescue { get; set; }

    [JsonProperty("TG_ENCOUNTERS")]
    public ThargoidEncountersStatistics? TgEncounters { get; set; }

    [JsonProperty("Crafting")]
    public required CraftingStatistics Crafting { get; set; }

    [JsonProperty("Crew")]
    public required CrewStatistics Crew { get; set; }

    [JsonProperty("Multicrew")]
    public required MulticrewStatistics Multicrew { get; set; }

    [JsonProperty("Material_Trader_Stats")]
    public MaterialTraderStatistics? MaterialTraderStats { get; set; }

    [JsonProperty("CQC")]
    public Dictionary<string, long>? Cqc { get; set; }
}