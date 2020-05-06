namespace EliteLogAgent.Autorun
{
    using DW.ELA.Interfaces;

    /// <summary>
    /// Auto starting an application is handled by external system (e.g. systemd, xinit, etc...)
    /// </summary>
    public class ExternalAutorunManager : IAutorunManager
    {
        public bool CanManage => false;
        public bool AutorunEnabled { get; set; }
    }
}