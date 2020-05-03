namespace DW.ELA.Controller.ViewModels
{
    using System;

    public class ApiKeyAddedEventArgs : EventArgs
    {
        public ApiKeyViewModel ApiKeyViewModel { get; }

        public ApiKeyAddedEventArgs(ApiKeyViewModel apiKeyViewModel)
        {
            ApiKeyViewModel = apiKeyViewModel;
        }
    }
}