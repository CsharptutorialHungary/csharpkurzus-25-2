using System;
using System.Collections.Generic;
using System.Text;
using RoadMaintenanceApp.Data;
using RoadMaintenanceApp.Models;
using System.Text.Json;

namespace RoadMaintenanceApp.Services
{
    public class RoadService
    {
        private readonly AppDbContext _context;

        public RoadService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void AddRoad(string id, RoadCondition condition, int length, int traffic)
        {
            if (!condition.IsValid())
                throw new ArgumentException("A kondíciók összegének pontosan 100-nak kell lennie! ");

            if (traffic > 10) throw new ArgumentException("A forgalom szintje max 10 lehet!");

            var street = new Street
            {
                Id = id,
                GoodConditionPercent = condition.GoodPercent,
                MidConditionPercent = condition.MidPercent,
                BadConditionPercent = condition.BadPercent,
                LengthKm = length,
                TraficLevel = traffic
            };

            _context.Streets.Add(street);
            _context.SaveChanges();
        }

        public void UpdateRoad(string id, RoadCondition condition, int? length, int? traffic)
        {
            var street = _context.Streets.FirstOrDefault(s => s.Id == id);
            if (street == null) throw new KeyNotFoundException("Nincs ilyen azonosítójú út.");

            if (!condition.IsValid())
                throw new ArgumentException("A kondíciók összegének pontosan 100-nak kell lennie! ");

            street.GoodConditionPercent = condition.GoodPercent;
            street.MidConditionPercent = condition.MidPercent;
            street.BadConditionPercent = condition.BadPercent;

            if (length.HasValue) street.LengthKm = length.Value;
            if (traffic.HasValue)
            {
                if (traffic.Value > 10) throw new ArgumentException("Max traffic level: 10");
                street.TraficLevel = traffic.Value;
            }

            _context.SaveChanges();
        }

        public void DeleteRoad(string id)
        {
            var street = _context.Streets.FirstOrDefault(s => s.Id == id);
            if (street != null)
            {
                _context.Streets.Remove(street);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Nem található törlendő elem.");
            }
        }

        public List<Street> Search(string? id, int? traffic, int? length, int? goodPct, int? midPct, int? badPct)
        {
            IQueryable<Street> query = _context.Streets;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(s => s.Id.Contains(id));

            if (traffic.HasValue)
                query = query.Where(s => s.TraficLevel == traffic.Value);

            if (length.HasValue)
                query = query.Where(s => s.LengthKm == length.Value);

            if (goodPct.HasValue)
                query = query.Where(s => s.GoodConditionPercent == goodPct.Value);

            if (midPct.HasValue)
                query = query.Where(s => s.MidConditionPercent == midPct.Value);

            if (badPct.HasValue)
                query = query.Where(s => s.BadConditionPercent == badPct.Value);


            return query.ToList();
        }

        public void ExportIdsToJson(List<Street> streets, string filePath)
        {
            var ids = streets.Select(s => s.Id).ToList();
            var json = JsonSerializer.Serialize(ids, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public List<Street> GetAll() => _context.Streets.ToList();
    }
}
