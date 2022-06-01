using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using KellermanSoftware.CompareNetObjectsTests.TestClasses;

namespace KellermanSoftware.CompareNetObjectsTests
{
    public class MyCustomComparer : BaseTypeComparer
    {
        public MyCustomComparer(RootComparer rootComparer) : base(rootComparer)
        {
        }

        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return type1 == typeof (SpecificTenant);
        }

        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            var st1 = (SpecificTenant)object1;
            var st2 = (SpecificTenant)object2;

            if (st1.Name != st2.Name || st1.Amount > 100 || st2.Amount < 100)
            {
                Difference difference = new Difference
                    {
                        PropertyName = breadCrumb,
                        Object1Value = object1.ToString(),
                        Object2Value = object2.ToString()
                    };

                result.Differences.Add(difference);
            }
        }
    }
}
