using System;
using System.Drawing;


namespace KellermanSoftware.CompareNetObjects.TypeComparers
{

    /// <summary>
    /// Class FontDescriptorComparer.
    /// </summary>
    public class FontComparer : BaseTypeComparer
    {
        /// <summary>
        /// Protected constructor that references the root comparer
        /// </summary>
        /// <param name="rootComparer">The root comparer.</param>
        public FontComparer(RootComparer rootComparer) : base(rootComparer)
        {
        }

        /// <summary>
        /// If true the type comparer will handle the comparison for the type
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns><c>true</c> if [is type match] [the specified type1]; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsFont(type1) && TypeHelper.IsFont(type2);
        }

        /// <summary>
        /// Compare the two objects
        /// </summary>
        /// <param name="result">The comparison result</param>
        /// <param name="object1">The value of the first object</param>
        /// <param name="object2">The value of the second object</param>
        /// <param name="breadCrumb">The breadcrumb</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void CompareType(ComparisonResult result, object object1, object object2, string breadCrumb)
        {
            Font font1 = object1 as Font;
            Font font2 = object2 as Font;

            string currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Bold");
            RootComparer.Compare(result, font1.Bold, font2.Bold, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "FontFamily.Name");
            RootComparer.Compare(result, font1.FontFamily.Name, font2.FontFamily.Name, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "OriginalFontName");
            RootComparer.Compare(result, font1.OriginalFontName, font2.OriginalFontName, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Size");
            RootComparer.Compare(result, font1.Size, font2.Size, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "SizeInPoints");
            RootComparer.Compare(result, font1.SizeInPoints, font2.SizeInPoints, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Strikeout");
            RootComparer.Compare(result, font1.Strikeout, font2.Strikeout, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Style");
            RootComparer.Compare(result, font1.Style.ToString(), font2.Style.ToString(), currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "SystemFontName");
            RootComparer.Compare(result, font1.SystemFontName, font2.SystemFontName, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Underline");
            RootComparer.Compare(result, font1.Underline, font2.Underline, currentBreadCrumb);

            currentBreadCrumb = AddBreadCrumb(result.Config, breadCrumb, "Unit");
            RootComparer.Compare(result, font1.Unit.ToString(), font2.Unit.ToString(), currentBreadCrumb);

        }
    }
}
