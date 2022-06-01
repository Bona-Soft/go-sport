using System;
using System.Data;
using System.Globalization;


namespace KellermanSoftware.CompareNetObjects.TypeComparers
{
    /// <summary>
    /// Compare all rows in a data table
    /// </summary>
    public class DataTableComparer : BaseTypeComparer
    {
        private readonly DataRowComparer _compareDataRow;

        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public DataTableComparer(RootComparer rootComparer)
            : base(rootComparer)
        {
            _compareDataRow = new DataRowComparer(rootComparer);
        }

        /// <summary>
        /// Returns true if both objects are of type DataTable
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsDataTable(type1) && TypeHelper.IsDataTable(type2);
        }

        /// <summary>
        /// Compare two datatables
        /// </summary>
        /// <param name="result">The comparison result</param>
        /// <param name="object1">The first object to compare</param>
        /// <param name="object2">The second object to compare</param>
        /// <param name="breadCrumb">The breadcrumb</param>
        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            DataTable dataTable1 = object1 as DataTable;
            DataTable dataTable2 = object2 as DataTable;

            //This should never happen, null check happens one level up
            if (dataTable1 == null || dataTable2 == null)
                return;

            //Only compare specific table names
            if (result.Config.MembersToInclude.Count > 0 && !result.Config.MembersToInclude.Contains(dataTable1.TableName))
                return;

            //If we should ignore it, skip it
            if (result.Config.MembersToInclude.Count == 0 && result.Config.MembersToIgnore.Contains(dataTable1.TableName))
                return;

            //There must be the same amount of rows in the datatable
            if (dataTable1.Rows.Count != dataTable2.Rows.Count)
            {
                Difference difference = new Difference
                {
                    PropertyName = breadCrumb,
                    Object1Value = dataTable1.Rows.Count.ToString(CultureInfo.InvariantCulture),
                    Object2Value = dataTable2.Rows.Count.ToString(CultureInfo.InvariantCulture),
                    ChildPropertyName = "Rows.Count",
                    Object1 = new WeakReference(object1),
                    Object2 = new WeakReference(object2)
                };

                AddDifference(result, difference);

                if (result.ExceededDifferences)
                    return;
            }

            if (ColumnCountsDifferent(result, object1, object2, breadCrumb, dataTable2, dataTable1)) return;

            CompareEachRow(result, breadCrumb, dataTable1, dataTable2);
        }

        private bool ColumnCountsDifferent(ComparisonResult result, object object1, object object2, string breadCrumb,
                                   DataTable dataTable2, DataTable dataTable1)
        {
            if (dataTable1.Columns.Count != dataTable2.Columns.Count)
            {
                Difference difference = new Difference
                {
                    PropertyName = breadCrumb,
                    Object1Value = dataTable1.Columns.Count.ToString(CultureInfo.InvariantCulture),
                    Object2Value = dataTable2.Columns.Count.ToString(CultureInfo.InvariantCulture),
                    ChildPropertyName = "Columns.Count",
                    Object1 = new WeakReference(object1),
                    Object2 = new WeakReference(object2)
                };

                AddDifference(result, difference);

                if (result.ExceededDifferences)
                    return true;
            }
            return false;
        }

        private void CompareEachRow(ComparisonResult result, string breadCrumb, DataTable dataTable1, DataTable dataTable2)
        {
            for (int i = 0; i < Math.Min(dataTable1.Rows.Count, dataTable2.Rows.Count); i++)
            {
                string currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Rows", string.Empty, i);

                _compareDataRow.CompareType(result, dataTable1.Rows[i], dataTable2.Rows[i], currentBreadCrumb);

                if (result.ExceededDifferences)
                    return;
            }
        }


    }
}
