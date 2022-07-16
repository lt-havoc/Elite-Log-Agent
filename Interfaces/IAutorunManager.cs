namespace DW.ELA.Interfaces;

public interface IAutorunManager
{
    bool CanManage { get; }
    bool AutorunEnabled { get; set; }
}