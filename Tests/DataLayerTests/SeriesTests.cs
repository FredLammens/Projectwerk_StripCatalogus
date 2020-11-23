using Data.Repositories;
using DataLayer;
using DomainLibrary.DomainLayer;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace Tests.DataLayerTests
{
    [TestClass]
    public class SeriesTests
    {
        AdoNetContext context = new AdoNetContext(true, "Test");
        [TestMethod]
        public void TestAddSeries()
        {
            ComicRepository cr = new ComicRepository(context);
            Series series1 = new Series("series1");
            Series series2 = new Series("series2");
            var result1 = cr.GetAllSeries();
            result1.Should().HaveCount(0);
            cr.AddSeries(series1);
            result1 = cr.GetAllSeries();
            result1.Should().HaveCount(1);            
            result1.First().Name.Should().Be("series1");
            cr.AddSeries(series2);
            result1 = cr.GetAllSeries();
            result1.Should().HaveCount(2);
            result1.First().Name.Should().Be("series1");
            result1.ElementAt(1).Name.Should().Be("series2");
        }
        [TestMethod]
        public void TestUpdateSeries()
        {
            ComicRepository cr = new ComicRepository(context);
            Series seriestoUpdate = new Series("series1");
            Series updated = new Series("series2");
            cr.AddSeries(seriestoUpdate);
            var result1 = cr.GetAllSeries();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("series1");
            cr.UpdateSeries(seriestoUpdate, updated);
            result1 = cr.GetAllSeries();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("series2");
        }
        [TestMethod]
        public void TestGetAllSeries()
        {
            ComicRepository cr = new ComicRepository(context);
            Series series1 = new Series("series1");
            Series series2 = new Series("series2");
            Series series3 = new Series("series3");
            cr.AddSeries(series1);
            var result1 = cr.GetAllSeries();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("series1");

            cr.AddSeries(series2);
            cr.AddSeries(series3);
            result1 = cr.GetAllSeries();
            result1.Should().HaveCount(3);
            result1.ElementAt(1).Name.Should().Be("series2");
            result1.ElementAt(2).Name.Should().Be("series3");
        }
        [TestMethod]
        public void CheckDuplicateSeries()
        {
            ComicRepository cr = new ComicRepository(context);
            Series series1 = new Series("series1");
            Series series2 = new Series("series1");
            cr.AddSeries(series1);
            cr.AddSeries(series2);
            var result1 = cr.GetAllSeries();
            result1.Should().HaveCount(1);
            result1.First().Name.Should().Be("series1");
            Series series3 = new Series("series2");
            cr.AddSeries(series3);
            var result2 = cr.GetAllSeries();
            result2.Should().HaveCount(2);
            result2.First().Name.Should().Be("series1");
            result2.ElementAt(1).Name.Should().Be("series2");
        }
    }
}
