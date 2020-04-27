namespace DW.ELA.UnitTests.Controller
{
    using DW.ELA.Controller;
    using NUnit.Framework;

    // TODO: run on Windows only
    [Explicit]
    public class SavedGamesDirectoryHelperTests
    {
        [Test]
        public void ShouldFindSavesDirectory() => Assert.IsNotEmpty(new SavedGamesDirectoryHelper().Directory);
    }
}
