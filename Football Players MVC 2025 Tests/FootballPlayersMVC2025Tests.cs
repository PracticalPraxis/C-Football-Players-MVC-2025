using ArsenalPlayers.Controllers;
using ArsenalPlayers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Football_Players_MVC_2025_Tests
{
    [TestClass]
    public class FootballPlayersMVC2025Tests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockContext = new Mock<ArsenalPlayers.Data.ArsenalPlayersContext>();
            var mockPlayerSet = new Mock<DbSet<Player>>();

            List<Player> playerList = GetTestPlayers();
            
            var queryablePlayerList = playerList.AsQueryable();


            var mockDBSet = new Mock<DbSet<Player>>();
            mockDBSet.As<IQueryable<Player>>().Setup(m => m.Provider).Returns(queryablePlayerList.Provider);
            mockDBSet.As<IQueryable<Player>>().Setup(m => m.Expression).Returns(queryablePlayerList.Expression);
            mockDBSet.As<IQueryable<Player>>().Setup(m => m.ElementType).Returns(queryablePlayerList.ElementType);
            mockDBSet.As<IQueryable<Player>>().Setup(m => m.GetEnumerator()).Returns(() => queryablePlayerList.GetEnumerator());

            mockContext.Setup(c => c.Player).Returns(mockPlayerSet.Object);
            var controller = new PlayersController(mockContext.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
        }

        private List<Player> GetTestPlayers()
        {
            List<Player> players = new List<Player>();
            players.Add(new Player()
            {
                Id = 1,
                PlayerName = "Test 1",
                Position = "Striker",
                JerseyNumber = 1,
                GoalsScored = 11
            });
            players.Add(new Player()
            {
                Id = 2,
                PlayerName = "Test 2",
                Position = "Goalie",
                JerseyNumber = 2,
                GoalsScored = 3
            });
            return players;
        }
    }
}