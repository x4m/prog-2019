using System;
using System.Collections.Generic;

namespace SpaceFleet
{
    /// <summary>
    /// Корабль
    /// </summary>
    public class SpaceShip
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата постройки
        /// </summary>
        public DateTime Build { get; set; }
        /// <summary>
        /// Техническое обсуживание
        /// </summary>
        public List<ServiceMaitanance> ServiceMaintanance {get;set;}
        /// <summary>
        /// Тип корабля
        /// </summary>
        public ShipType ShipType { get; set; }
        /// <summary>
        /// Бортовой журнал
        /// </summary>
        public List<Flight> Journal { get; set; }
        /// <summary>
        /// Фотография
        /// </summary>
        public byte[] Photo { get; set; }
    }

    /// <summary>
    /// Полёт
    /// </summary>
    public class Flight
    {
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

    /// <summary>
    /// Тип коробля
    /// </summary>
    public enum ShipType
    {
        /// <summary>
        /// Гражданский транспорт
        /// </summary>
        Civil,
        /// <summary>
        /// Военный корабль
        /// </summary>
        Military,
        /// <summary>
        /// Грузовой
        /// </summary>
        Cargo,
    }

    /// <summary>
    /// Сервисное обслуживание
    /// </summary>
    public class ServiceMaitanance
    {
        /// <summary>
        /// Станция сервисного обслуживания
        /// </summary>
        public string ServiceStationLocation { get; set; }
        /// <summary>
        /// Дата сервисного обслуживания
        /// </summary>
        public DateTime MaintananceDate { get; set; }

        /// <summary>
        /// Ожидаемая дата следующего обсуживания
        /// </summary>
        public DateTime? NextPlannedSerice { get; set; }

        /// <summary>
        /// Описание работ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Плановое обслуживание
        /// </summary>
        public bool IsPlannedService { get; set; }

        public override string ToString()
        {
            return $"{nameof(Description)}: {Description}, {nameof(IsPlannedService)}: {IsPlannedService}, {nameof(MaintananceDate)}: {MaintananceDate}, {nameof(NextPlannedSerice)}: {NextPlannedSerice}, {nameof(ServiceStationLocation)}: {ServiceStationLocation}";
        }
    }
}
