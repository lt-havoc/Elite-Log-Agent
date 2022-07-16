﻿using System.Collections.Generic;
using DW.ELA.Interfaces;
using Newtonsoft.Json.Linq;

namespace DW.ELA.Plugin.EDSM
{
    public class EdsmEventConverter : IEventConverter<JObject>
    {
        private readonly IPlayerStateHistoryRecorder playerStateRecorder;

        public EdsmEventConverter(IPlayerStateHistoryRecorder playerStateRecorder)
        {
            this.playerStateRecorder = playerStateRecorder ?? throw new System.ArgumentNullException(nameof(playerStateRecorder));
        }

        public IEnumerable<JObject> Convert(JournalEvent sourceEvent)
        {
            var @event = (JObject)sourceEvent.Raw.DeepClone();
            var timestamp = sourceEvent.Timestamp;
            string starSystem = @event["StarSystem"]?.ToObject<string>() ?? playerStateRecorder.GetPlayerSystem(timestamp);

            @event["_systemName"] = starSystem;
            @event["_shipId"] = playerStateRecorder.GetPlayerShipId(timestamp);
            yield return @event;
        }
    }
}
