using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Station : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Route> RouteFinalStation { get; set; }
        public virtual ICollection<Route> RouteStartingStation { get; set; }
    }
}