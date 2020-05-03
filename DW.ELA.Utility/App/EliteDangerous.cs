using System.Diagnostics;

namespace DW.ELA.Utility.App
{
    public static class EliteDangerous
    {
        // TODO: implement linux/proton check
        public static bool IsRunning => Process.GetProcessesByName("EliteDangerous64").Length > 0;
    }
}
