/*using OfficeOpenXml;
using OfficeOpenXml.Style;
using webAplication.DAL.models;
using webAplication.Domain.Persons;
using wepAplication;

namespace webAplication.Domain
{
    public class Report
    {

        public List<ReportSchoolKid> data { get; set; }
        public Report()
        {
            data = new List<ReportSchoolKid>();
        }

        public static void CreateExcel(Dictionary<TimeToService, List<Order>> orders, List<SchoolKid> schoolKids, List<Dish> dishes)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileStream fileStream = new FileStream("..\\webAplication\\Files\\report.xlsx", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

            using (var exel = new ExcelPackage(fileStream))
            {

                var sheet = exel.Workbook.Worksheets.Add("Отчет");

                string[] times = new string[] { "Завтрак", "Обед", "Ужин" };
                var j = 0;
                for (int i = 2; i < 13; i += 5)
                {
                    sheet.Cells[2, i].Value = $"Меню: {times[j]}";
                    sheet.Cells[3, i].Value = "Ученик";
                    sheet.Cells[3, i + 1].Value = "Заказ";
                    sheet.Cells[3, i + 2].Value = "Всего";

                    sheet.Cells[3, i + 2, 3, i + 3].Merge = true;
                    sheet.Cells[3, i + 2, 3, i + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Cells[2, i, 2, i + 1].Merge = true;
                    sheet.Cells[2, i, 2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    sheet.Cells[2, i, 3, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[2, i, 3, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[2, i, 3, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[2, i, 3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    sheet.Cells[3, i + 2,3, i+3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    //DishId:Amount
                    var dishAmount = new Dictionary<string, int>();
                    var curRow = 4;
                    foreach (var order in orders[(TimeToService)j])
                    {
                        var schoolkid = schoolKids.FirstOrDefault(x => x.Id == order.SchoolKidId);
                        if (schoolkid == null)
                        {
                            continue;
                        }
                        sheet.Cells[curRow, i].Value = schoolkid.name;

                        foreach (var dishId in order.DishIds)
                        {
                            var dish = dishes.FirstOrDefault(x => x.Id == dishId);
                            if (dish == null)
                            {
                                continue;
                            }

                            if (dishAmount.Keys.Contains(dishId))
                                dishAmount[dishId]++;
                            else dishAmount[dishId] = 1;

                            sheet.Cells[curRow, i + 1].Value = dish.title;
                            curRow++;
                        }
                        sheet.Cells[curRow, i, curRow, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    }

                    using (var range = sheet.Cells[2, i, curRow, i + 3])
                    {
                        sheet.Cells.AutoFitColumns(15);

                    }
                    sheet.Cells[2,i,curRow-1,i+1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[2, i+1, curRow-1, i+1].Style.Border.Left.Style = ExcelBorderStyle.Thin;


                    curRow = 4;
                    foreach (var dishId in dishAmount.Keys)
                    {
                        var dish = dishes.FirstOrDefault(x => x.Id == dishId);
                        if (dish == null)
                        {
                            continue;
                        }
                        sheet.Cells[curRow, i + 2].Value = dish.title;
                        sheet.Cells[curRow, i + 3].Value = dishAmount[dishId];
                        curRow++;
                    }
                    sheet.Cells[3, i + 2, curRow-1, i + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                    j++;
                }
                exel.Save();
            }
            fileStream.Close();
        }

        public void AddData(SchoolKid schoolKid, SchoolKidAttendanceType attendance, List<Order> menu)
        {
            if (attendance == SchoolKidAttendanceType.Present)
            {
                data.Add(new ReportSchoolKid(schoolKid, menu));
            }
        }
    }
}
*/