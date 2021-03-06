using System;
using System.Collections.Generic;
using System.Linq;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjectsTests.TestClasses;
using NUnit.Framework;

namespace KellermanSoftware.CompareNetObjectsTests
{
    [TestFixture]
    public class IgnoreOrderTests
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

        #region Tests

        [Test]
        public void CollectionDisorderedWithSpecMatchAndOffByCount()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();

            entity1.Children.Add(new Entity
            {
                Description = "1",
                EntityLevel = Level.Company
            });
            entity1.Children.Add(new Entity
            {
                Description = "5",
                EntityLevel = Level.Division
            });
            entity1.Children.Add(new Entity
            {
                Description = "3",
                EntityLevel = Level.Department
            });


            entity2.Children.Add(new Entity
            {
                Description = "1",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "2",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "4",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "5",
                EntityLevel = Level.Division
            });

            entity2.Children.Add(new Entity
            {
                Description = "3",
                EntityLevel = Level.Division
            });

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.MaxDifferences = int.MaxValue;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);
            Console.WriteLine(result.DifferencesString);

            Assert.AreEqual(result.Differences.Where(d => d.Object1Value == Level.Company.ToString() && d.Object2Value == Level.Department.ToString()).Count(), 1);

            Assert.AreEqual(result.Differences.Where(d => d.ToString().Contains("Description:")).Count(), 4);

            Assert.AreEqual(result.Differences.Where(d => d.Object1Value == "(null)" || d.Object2Value == "(null)").Count(), 2);

        }



        [Test]
        public void CollectionWithSpecMatch()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();
            var len = 3;

            for (int i = 0; i < len; i++)
            {
                entity1.Children.Add(new Entity
                {
                    Description = i.ToString(),
                    EntityLevel = Level.Company
                });

                entity2.Children.Add(new Entity
                {
                    Description = (i).ToString(),
                    EntityLevel = Level.Department
                });
            }

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;

            _compare.Config.MaxDifferences = int.MaxValue;

            var result = _compare.Compare(entity1, entity2);

            Assert.AreEqual(result.Differences[0].Object1Value, Level.Company.ToString());

            Assert.AreEqual(result.Differences[0].Object2Value, Level.Department.ToString());

            Assert.IsTrue(result.Differences[0].ToString().Contains("Description:"));
        }

        [Test]
        public void CollectionWithSpecMatchMatchSpecObjectDifference()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();

            entity1.Children.Add(new Entity
            {
                Description = "Entity",
                EntityLevel = Level.Company
            });

            entity2.Children.Add(new Entity
            {
                Description = "Entity",
                EntityLevel = Level.Department
            });


            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);

            Assert.AreEqual(result.Differences[0].Object1Value, Level.Company.ToString());

            Assert.AreEqual(result.Differences[0].Object2Value, Level.Department.ToString());

            Assert.IsTrue(result.Differences[0].ToString().Contains("Description:"));
        }

        [Test]
        public void CollectionWithSpecNoMatch()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();
            var len = 3;

            for (int i = 0; i < len; i++)
            {
                entity1.Children.Add(new Entity
                {
                    Description = i.ToString(),
                    EntityLevel = Level.Company
                });

                entity2.Children.Add(new Entity
                {
                    Description = (i).ToString(),
                    EntityLevel = Level.Department
                });
            }

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description", "EntityLevel" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);

            Assert.AreEqual(result.Differences[0].Object1Value, typeof(Entity).FullName);

            Assert.AreEqual(result.Differences[0].Object2Value, "(null)");

        }

        [Test]
        public void GenericEntityListMultipleItemsOddOrderTest()
        {
            GenericEntity<IEntity> genericEntity = new GenericEntity<IEntity>();
            genericEntity.MyList = new List<IEntity>();

            GenericEntity<IEntity> genericEntityCopy = new GenericEntity<IEntity>();
            genericEntityCopy.MyList = new List<IEntity>();

            //Brave Sir Robin Security Company
            Entity top1 = new Entity();
            top1.Description = "Brave Sir Robin Security Company";
            top1.Parent = null;
            top1.EntityLevel = Level.Company;

            //Brave Sir Robin Security Company
            Entity top2 = new Entity();
            top2.Description = "Brave Sir Robin Security Company";
            top2.Parent = null;
            top2.EntityLevel = Level.Company;

            //Brave Sir Robin Security Company
            Entity top5 = new Entity();
            top5.Description = "Brave Sir Robin Security Company";
            top5.Parent = null;
            top5.EntityLevel = Level.Company;

            Entity top3 = new Entity();
            top3.Description = "May I obey all your commands with equal pleasure, Sire!";
            top3.Parent = null;
            top3.EntityLevel = Level.Department;

            Entity top4 = new Entity();
            top4.Description = "Overtaxed, overworked and paid off with a knife, a club or a rope.";
            top4.Parent = null;
            top4.EntityLevel = Level.Department;


            genericEntity.MyList.Add(top4);
            genericEntity.MyList.Add(top3);
            genericEntity.MyList.Add(top5);
            genericEntity.MyList.Add(top2);
            genericEntityCopy.MyList.Add(top2);
            genericEntityCopy.MyList.Add(top1);
            genericEntityCopy.MyList.Add(top3);
            genericEntityCopy.MyList.Add(top4);

            _compare.Config.IgnoreCollectionOrder = true;

            var result = _compare.Compare(genericEntity, genericEntityCopy);

            Console.WriteLine(result.DifferencesString);

            Assert.IsTrue(_compare.Compare(genericEntity, genericEntityCopy).AreEqual);

            genericEntity.MyList[2].Description = "When danger reared its ugly head Brave Sir Robin fled.";

            Assert.IsFalse(_compare.Compare(genericEntity, genericEntityCopy).AreEqual);
        }

        [Test]
        public void GenericEntityListMultipleItemsWithIgnoreCollectionOrderTest()
        {
            GenericEntity<IEntity> genericEntity = new GenericEntity<IEntity>();
            genericEntity.MyList = new List<IEntity>();

            //Brave Sir Robin Security Company
            Entity top1 = new Entity();
            top1.Description = "Brave Sir Robin Security Company";
            top1.Parent = null;
            top1.EntityLevel = Level.Company;

            GenericEntity<IEntity> genericEntityCopy = new GenericEntity<IEntity>();
            genericEntityCopy.MyList = new List<IEntity>();

            //Brave Sir Robin Security Company
            Entity top2 = new Entity();
            top2.Description = "Brave Sir Robin Security Company";
            top2.Parent = null;
            top2.EntityLevel = Level.Company;


            Entity top3 = new Entity();
            top3.Description = "May I obey all your commands with equal pleasure, Sire!";
            top3.Parent = null;
            top3.EntityLevel = Level.Department;


            genericEntity.MyList.Add(top3);
            genericEntity.MyList.Add(top1);
            genericEntityCopy.MyList.Add(top2);
            genericEntityCopy.MyList.Add(top3);

            _compare.Config.IgnoreCollectionOrder = true;

            Assert.IsTrue(_compare.Compare(genericEntity, genericEntityCopy).AreEqual);

            genericEntityCopy.MyList[0].Description = "When danger reared its ugly head Brave Sir Robin fled.";

            Assert.IsFalse(_compare.Compare(genericEntity, genericEntityCopy).AreEqual);
        }

        [Test]
        public void HashSetsMultipleItemsWithIgnoreCollectionOrderTest()
        {
            HashSetWrapper hashSet1 = new HashSetWrapper
            {
                StatusId = 1,
                Name = "Paul"
            };
            HashSetWrapper hashSet2 = new HashSetWrapper
            {
                StatusId = 1,
                Name = "Paul"
            };

            HashSetClass secondClassObject1 = new HashSetClass
            {
                Id = 1
            };
            HashSetClass secondClassObject2 = new HashSetClass
            {
                Id = 2
            };

            HashSetClass secondClassObject3 = new HashSetClass
            {
                Id = 3
            };
            HashSetClass secondClassObject4 = new HashSetClass
            {
                Id = 4
            };


            hashSet1.HashSetCollection.Add(secondClassObject3);
            hashSet1.HashSetCollection.Add(secondClassObject1);
            hashSet1.HashSetCollection.Add(secondClassObject2);
            hashSet1.HashSetCollection.Add(secondClassObject4);

            hashSet2.HashSetCollection.Add(secondClassObject2);
            hashSet2.HashSetCollection.Add(secondClassObject4);
            hashSet2.HashSetCollection.Add(secondClassObject3);
            hashSet2.HashSetCollection.Add(secondClassObject1);

            _compare.Config.IgnoreCollectionOrder = true;

            ComparisonResult result = _compare.Compare(hashSet1, hashSet2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void TestDictionaryWithIgnoreCollectionOrder()
        {
            var p1 = new Person();
            p1.DateCreated = DateTime.Now;
            p1.Name = "Owen";

            var p2 = new Person();
            p2.Name = "Greg";
            p2.DateCreated = DateTime.Now.AddDays(-1);

            var p3 = new Person();
            p3.Name = "Wink";
            p3.DateCreated = DateTime.Now.AddDays(-2);

            var dict1 = new Dictionary<string, Person>();
            dict1.Add("1001", p1);
            dict1.Add("1002", p2);

            var dict2 = new Dictionary<string, Person>();
            dict2.Add("1002", p2);
            dict2.Add("1001", p1);

            _compare.Config.IgnoreCollectionOrder = true;

            ComparisonResult result = _compare.Compare(dict1, dict2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);

            result = _compare.Compare(dict2, dict1);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);


            dict1.Add("1003", p3);
            dict2.Add("1003", p3);

            result = _compare.Compare(dict1, dict2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);

            result = _compare.Compare(dict2, dict1);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void TestIndexerWithIgnoreCollectionOrder()
        {
            var jane = new Person { Name = "Jane" };
            var john = new Person { Name = "Mary" };
            var mary = new Person { Name = "Mary" };
            var jack = new Person { Name = "Jack" };

            var nameList1 = new List<Person>() { jane, jack, mary };
            var nameList2 = new List<Person>() { jane, john, jack };

            var class1 = new ListClass<Person>(nameList1);
            var class2 = new ListClass<Person>(nameList2);

            _compare.Config.IgnoreCollectionOrder = true;

            Assert.IsTrue(_compare.Compare(class1, class2).AreEqual);
            Assert.IsTrue(_compare.Compare(class2, class1).AreEqual);
        }

        [Test]
        public void TestIndexerWithIgnoreCollectionOrderNegative()
        {
            var jane = new Person { Name = "Jane" };
            var john = new Person { Name = "Mary" };
            var mary = new Person { Name = "Mary" };
            var jack = new Person { Name = "Jack" };
            var jo = new Person { Name = "Jo" };
            var sarah = new Person { Name = "Sarah" };

            var nameList1 = new List<Person>() { jane, jack, mary, jo };
            var nameList2 = new List<Person>() { jane, john, jack, sarah };

            var class1 = new ListClass<Person>(nameList1);
            var class2 = new ListClass<Person>(nameList2);

            _compare.Config.IgnoreCollectionOrder = true;

            Assert.IsFalse(_compare.Compare(class1, class2).AreEqual);
            Assert.IsFalse(_compare.Compare(class2, class1).AreEqual);
        }

        [Test]
        public void TestIndexerWithIgnoreCollectionLengthNegative()
        {
            var jane = new Person { Name = "Jane" };
            var john = new Person { Name = "John" };
            var mary = new Person { Name = "Mary" };
            var jack = new Person { Name = "Jack" };

            var nameList1 = new List<Person>() { jane, john, jack, mary };
            var nameList2 = new List<Person>() { jane, john, jack };

            var class1 = new ListClass<Person>(nameList1);
            var class2 = new ListClass<Person>(nameList2);

            var prior = _compare.Config.MaxDifferences;
            _compare.Config.MaxDifferences = int.MaxValue;
            _compare.Config.IgnoreCollectionOrder = true;

            var result = _compare.Compare(class1, class2);
            Console.WriteLine(result.DifferencesString);

            Assert.AreEqual(2, result.Differences.Count);

            result = _compare.Compare(class2, class1);
            Console.WriteLine(result.DifferencesString);
            Assert.AreEqual(2, result.Differences.Count);


            _compare.Config.MaxDifferences = prior;
        }

        [Test]
        public void CollectionWithSpecComplexMatch()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();
            var len = 3;

            var parent = new Entity
            {
                Description = "Me Papa",
                EntityLevel = Level.Company
            };

            //entity1.Parent = parent;
            //entity2.Parent = parent;

            for (int i = 0; i < len; i++)
            {
                entity1.Children.Add(new Entity
                {
                    Description = i.ToString(),
                    EntityLevel = Level.Division,
                    Parent = parent
                });


                entity2.Children.Add(new Entity
                {
                    Description = (i).ToString(),
                    EntityLevel = Level.Division,
                    Parent = parent
                });
            }

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Parent", "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;
            _compare.Config.MaxDifferences = int.MaxValue;

            var result = _compare.Compare(entity1, entity2);

            Assert.That(result.AreEqual);
        }

        [Test]
        public void CollectionFirstBiggerThanSecondWithSpecMatchHasSpecMatchInResult()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();

            entity1.Children.Add(new Entity
            {
                Description = "Entity1",
                EntityLevel = Level.Company
            });
            entity1.Children.Add(new Entity
            {
                Description = "Entity2",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "Entity2",
                EntityLevel = Level.Department
            });


            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.MaxDifferences = int.MaxValue;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);

            Assert.IsTrue(result.Differences.Where(d => d.Object1Value == "(null)" || d.Object2Value == "(null)").Count() > 0);
            Assert.IsTrue(result.Differences.Where(d => d.ToString().Contains("Description:")).Count() > 0);


        }

        [Test]
        public void CollectionWithSpecEmptyProps()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();
            var len = 3;

            for (int i = 0; i < len; i++)
            {
                entity1.Children.Add(new Entity
                {
                    Description = i.ToString(),
                    EntityLevel = Level.Company
                });

                entity2.Children.Add(new Entity
                {
                    Description = (i).ToString(),
                    EntityLevel = Level.Department
                });
            }

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);


            Assert.AreEqual(result.Differences[0].Object1Value, Level.Company.ToString());

        }

        [Test]
        public void CollectionWithSpecEmptyPropsAndEquality()
        {
            var entity1 = new EntityWithEquality();
            var entity2 = new EntityWithEquality();
            var len = 3;

            for (int i = 0; i < len; i++)
            {
                entity1.Children.Add(new EntityWithEquality
                {
                    Description = i.ToString(),
                    EntityLevel = Level.Company
                });

                entity2.Children.Add(new EntityWithEquality
                {
                    Description = (i).ToString(),
                    EntityLevel = Level.Department
                });
            }

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(EntityWithEquality), new string[] { });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);


            Assert.AreEqual(result.Differences[0].Object1Value, Level.Company.ToString());

            Assert.AreEqual(result.Differences[0].Object2Value, Level.Department.ToString());

            Assert.IsTrue(result.Differences[0].ToString().Contains("(Company,Department)"));

        }

        [Test]
        public void TestDictionaryWithIgnoreCollectionOrderOddOrder()
        {
            var p1 = new Person();
            p1.DateCreated = DateTime.Now;
            p1.Name = "Owen";

            var p2 = new Person();
            p2.Name = "Greg";
            p2.DateCreated = DateTime.Now.AddDays(-1);

            var p3 = new Person();
            p3.Name = "Wink";
            p3.DateCreated = DateTime.Now.AddDays(-2);

            var p4 = new Person();
            p3.Name = "Larry";
            p3.DateCreated = DateTime.Now.AddDays(-3);

            var dict1 = new Dictionary<string, Person>();
            dict1.Add("1003", p3);
            dict1.Add("1001", p1);
            dict1.Add("1004", p4);
            dict1.Add("1002", p2);

            var dict2 = new Dictionary<string, Person>();
            dict2.Add("1002", p2);
            dict2.Add("1001", p1);
            dict2.Add("1003", p3);
            dict2.Add("1004", p4);

            Assert.IsFalse(_compare.Compare(dict1, dict2).AreEqual);
            Assert.IsFalse(_compare.Compare(dict2, dict1).AreEqual);
        }

        [Test]
        public void TestDictionaryWithIgnoreCollectionOrderNegative()
        {
            var p1 = new Person();
            p1.DateCreated = DateTime.Now;
            p1.Name = "Owen";

            var p2 = new Person();
            p2.Name = "Greg";
            p2.DateCreated = DateTime.Now.AddDays(-1);

            var p3 = new Person();
            p3.Name = "Wink";
            p3.DateCreated = DateTime.Now.AddDays(-2);

            var p4 = new Person();
            p3.Name = "Larry";
            p3.DateCreated = DateTime.Now.AddDays(-3);

            var dict1 = new Dictionary<string, Person>();
            dict1.Add("1001", p1);
            dict1.Add("1002", p2);
            dict1.Add("1003", p3);

            var dict2 = new Dictionary<string, Person>();
            dict2.Add("1002", p2);
            dict2.Add("1001", p1);
            dict2.Add("1003", p4);

            Assert.IsFalse(_compare.Compare(dict1, dict2).AreEqual);
        }

        [Test]
        public void HashSetsMultipleItemsWithIgnoreCollectionOrderNegativeTest()
        {
            HashSetWrapper hashSet1 = new HashSetWrapper
                                      {
                                          StatusId = 1,
                                          Name = "Paul"
                                      };
            HashSetWrapper hashSet2 = new HashSetWrapper
                                      {
                                          StatusId = 1,
                                          Name = "Paul"
                                      };

            HashSetClass secondClassObject1 = new HashSetClass
                                              {
                                                  Id = 1
                                              };
            HashSetClass secondClassObject2 = new HashSetClass
                                              {
                                                  Id = 2
                                              };

            HashSetClass secondClassObject3 = new HashSetClass
                                              {
                                                  Id = 3
                                              };
            HashSetClass secondClassObject4 = new HashSetClass
                                              {
                                                  Id = 4
                                              };


            hashSet1.HashSetCollection.Add(secondClassObject3);
            hashSet1.HashSetCollection.Add(secondClassObject1);
            hashSet1.HashSetCollection.Add(secondClassObject2);
            hashSet1.HashSetCollection.Add(secondClassObject4);

            hashSet2.HashSetCollection.Add(secondClassObject2);
            hashSet2.HashSetCollection.Add(secondClassObject4);
            hashSet2.HashSetCollection.Add(secondClassObject3);
            hashSet2.HashSetCollection.Add(secondClassObject1);

            _compare.Config.IgnoreCollectionOrder = false;


            Assert.IsFalse(_compare.Compare(hashSet1, hashSet2).AreEqual);
        }

        [Test]
        public void ComparerIgnoreOrderTest()
        {
            var elemA = new Element(1, 4);
            var elemB = Element.ReverseCopy(elemA);

            var comparer = new CompareLogic();
            comparer.Config.IgnoreCollectionOrder = true;

            ComparisonResult result = comparer.Compare(elemA, elemB);
            Assert.IsTrue(result.Differences.Count == 0); //difference count should be 0 but 1 difference is found
        }

        [Test]
        public void ComparerIgnoreOrderSimpleArraysTest()
        {
            var a = new String[] { "A", "E", "F" };
            var b = new String[] { "A", "c", "d", "F" };

            var comparer = new CompareLogic();
            comparer.Config.IgnoreCollectionOrder = true;
            comparer.Config.MaxDifferences = int.MaxValue;

            ComparisonResult result = comparer.Compare(a, b);
            Console.WriteLine(result.DifferencesString);
            Assert.IsTrue(result.Differences.Where(d => d.Object1Value == "E").Count() == 1);
            Assert.IsTrue(result.Differences.Where(d => d.Object2Value == "c").Count() == 1);
            Assert.IsTrue(result.Differences.Where(d => d.Object2Value == "d").Count() == 1);

        }

        [Test]
        public void CollectionDisorderedWithSpecMatch()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();

            entity1.Children.Add(new Entity
            {
                Description = "1",
                EntityLevel = Level.Company
            });
            entity1.Children.Add(new Entity
            {
                Description = "2",
                EntityLevel = Level.Division
            });
            entity1.Children.Add(new Entity
            {
                Description = "3",
                EntityLevel = Level.Department
            });
            entity1.Children.Add(new Entity
            {
                Description = "4",
                EntityLevel = Level.Employee
            });

            entity2.Children.Add(new Entity
            {
                Description = "2",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "4",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "1",
                EntityLevel = Level.Department
            });

            entity2.Children.Add(new Entity
            {
                Description = "3",
                EntityLevel = Level.Division
            });

            var spec = new Dictionary<Type, IEnumerable<string>>();

            spec.Add(typeof(Entity), new string[] { "Description" });

            _compare.Config.IgnoreCollectionOrder = true;

            _compare.Config.MaxDifferences = int.MaxValue;

            _compare.Config.CollectionMatchingSpec = spec;

            var result = _compare.Compare(entity1, entity2);

            Console.WriteLine(result.DifferencesString);

            Assert.AreEqual(result.Differences[0].Object1Value, Level.Company.ToString());

            Assert.AreEqual(result.Differences[0].Object2Value, Level.Department.ToString());

            Assert.IsTrue(result.Differences[0].ToString().Contains("Description:1"));

            Assert.IsTrue(result.Differences.Where(d => d.Object1Value == "(null)" || d.Object2Value == "(null)").ToArray().Length == 0);

        }
        #endregion
    }
}
