using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Betting.Web.ApiControllers;
using Betting.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Betting.Web.Tests
{
    [TestClass]
    public class BettingTests
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            AppDomain.CurrentDomain.SetData(
                "DataDirectory",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            Database.Delete("BettingContext");
        }

        [TestInitialize]
        public void TestInit()
        {
            ClearDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ClearDatabase();
        }

        private void ClearDatabase()
        {
            BettingContext dbContext = new BettingContext();
            dbContext.Database.Delete();
            dbContext.Database.Create();
        }

        [TestMethod]
        public void TestBetting()
        {
            var person1Name = "Person1";
            var person2Name = "Person2";

            var race1Name = "Race1";
            var race2Name = "Race2";

            var peopleController = new PeopleController();
            var racesController = new RacesController();
            var raceListsController = new RaceListsController();
            var raceBetsController = new RaceBetsController();

            peopleController.PostPerson(new Person { Name = person1Name }).Wait();
            peopleController.PostPerson(new Person { Name = person2Name }).Wait();

            racesController.PostRace(new Race { Name = race1Name }).Wait();
            racesController.PostRace(new Race { Name = race2Name }).Wait();

            var people = peopleController.GetPeople().ToList();
            Assert.AreEqual(2, people.Count);

            var races = racesController.GetRaces().ToList();
            Assert.AreEqual(2, races.Count);

            raceListsController.PostRaceList(new RaceList
            {
                Position = 1,
                RaceId = races.First(x => x.Name == race1Name).Id,
                PersonId = people.First(x => x.Name == person1Name).Id
            }).Wait();
            raceListsController.PostRaceList(new RaceList
            {
                Position = 1,
                RaceId = races.First(x => x.Name == race2Name).Id,
                PersonId = people.First(x => x.Name == person1Name).Id
            }).Wait();
            raceListsController.PostRaceList(new RaceList
            {
                Position = 2,
                RaceId = races.First(x => x.Name == race2Name).Id,
                PersonId = people.First(x => x.Name == person2Name).Id
            }).Wait();

            var raceLists = raceListsController.GetRaceLists().ToList();
            Assert.AreEqual(3, raceLists.Count);

            raceBetsController.PostRaceBet(new RaceBet
            {
                Position = 1,
                RaceId = races.First(x => x.Name == race1Name).Id,
                PersonId = people.First(x => x.Name == person1Name).Id,
                RaceListId = raceLists.First(x => x.Race.Name == race1Name && x.Person.Name == person1Name).Id
            }).Wait();
            raceBetsController.PostRaceBet(new RaceBet
            {
                Position = 2,
                RaceId = races.First(x => x.Name == race2Name).Id,
                PersonId = people.First(x => x.Name == person1Name).Id,
                RaceListId = raceLists.First(x => x.Race.Name == race2Name && x.Person.Name == person1Name).Id
            }).Wait();
            raceBetsController.PostRaceBet(new RaceBet
            {
                Position = 2,
                RaceId = races.First(x => x.Name == race2Name).Id,
                PersonId = people.First(x => x.Name == person2Name).Id,
                RaceListId = raceLists.First(x => x.Race.Name == race2Name && x.Person.Name == person2Name).Id
            }).Wait();

            var raceBets = raceBetsController.GetRaceBets().ToList();
            Assert.AreEqual(3, raceBets.Count);

            raceBetsController.DeleteRaceBet(raceBets.Last().Id).Wait();
            Assert.AreEqual(2, raceBetsController.GetRaceBets().ToList().Count);
            Assert.AreEqual(3, raceListsController.GetRaceLists().ToList().Count);

            raceListsController.DeleteRaceList(raceLists[1].Id).Wait();
            Assert.AreEqual(1, raceBetsController.GetRaceBets().ToList().Count);
            Assert.AreEqual(2, raceListsController.GetRaceLists().ToList().Count);

            peopleController.DeletePerson(people.First().Id).Wait();
            peopleController.DeletePerson(people.Last().Id).Wait();

            racesController.DeleteRace(races.First().Id).Wait();
            racesController.DeleteRace(races.Last().Id).Wait();

            Assert.AreEqual(0, peopleController.GetPeople().ToList().Count);
            Assert.AreEqual(0, racesController.GetRaces().ToList().Count);
            Assert.AreEqual(0, raceListsController.GetRaceLists().ToList().Count);
            Assert.AreEqual(0, raceBetsController.GetRaceBets().ToList().Count);
        }

        [TestMethod]
        public void TestGetScore()
        {
            var peopleController = new PeopleController();
            var racesController = new RacesController();
            var raceListsController = new RaceListsController();
            var raceBetsController = new RaceBetsController();

            peopleController.PostPerson(new Person { Id = 1, Name = "Person1" }).Wait();
            peopleController.PostPerson(new Person { Id = 2, Name = "Person2" }).Wait();
            peopleController.PostPerson(new Person { Id = 3, Name = "Person3" }).Wait();
            peopleController.PostPerson(new Person { Id = 4, Name = "Person4" }).Wait();
            peopleController.PostPerson(new Person { Id = 5, Name = "Person5" }).Wait();
            peopleController.PostPerson(new Person { Id = 6, Name = "Person6" }).Wait();
            peopleController.PostPerson(new Person { Id = 7, Name = "Person7" }).Wait();

            racesController.PostRace(new Race { Id = 1, Name = "Race1" }).Wait();

            raceListsController.PostRaceList(new RaceList { Id = 1, Position = 1, RaceId = 1, PersonId = 1 }).Wait();
            raceListsController.PostRaceList(new RaceList { Id = 2, Position = 2, RaceId = 1, PersonId = 2 }).Wait();
            raceListsController.PostRaceList(new RaceList { Id = 3, Position = 3, RaceId = 1, PersonId = 3 }).Wait();
            raceListsController.PostRaceList(new RaceList { Id = 4, Position = 4, RaceId = 1, PersonId = 4 }).Wait();
            raceListsController.PostRaceList(new RaceList { Id = 5, Position = 5, RaceId = 1, PersonId = 5 }).Wait();

            raceBetsController.PostRaceBet(new RaceBet { Id = 1, Position = 1, RaceId = 1, PersonId = 1, RaceListId = 1 }).Wait(); // 5
            raceBetsController.PostRaceBet(new RaceBet { Id = 2, Position = 1, RaceId = 1, PersonId = 2, RaceListId = 2 }).Wait(); // 2
            raceBetsController.PostRaceBet(new RaceBet { Id = 3, Position = 1, RaceId = 1, PersonId = 3, RaceListId = 3 }).Wait(); // 1
            raceBetsController.PostRaceBet(new RaceBet { Id = 4, Position = 1, RaceId = 1, PersonId = 4, RaceListId = 4 }).Wait(); // 0
            raceBetsController.PostRaceBet(new RaceBet { Id = 5, Position = 2, RaceId = 1, PersonId = 5, RaceListId = 2 }).Wait(); // 3

            raceBetsController.PostRaceBet(new RaceBet { Id = 5, Position = 1, RaceId = 1, PersonId = 6, RaceListId = 1 }).Wait(); // 5
            raceBetsController.PostRaceBet(new RaceBet { Id = 6, Position = 2, RaceId = 1, PersonId = 6, RaceListId = 2 }).Wait(); // 3
            raceBetsController.PostRaceBet(new RaceBet { Id = 7, Position = 3, RaceId = 1, PersonId = 6, RaceListId = 3 }).Wait(); // 3
            raceBetsController.PostRaceBet(new RaceBet { Id = 8, Position = 4, RaceId = 1, PersonId = 6, RaceListId = 4 }).Wait(); // 3

            raceBetsController.PostRaceBet(new RaceBet { Id = 9, Position = 1, RaceId = 1, PersonId = 7, RaceListId = 5 }).Wait(); // 0

            var score = new RaceController().GetRaceScore(1).OrderByDescending(x => x.Score).ToList();
            Assert.AreEqual(7, score.Count);
            //Assert.AreEqual(14, score[0].Score);
            Assert.AreEqual(12, score[0].Score);
            //Assert.AreEqual(5, score[1].Score);
            Assert.AreEqual(3, score[1].Score);
            Assert.AreEqual(3, score[2].Score);
            Assert.AreEqual(1, score[3].Score);
            Assert.AreEqual(0, score[4].Score);
            Assert.AreEqual(0, score[5].Score);
            Assert.AreEqual(0, score[6].Score);

            Assert.AreEqual("Person6", score[0].Name);
            Assert.AreEqual("Person1", score[1].Name);
            Assert.AreEqual("Person5", score[2].Name);
            Assert.AreEqual("Person2", score[3].Name);
            Assert.AreEqual("Person3", score[4].Name);
            Assert.AreEqual("Person4", score[5].Name);
        }
    }
}