using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace SpaceFleet.Web.Models
{
    namespace DataAccessPostgreSqlProvider
    {
        // >dotnet ef migration add testMigration in AspNet5MultipleProject
        public class SpaceFleetDbContext : DbContext
        {
            public SpaceFleetDbContext()
            {
                Database.EnsureCreated();
            }

            public SpaceFleetDbContext(DbContextOptions<SpaceFleetDbContext> options) : base(options)
            {
            }

            public DbSet<DbSpaceShip> SpaceShips { get; set; }
            public DbSet<DbFlight> Flights { get; set; }
            public static string ConnectionString { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql(SpaceFleetDbContext.ConnectionString);
                
                base.OnConfiguring(optionsBuilder);
            }
        }

        /// <summary>
        /// Корабль
        /// </summary>
        public class DbSpaceShip
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            /// <summary>
            /// Название
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Дата постройки
            /// </summary>
            public DateTime Build { get; set; }
            /// <summary>
            /// Тип корабля
            /// </summary>
            public ShipType ShipType { get; set; }
            /// <summary>
            /// Бортовой журнал
            /// </summary>
            public virtual Collection<DbFlight> Journal { get; set; }
            /// <summary>
            /// Фотография
            /// </summary>
            public byte[] Photo { get; set; }
        }

        /// <summary>
        /// Полёт
        /// </summary>
        public class DbFlight
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int SpaceShipId { get; set; }
            [ForeignKey("SpaceShipId")]
            public virtual SpaceShip SpaceShip { get; set; }

            /// <summary>
            /// Откуда
            /// </summary>
            public string From { get; set; }
            /// <summary>
            /// Куда
            /// </summary>
            public string To { get; set; }
            /// <summary>
            /// Команда
            /// </summary>
            public string Crew { get; set; }
            /// <summary>
            /// Пассажиры
            /// </summary>
            public string Passengers { get; set; }

            public override string ToString()
            {
                return $"Откуда: {From}, Куда: {To}, Команда: {Crew}, Пассажиры: {Passengers}";
            }
        }
    }
}