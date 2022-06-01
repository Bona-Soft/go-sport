using System;
using System.Data;

namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    /// <summary>
    /// Compare all columns in a data row
    /// </summary>
    public class DataRowComparer : BaseTypeComparer
    {
        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public DataRowComparer(RootComparer rootComparer)
            : base(rootComparer)
        { }

        /// <summary>
        /// Returns true if this is a DataRow
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsDataRow(type1) && TypeHelper.IsDataRow(type2);
        }

        /// <summary>
        /// Compare two data rows
        /// </summary>
        /// <param name="result">The comparison result</param>
        /// <param name="object1">The first data row to compare</param>
        /// <param name="object2">The second data row to compare</param>
        /// <param name="breadCrumb">The breadcrumb</param>
        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            DataRow dataRow1 = object1 as DataRow;
            DataRow dataRow2 = object2 as DataRow;

            //This should never happen, null check happens one level up
            if (dataRow1 == null || dataRow2 == null)
                return;

            for (int i = 0; i < Math.Min(dataRow2.Table.Columns.Count, dataRow1.Table.Columns.Count); i++)
            {
                //Only compare specific column names
                if (result.Config.MembersToInclude.Count > 0 && !result.Config.MembersToInclude.Contains(dataRow1.Table.Columns[i].ColumnName))
                    continue;

                //If we should ignore it, skip it
                if (result.Config.MembersToInclude.Count == 0 && result.Config.MembersToIgnore.Contains(dataRow1.Table.Columns[i].ColumnName))
                    continue;

                //If we should ignore read only, skip it
                if (!result.Config.CompareReadOnly && dataRow1.Table.Columns[i].ReadOnly)
                    continue;

                //Both are null
                if (dataRow1.IsNull(i) && dataRow2.IsNull(i))
                    continue;

                string currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, string.Empty, string.Empty, dataRow1.Table.Columns[i].ColumnName);

                //Check if one of them is null
                if (dataRow1.IsNull(i))
                {
                    Difference difference = new Difference
                    {
                        PropertyName = currentBreadCrumb,
                        Object1Value = "(null)",
                        Object2Value = NiceString(object2),
                        Object1 = new WeakReference(object1),
                        Object2 = new WeakReference(object2)
                    };

                    AddDifference(result, difference);
                    return;
                }

                if (dataRow2.IsNull(i))
                {
                    Difference difference = new Difference
                    {
                        PropertyName = currentBreadCrumb,
                        Object1Value = NiceString(object1),
                        Object2Value = "(null)",
                        Object1 = new WeakReference(object1),
                        Object2 = new WeakReference(object2)
                    };

                    AddDifference(result, difference);
                    return;
                }

                //Check if one of them is deleted
                if (dataRow1.RowState == DataRowState.Deleted ^ dataRow2.RowState == DataRowState.Deleted)
                {
                    Difference difference = new Difference
                    {
                        PropertyName = currentBreadCrumb,
                        Object1Value = dataRow1.RowState.ToString(),
                        Object2Value = dataRow2.RowState.ToString(),
                        Object1 = new WeakReference(object1),
                        Object2 = new WeakReference(object2)
                    };

                    AddDifference(result, difference);
                    return;
                }


                RootComparer.Compare(result, dataRow1[i], dataRow2[i], currentBreadCrumb);

                if (result.ExceededDifferences)
                    return;
            }
        }
    }
}
