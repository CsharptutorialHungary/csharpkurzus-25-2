using System;
using System.Collections.Generic;
using System.Text;

namespace RoadMaintenanceApp.Models
{
    public record RoadCondition(int GoodPercent, int MidPercent, int BadPercent)
    {
        public bool IsValid() => (GoodPercent + MidPercent + BadPercent) == 100;
    }
}
