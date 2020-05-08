using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var reportService = new Reporting();

            var path = @"C:\Users\rahum\" + Guid.NewGuid().ToString() + ".pdf";

            reportService.Export(path);

            Process.Start(path);
        }
    }
}
