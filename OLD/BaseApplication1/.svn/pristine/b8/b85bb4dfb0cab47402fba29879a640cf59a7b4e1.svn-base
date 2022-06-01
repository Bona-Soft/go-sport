using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjectsTests.TestClasses;
using NUnit.Framework;

namespace KellermanSoftware.CompareNetObjectsTests
{
    [TestFixture]
    public class CompareStructTests
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

        #region Struct Tests
        [Test]
        public void TestStruct()
        {
            Size size1 = new Size();
            size1.Width = 800;
            size1.Height = 600;

            Size size2 = new Size();
            size2.Width = 1024;
            size2.Height = 768;

            List<Size> list1 = new List<Size>();
            list1.Add(size1);
            list1.Add(size2);

            List<Size> list2 = new List<Size>();
            list2.Add(size1);
            list2.Add(size2);

            ComparisonResult result = _compare.Compare(list1, list2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void TestStructNegative()
        {
            Size size1 = new Size();
            size1.Width = 800;
            size1.Height = 600;

            Size size2 = new Size();
            size2.Width = 1024;
            size2.Height = 768;

            List<Size> list1 = new List<Size>();
            list1.Add(size1);
            list1.Add(size2);

            List<Size> list2 = new List<Size>();
            list2.Add(size1);
            Size size3 = new Size();
            size3.Width = 1025;
            size3.Height = 768;
            list2.Add(size3);

            Assert.IsFalse(_compare.Compare(list1, list2).AreEqual);
        }

        [Test]
        public void TestStructWithNoPublicFields()
        {
            var point1 = new Point(1, 1);
            var point2 = new Point(1, 1);

            ComparisonResult result = _compare.Compare(point1, point2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void TestStructWithNoPublicFieldsNegative()
        {
            var point1 = new Point(1, 1);
            var point2 = new Point(2, 2);

            Assert.IsFalse(_compare.Compare(point1, point2).AreEqual);
        }

        [Test]
        public void TestStructWithPublicStaticPropOfSameType()
        {
            var point1 = StructWithStaticProperty.Origin;
            var point2 = StructWithStaticProperty.Origin;

            ComparisonResult result = _compare.Compare(point1, point2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        #endregion
    }
}
