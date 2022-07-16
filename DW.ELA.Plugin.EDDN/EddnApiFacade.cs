﻿using System;
using System.Threading.Tasks;
using DW.ELA.Interfaces;
using DW.ELA.Plugin.EDDN.Model;
using DW.ELA.Utility.Json;
using NLog;
using NLog.Fluent;

namespace DW.ELA.Plugin.EDDN
{
    public class EddnApiFacade : IEddnApiFacade
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        private readonly IRestClient restClient;

        public EddnApiFacade(IRestClient restClient)
        {
            this.restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task PostEventAsync(EddnEvent @event)
        {
            string eventData = Serialize.ToJson(@event);
            try
            {
                string result = await restClient.PostAsync(eventData);
                if (result != "OK")
                {
                    Log.Error()
                        .Message(result)
                        .Property("input", eventData)
                        .Write();
                }
                else
                {
                    Log.Info("Pushed event {0}", @event.GetType());
                }
            }
            catch (Exception e)
            {
                Log.Error()
                    .Message("Error pushing event")
                    .Exception(e)
                    .Property("input", eventData)
                    .Write();
            }
        }
    }
}
