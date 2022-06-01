using System;
using System.Data;
using System.Globalization;

namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    /// <summary>
    /// Compare all tables and all rows in all tables
    /// </summary>
    public class DatasetComparer : BaseTypeComparer
    {
        private readonly DataTableComparer _compareDataTable;

        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public DatasetComparer(RootComparer rootComparer)
            : base(rootComparer)
        {
            _compareDataTable = new DataTableComparer(rootComparer);
        }

        /// <summary>
        /// Returns true if both objects are data sets
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsDataset(type1) && TypeHelper.IsDataset(type2);
        }

        /// <summary>
        /// Compare two data sets
        /// </summary>
        /// <param name="result">The comparison result</param>
        /// <param name="object1">The first dataset to compare</param>
        /// <param name="object2">The second dataset to compare</param>
        /// <param name="breadCrumb">The current breadcrumb</param>
        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            DataSet dataSet1 = object1 as DataSet;
            DataSet dataSet2 = object2 as DataSet;

            //This should never happen, null check happens one level up
            if (dataSet1 == null || dataSet2 == null)
                return;

            if (TableCountsDifferent(result, object1, object2, breadCrumb, dataSet2, dataSet1)) return;

            CompareEachTable(result, breadCrumb, dataSet1, dataSet2);
        }

        private bool TableCountsDifferent(ComparisonResult result, object object1, object object2, string breadCrumb,
                                     DataSet dataSet2, DataSet dataSet1)
        {
            if (dataSet1.Tables.Count != dataSet2.Tables.Count)
            {
                Difference difference = new Difference
                                            {
                                                PropertyName = breadCrumb,
                                                Object1Value = dataSet1.Tables.Count.ToString(CultureInfo.InvariantCulture),
                                                Object2Value = dataSet2.Tables.Count.ToString(CultureInfo.InvariantCulture),
                                                ChildPropertyName = "Tables.Count",
                                                Object1 = new WeakReference(object1),
                                                Object2 = new WeakReference(object2)
                                            };

                AddDifference(result, difference);

                if (result.ExceededDifferences)
                    return true;
            }
            return false;
        }

        private void CompareEachTable(ComparisonResult result, string breadCrumb, DataSet dataSet1, DataSet dataSet2)
        {
            for (int i = 0; i < Math.Min(dataSet1.Tables.Count, dataSet2.Tables.Count); i++)
            {
                string currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Tables", string.Empty,
                                                         dataSet1.Tables[i].TableName);

                _compareDataTable.CompareType(result, dataSet1.Tables[i], dataSet2.Tables[i], currentBreadCrumb);

                if (result.ExceededDifferences)
                    return;
            }
        }
    }
}
