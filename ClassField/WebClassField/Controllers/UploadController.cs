using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassField;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WebClassField.Models;
using WebClassField.Models.DataAccessPostgreSqlProvider;

namespace WebClassField.Controllers
{
    public class UploadController : Controller
    {
        public static string ToStringJury(List<string> juri)
        {

            string stroka = "";
            foreach (var e in juri)
            {
                stroka = stroka + e;
                stroka += ", ";
            }

            return stroka;
        }
            public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var xs = new XmlSerializer(typeof(TableOfResult));
                var Table = (TableOfResult)xs.Deserialize(stream);


                using (var db = new WebClassFielddbContext())
                {
                    var dbs = new DbTableOfResult()
                    {
                        Competitions = Table.Competitions,
                        CategoryCompetition = Table.CategoryCompetition,
                        AgeСategory = Table.AgeСategory,
                        DateCompetition = Table.DateCompetition,
                        CityofComp = Table.CityofComp,
                    };
                    dbs.ListCompetitors = new List<DbCompetitor>();
                    foreach (var competitor in Table.ListCompetitors)
                    {
                        dbs.ListCompetitors.Add(new DbCompetitor()
                        {
                            Name = competitor.Name,
                            Score1 = competitor.Score1,
                            Score2 = competitor.Score2,
                            AllScore = competitor.AllScore,
                        });
                    }

                    db.TableOfResult.Add(dbs);
                    db.SaveChanges();
                }


                return View(Table);
            }
        }

 
        public ActionResult List()
        {
            List<DbTableOfResult> list;
            using (var db = new WebClassFielddbContext())
            {
                list = db.TableOfResult.Include(s => s.ListCompetitors).ToList();
            }

            return View(list);
        }


        public ActionResult Print(int id)
        {
            using (var db = new WebClassFielddbContext())
            {
                var Table = db.TableOfResult.Include(s1 => s1.ListCompetitors).First(s1 => s1.Id == id);
                IWorkbook workbook =
                    new XSSFWorkbook(System.IO.File.OpenRead("template.xlsx"));

                var sheet = workbook.GetSheetAt(0);

                sheet.GetRow(1).Cells[1].SetCellValue(Table.Competitions);

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    var lastCellNum = row.LastCellNum;
                    for (int j = row.FirstCellNum; j < lastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            if (cell.StringCellValue == "$PropRow")
                            {
                                cell.SetCellValue("Дата соревнований");
                                cell = row.GetCell(j + 1) ?? row.CreateCell(j + 1);
                                cell.SetCellValue(Table.DateCompetition);
                                cell.CellStyle.DataFormat = 14;

                                row = sheet.CopyRow(i, i + 1);
                                i++;
                                cell = row.GetCell(j) ?? row.CreateCell(j);
                                cell.SetCellValue("Категорий соревнований");
                                row.CreateCell(j + 1).SetCellValue(Table.CategoryCompetition); row = sheet.CreateRow(i++);

                                break;
                            }

                            if (cell.StringCellValue == "$TEST")
                            {
                                foreach (var competitor in Table.ListCompetitors)
                                {
                                    row = sheet.GetRow(i);
                                    cell = row.GetCell(j);
                                    cell.SetCellValue(competitor.Name);
                                    cell = row.GetCell(j + 1) ?? row.CreateCell(j + 1);
                                    cell.SetCellValue(competitor.Score1);
                                    cell = row.GetCell(j + 2) ?? row.CreateCell(j + 2);
                                    cell.SetCellValue(competitor.Score2);
                                    cell = row.GetCell(j + 3) ?? row.CreateCell(j + 3);
                                    cell.SetCellValue(competitor.AllScore);
                                    if (competitor != Table.ListCompetitors.Last())
                                        row = sheet.CopyRow(i, i + 1);
                                    i++;
                                }
                                break;
                            }
                        }
                    }
                }

                var ms = new MemoryStream();
                workbook.Write(ms);

                ms.Position = 0;

                return base.File(ms, "application/octet-stream", "Table" + id + ".xlsx");
            }
        }
    }
}