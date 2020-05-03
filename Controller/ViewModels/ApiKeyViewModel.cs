namespace DW.ELA.Controller.ViewModels
{
    using Interfaces;

    public enum ApiKeyValidity { Valid, Invalid, Unknown, Checking, NotChecked }
    public class ApiKeyViewModel : ViewModelBase
    {
        public static ApiKeyViewModel Empty() => new ApiKeyViewModel("", "");
        public ApiKeyViewModel(string commander, string key, ApiKeyValidity validity = ApiKeyValidity.NotChecked)
        {
            Commander = commander;
            Key = key;
            Validity = validity;
        }
        
        private string commander = "";
        public string Commander
        {
            get => commander;
            set => RaiseAndSetIfChanged(ref commander, value);
        }
        
        private string key = "";
        public string Key
        {
            get => key;
            set => RaiseAndSetIfChanged(ref key, value);
        }
        
        private ApiKeyValidity validity;
        public ApiKeyValidity Validity
        {
            get => validity;
            set => RaiseAndSetIfChanged(ref validity, value);
        }
    }
}