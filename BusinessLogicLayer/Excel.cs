﻿using System;
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

           
            //workSheet.Cells[1, "A"] = "Firstname";
            //workSheet.Cells[1, "B"] = "Lastname";

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
                
                //foreach(var question in user.ongoingTest.test.questionList)
                //{
                //    workSheet.Cells[row++, column] = question._questionText;

                //    foreach(var answerList in user.ongoingTest._answers)
                //    {
                //        foreach(var answer in answerList)
                //        {
                //            workSheet.Cells[row++, column] = answer;
                           
                //        }
                        
                //    }

                //}
               

                column++;
            }

            //workSheet.Columns[1].AutoFit();
            //workSheet.Columns[2].AutoFit();

            workSheet.Range["A1", "B3"].AutoFormat(
                Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

        }
    }
}