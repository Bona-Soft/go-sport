using System;

namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    /// <summary>
    /// Compare primitive types (long, int, short, byte etc.) and DateTime, decimal, and Guid
    /// </summary>
    public class SimpleTypeComparer : BaseTypeComparer
    {
        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public SimpleTypeComparer(RootComparer rootComparer) : base(rootComparer)
        {
        }

        /// <summary>
        /// Returns true if the type is a simple type
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsSimpleType(type1) && TypeHelper.IsSimpleType(type2);
        }

        /// <summary>
        /// Compare two simple types
        /// </summary>
        /// <param name="result">The comparison result</param>
        /// <param name="object1">The first object to compare</param>
        /// <param name="object2">The second object to compare</param>
        /// <param name="breadCrumb">The breadcrumb</param>
        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            //This should never happen, null check happens one level up
            if (object2 == null || object1 == null)
                return;

            IComparable valOne = object1 as IComparable;

            if (valOne == null)
                throw new Exception("Expected value does not implement IComparable");

            if (valOne.CompareTo(object2) != 0)
            {
                Difference difference = new Difference
                {
                    PropertyName = breadCrumb,
                    Object1Value = object1.ToString(),
                    Object2Value = object2.ToString(),
                    Object1 = new WeakReference(object1),
                    Object2 = new WeakReference(object2)
                };

                AddDifference(result, difference);
            }
        }
    }
}
