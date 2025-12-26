using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadMaintenanceApp.Models
{
    [Table("Streats")]
    public class Street
    {
        [Key]
        [MaxLength(64)]
        public string Id { get; set; } = string.Empty;

        public int GoodConditionPercent { get; set; }
        public int MidConditionPercent { get; set; }
        public int BadConditionPercent { get; set; }

        public int LengthKm { get; set; }

        public int TraficLevel { get; set; }
    }
}
