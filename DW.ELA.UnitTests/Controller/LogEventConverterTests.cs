﻿using System;
using System.Collections.Generic;
using System.Linq;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Events;
using DW.ELA.LogModel;
using DW.ELA.UnitTests.Utility;
using DW.ELA.Utility.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DW.ELA.UnitTests.Controller
{
    [TestFixture]
    [Parallelizable]
    public class LogEventConverterTests
    {
        private static IEnumerable<TestCaseData> RawTestCases => TestEventSource.CannedEventsRaw
            .Select(jo => new TestCaseData(jo).SetArgDisplayNames(jo.Property("event").Value.ToString()));

        [Test]
        public void ShouldConvertFsdJumpEvent()
        {
            string eventString = @"{""timestamp"":""2018-06-25T18:10:30Z"", ""event"":""FSDJump"", ""StarSystem"":""Shinrarta Dezhra"", 
                ""SystemAddress"":3932277478106, ""StarPos"":[55.71875, 17.59375, 27.15625 ], ""SystemAllegiance"":""PilotsFederation"", 
                ""SystemEconomy"":""$economy_HighTech;"", ""SystemEconomy_Localised"":""High Tech"", ""SystemSecondEconomy"":""$economy_Industrial;"", 
                ""SystemSecondEconomy_Localised"":""Industrial"", ""SystemGovernment"":""$government_Democracy;"", ""SystemGovernment_Localised"":""Democracy"", 
                ""SystemSecurity"":""$SYSTEM_SECURITY_high;"", ""SystemSecurity_Localised"":""High Security"", ""Population"":85206935, ""JumpDist"":11.896, 
                ""FuelUsed"":2.983697, ""FuelLevel"":12.767566, ""Factions"":[{""Name"":""Lori Jameson"", ""FactionState"":""None"", ""Government"":""Engineer"", 
                ""Influence"":0.000000, ""Allegiance"":""Independent""} ] }";

            var @event = (FsdJump)JournalEventConverter.Convert(JObject.Parse(eventString));
            Assert.AreEqual(new DateTime(2018, 06, 25, 18, 10, 30, DateTimeKind.Utc), @event.Timestamp);
        }

        [Test]
        [Parallelizable]
        [TestCaseSource(typeof(LogEventConverterTests), nameof(RawTestCases))]
        public void EventsTransformationShouldNotSpoilData(JObject source)
        {
            var @event = JournalEventConverter.Convert(source);

            if (@event.GetType() == typeof(JournalEvent))
                Assert.Inconclusive("Event is not typed");

            var serialized = JObject.FromObject(@event, Converter.Serializer);

            if (@event is Scan)
                source.Remove("Parents"); // TODO: find a way to serialize that structure

            var diffs = JsonComparer.Compare(@event.Event, source, serialized);
            Assert.IsEmpty(diffs);
        }

        [Test]
        [Explicit("Heavy test")]
        public void ShouldConvertLocalEvents()
        {
            var events = TestEventSource.LocalEventsRaw.ToList();
            events.ForEach(x => JournalEventConverter.Convert(x));
        }
    }
}
