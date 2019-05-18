using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceFleet.Web.Models.DataAccessPostgreSqlProvider;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SpaceFleet.Web.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var xs = new XmlSerializer(typeof(SpaceShip));
                var ship = (SpaceShip) xs.Deserialize(stream);


                using (var db = new SpaceFleetDbContext())
                {
                    var dbs = new DbSpaceShip()
                    {
                        Name = ship.Name,
                        Build = ship.Build,
                        Photo = ship.Photo,
                    };
                    dbs.Journal = new Collection<DbFlight>();
                    foreach (var flight in ship.Journal)
                    {
                        dbs.Journal.Add(new DbFlight()
                        {
                            Crew = flight.Crew,
                            From = flight.From,
                            Passengers = flight.Passengers,
                            To = flight.To
                        });
                    }

                    db.SpaceShips.Add(dbs);
                    db.SaveChanges();
                }


                return View(ship);
            }
        }

        public ActionResult Image(int id)
        {
            using (var db = new SpaceFleetDbContext())
            {
                return base.File(db.SpaceShips.Find(id).Photo, "image/jpeg");
            }
        }

        public ActionResult List()
        {
            List<DbSpaceShip> list;
            using (var db = new SpaceFleetDbContext())
            {
                list = db.SpaceShips.Include(s=>s.Journal).ToList();
            }

            return View(list);
        }


        public ActionResult Print(int id)
        {
            using (var db = new SpaceFleetDbContext())
            {
                var ship = db.SpaceShips.Include(s1=>s1.Journal).First(s1=>s1.Id == id);
                IWorkbook workbook =
                    new XSSFWorkbook(System.IO.File.OpenRead("template.xlsx"));

                var sheet = workbook.GetSheetAt(0);

                sheet.GetRow(1).Cells[1].SetCellValue(ship.Name);
                
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
                                cell.SetCellValue("Дата постройки");
                                cell = row.GetCell(j+1) ?? row.CreateCell(j + 1);
                                cell.SetCellValue(ship.Build);
                                cell.CellStyle.DataFormat = 14;

                                row = sheet.CopyRow(i ,i+1);
                                i++;
                                cell = row.GetCell(j)??row.CreateCell(j);
                                cell.SetCellValue("Тип корабля");
                                row.CreateCell(j + 1).SetCellValue(ship.ShipType.ToString());row = sheet.CreateRow(i++);
                                
                                break;
                            }

                            if (cell.StringCellValue == "$Flight")
                            {
                                foreach (var flight in ship.Journal)
                                {
                                    row = sheet.GetRow(i);
                                    cell = row.GetCell(j);
                                    cell.SetCellValue(flight.Crew);
                                    cell = row.GetCell(j + 1) ?? row.CreateCell(j + 1);
                                    cell.SetCellValue(flight.From);
                                    cell = row.GetCell(j + 2) ?? row.CreateCell(j + 2);
                                    cell.SetCellValue(flight.To);
                                    cell = row.GetCell(j + 3) ?? row.CreateCell(j + 3);
                                    cell.SetCellValue(flight.Passengers);
                                    if (flight != ship.Journal.Last())
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

                return base.File(ms, "application/octet-stream", "ship" + id + ".xlsx");
            }
        }
    }
}