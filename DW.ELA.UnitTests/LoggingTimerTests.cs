﻿using System;
using DW.ELA.Utility.Log;
using NUnit.Framework;

namespace DW.ELA.UnitTests
{
    public class LoggingTimerTests
    {
        [Test]
        public void ShouldShowReasonableTime()
        {
            using var timer = new LoggingTimer("Test");
            Assert.That(timer.Elapsed, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
            Assert.That(timer.Elapsed, Is.LessThan(TimeSpan.FromSeconds(5)));
        }
    }
}
