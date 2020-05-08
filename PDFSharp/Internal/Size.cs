﻿using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSharp.Internal
{
    internal class Size
    {
        // Top and bottom margins are larger to account for the header and footer
        public static readonly Unit TopBottomPageMargin = "0.30 in";
        public static readonly Unit LeftRightPageMargin = "0.50 in";

        public static readonly Unit HeaderFooterMargin = "0.7 in";

        public static readonly Unit TableCellPadding = "0.07 in";

        public static Unit GetWidth(Section section)
        {
            PageSetup.GetPageSize(section.PageSetup.PageFormat, out Unit pageWidth, out Unit _);
            return pageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
        }
    }
}
