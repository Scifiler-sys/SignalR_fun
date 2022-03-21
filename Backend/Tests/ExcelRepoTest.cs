using System.Collections.Generic;
using DL;
using Models;
using Xunit;

namespace Tests;

public class ExcelRepoTest
{
    [Fact]
    public void ReadShouldGiveListOfPopulation()
    {
        //Arrange
        IRepository<Population> repo = new ExcelRepository(@"C:\Users\Stephen - Work\Documents\Revature\SignalR_fun\Backend\API\DL\WPP2019_TotalPopulationBySex.xlsx");
        List<Population> popList = new List<Population>();

        //Act
        popList = repo.read();

        //Assert
        Assert.NotEmpty(popList);
        Assert.Equal(popList[0].Location, "United States of America");
    }
}