using System;
using System.Collections.Generic;
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
                        Name = ship.Name
                    };

                    db.SpaceShips.Add(dbs);
                    db.SaveChanges();
                }


                return View(ship);
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