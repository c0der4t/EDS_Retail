using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using FastReport;
using FastReport.Data;
using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Format;
using FastReport.Table;
using FastReport.Utils;

namespace FastReportTest
{
    internal class CustomReports
    {
        public Report TestReport()
        {
            Report report = new Report();
            ReportPage page = new ReportPage();

            report.Pages.Add(page);
            page.CreateUniqueName();

            page.ReportTitle = new ReportTitleBand();
            page.ReportTitle.Height = Units.Centimeters * 4;
            page.ReportTitle.CreateUniqueName();

            TextObject titleText = new TextObject();
            titleText.Parent = page.ReportTitle;
            titleText.CreateUniqueName();
            titleText.Bounds = new RectangleF(Units.Centimeters * 5, 0, Units.Centimeters * 10, Units.Centimeters * 1);
            titleText.Font = new Font("Arial", 14, FontStyle.Bold);
            titleText.Text = "EDS Retail Test Report";
            titleText.HorzAlign = HorzAlign.Center;

            return report;



        }


        public Report LoadReportFromFile(string pathToReportFile)
        {
            Report report = new Report();
            string reportContent = File.ReadAllText(pathToReportFile);

            report.Load(pathToReportFile);

            return report;
        }

        public string GenerateReport(Report reportToGenerate, bool saveToFile = true, bool openAfterSave = false, string saveDirLocation = @"c:\temp")
        {
            reportToGenerate.Prepare();
            reportToGenerate.SavePrepared($@"{saveDirLocation}\export.fpx");
            HTMLExport htmlReport = new HTMLExport();
            reportToGenerate.Export(htmlReport, $@"{saveDirLocation}\export.html");

            if (saveToFile)
            {
                if (!Directory.Exists(saveDirLocation))
                {
                    throw new DirectoryNotFoundException();
                }
            }
            else
            {
               

                if (openAfterSave)
                {
                    Process.Start($@"{saveDirLocation}\export.html");
                }
            }
            return $@"Created report export: {saveDirLocation}\export.html";
        }

    }
}
