using System;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.Common.Extensions
{
    internal static class LongExtension
    {
        internal static string ToFileSizeString(this long value)
        {
            decimal pivote = value;
            var iterationCount = 0;

            while (pivote > 1024)
            {
                pivote /= 1024;
                iterationCount++;
            }

            pivote = Math.Round(pivote, 2);

            switch (iterationCount)
            {
                case 1:
                    return pivote + " KB";
                case 2:
                    return pivote + " MB";
                case 3:
                    return pivote + " GB";
                case 4:
                    return pivote + " TB";
                default:
                    return pivote + " B";
            }
        }
    }
}
