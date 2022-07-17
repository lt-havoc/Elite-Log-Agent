using System;
using DW.ELA.Utility;
using NUnit.Framework;

namespace DW.ELA.UnitTests;

public class SteamHelperTests
{
    [Test]
    public void ParseAppLibraryPath_Returns_Null_When_AppId_Not_Found()
    {
        var libraryFoldersVdf = """
            "libraryfolders"
            {
                "0"
                {
                    "path"		"/test/path/1"
                    "apps"
                    {
                        "1234"		"0123456789"
                    }
                }
                "1"
                {
                    "path"		"/test/path/2"
                    "apps"
                    {
                        "1235"		"0123456789"
                    }
                }
            }
            """
            .Split(Environment.NewLine);

        var result = SteamHelper.ParseAppLibraryPath("0", libraryFoldersVdf);
        
        Assert.IsNull(result);
    }
    
    [Test]
    public void ParseAppLibraryPath_Parses_Path()
    {
        var libraryFoldersVdf = """
            "libraryfolders"
            {
                "0"
                {
                    "path"		"/test/path/1"
                    "apps"
                    {
                        "1234"		"0123456789"
                    }
                }
            }
            """
            .Split(Environment.NewLine);

        var result = SteamHelper.ParseAppLibraryPath("1234", libraryFoldersVdf);
        
        Assert.AreEqual("/test/path/1", result);
    }
    
    [Test]
    public void ParseAppLibraryPath_Prioritizes_Last_Defined()
    {
        var libraryFoldersVdf = """
            "libraryfolders"
            {
                "0"
                {
                    "path"		"/test/path/1"
                    "apps"
                    {
                        "1234"		"0123456789"
                    }
                }
                "1"
                {
                    "path"		"/test/path/2"
                    "apps"
                    {
                        "1235"		"0123456789"
                    }
                }
                "2"
                {
                    "path"		"/test/path/3"
                    "apps"
                    {
                        "1235"		"0123456789"
                    }
                }
            }
            """
            .Split(Environment.NewLine);

        var result = SteamHelper.ParseAppLibraryPath("1235", libraryFoldersVdf);
        
        Assert.AreEqual("/test/path/3", result);
    }
}