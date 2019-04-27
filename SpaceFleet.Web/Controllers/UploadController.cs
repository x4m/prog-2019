using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceFleet.Web.Models.DataAccessPostgreSqlProvider;

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
                var ship = (SpaceShip)xs.Deserialize(stream);

                
                
                
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
                list = db.SpaceShips.ToList();
            }

            return View(list);
        }
    }
}