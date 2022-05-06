using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace BusinessLogicLayer
{
    public static class Excel
    {
       public static void DisplayInExcel(UserGroup userGroup)
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

            var row = 1 ;
            var column = 1;
            foreach (var user in userGroup.Users)
            {
                row = 1;

                workSheet.Cells[row++, column] = user.FirstName;
                workSheet.Cells[row++, column] = user.LastName;
                workSheet.Cells[row++, column] = user.ongoingTest.test.name;

                for(int i=0;i< user.ongoingTest.test.questionList.Count;++i)
                {
                    workSheet.Cells[row++, column] = user.ongoingTest.test.questionList[i]._questionText;
                   foreach(var answer in user.ongoingTest._answers[i])
                    {
                        workSheet.Cells[row++, column] = answer;
                    }
                }
                column++;
            }

            workSheet.Range["A1", "B3"].AutoFormat(
            Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
        }
    }
}
