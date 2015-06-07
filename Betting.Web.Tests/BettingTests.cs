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
        public static void Clean()
        {
            Database.Delete("BettingContext");
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

            peopleController.PostPerson(new Person {Name = person1Name}).Wait();
            peopleController.PostPerson(new Person {Name = person2Name}).Wait();

            racesController.PostRace(new Race {Name = race1Name}).Wait();
            racesController.PostRace(new Race {Name = race2Name}).Wait();

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
    }
}