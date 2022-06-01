#region Includes
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjectsTests.TestClasses;
using NUnit.Framework;
#endregion

namespace KellermanSoftware.CompareNetObjectsTests
{
    [TestFixture(Description = "Tests for CompareLogic"), Category("CompareLogic")]
    public class CompareLogicTests
    {
        #region Class Variables
        private CompareLogic _compare;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            _compare = new CompareLogic();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            _compare = null;
        }
        #endregion

        #region Documentation Tests
        [Test]
        public void DocumentationTest()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            //Create a couple objects to compare
            Person person1 = new Person();
            person1.DateCreated = DateTime.Now;
            person1.Name = "Greg";

            Person person2 = new Person();
            person2.Name = "John";
            person2.DateCreated = person1.DateCreated;

            //These will be different, write out the differences
            ComparisonResult result = compareLogic.Compare(person1, person2);
            if (!result.AreEqual)
                Console.WriteLine(result.DifferencesString);
        }

        #endregion



        #region Caching Tests
        [Test]
        public void CachingTest()
        {
            List<Person> list1 = new List<Person>();
            List<Person> list2 = new List<Person>();

            for (int i = 1; i <= 10000; i++)
            {
                Person person = new Person();
                person.DateCreated = DateTime.Now;
                person.Name = "Robot " + i;
                list1.Add(person);
                list2.Add(Common.CloneWithSerialization(person));
            }

            _compare.Config.Caching = false;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Assert.IsTrue(_compare.Compare(list1,list2).AreEqual);
            watch.Stop();
            long timeWithNoCaching = watch.ElapsedMilliseconds;
            Console.WriteLine("Compare 10000 objects no caching: {0} milliseconds", timeWithNoCaching);

            _compare.Config.Caching = true;
            watch.Reset();
            watch.Start();
            Assert.IsTrue(_compare.Compare(list1, list2).AreEqual);
            watch.Stop();
            long timeWithCaching = watch.ElapsedMilliseconds;
            Console.WriteLine("Compare 10000 objects with caching: {0} milliseconds", timeWithCaching);

            Assert.IsTrue(timeWithCaching < timeWithNoCaching);
        }

        #endregion








    }
}
