#region EPP Plus
/*****************************OfficeOpenXml***********************************
 * Note: This class uses EPP Plus for Excel operations. Found in NuGet, to add
 * reference to the project, go to NuGet package manager command line:
 * PM> Install-Package EPPlus
 */
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml; //NuGet console>  Install-Package EPPlus v5.5
using OfficeOpenXml.Style;
using System.Drawing;
using Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Objects;

namespace Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Libraries
{
    public class ExcelProcessingLib
    {
        public void ExportReportToExcel(string filePath, List<string[]> headerData, string worksheetName, string cellData)
        {
            string[] bodyData = cellData.Split('\n');
            //Count the number of items in the header
            string rangeEnd = GetLetter(headerData[0].Length);

            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    excelPackage.Workbook.Worksheets.Add(worksheetName);

                    // Determine the header range (e.g. A1:D1)
                    string headerRange = string.Format($"A1:{Char.ConvertFromUtf32(headerData[0].Length + 64)}1");
                    string headerRange1 = "A1:" + Char.ConvertFromUtf32(headerData[0].Length + 64) + "1";
                    string entryData = string.Format($"A2:{rangeEnd}{bodyData.Length.ToString()}");
                    string entryTextRange = string.Format($"{rangeEnd}1:{rangeEnd}{bodyData.Length.ToString()}");

                    // Target a worksheet
                    var worksheet = excelPackage.Workbook.Worksheets[worksheetName];

                    // Populate header row data
                    using (ExcelRange rng = worksheet.Cells[headerRange])
                    {
                        //Set bold and line around cells
                        rng.LoadFromArrays(headerData);
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                        rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                    using (ExcelRange rng = worksheet.Cells[entryData])
                    {
                        //Set line around cells
                        rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    //This takes the cell range, starting from row 2 column 1
                    //The first row is the row header information
                    worksheet.Cells[2, 1].LoadFromText(cellData).AutoFitColumns();
                    FileInfo excelFile = new FileInfo(filePath);

                    //Save the changes
                    excelPackage.SaveAs(excelFile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the letter in the alphabet based on the number provided.
        /// This is used to get the range for Excel reporting
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public string GetCellDAta(List<WorkItemInstance> workItemsAndCommentsRecord, string[] fields)//, bool addComments)
        {
            string blah = String.Empty;
            string individualRecord = String.Empty;
            string stringList = String.Empty;

            foreach (var record in workItemsAndCommentsRecord)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < fields.Length; i++)
                {
                    stringBuilder.Append(record.AdoWorkItem.Fields[fields[i]]);
                    stringBuilder.Append(",");
                }
                //Remove the last comma
                stringBuilder.Length--;
                //add new line
                stringBuilder.AppendLine();

                individualRecord += stringBuilder.ToString();
            }

            return individualRecord;
        }

        public string GetLetter(int value)
        {
            char letter = (char)('A' - 1 + value);
            return letter.ToString();
        }
    }
}

