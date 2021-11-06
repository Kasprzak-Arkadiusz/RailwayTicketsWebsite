using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Station : BaseEntity
    {
        public Station()
        {
            RouteFinalStationNavigations = new HashSet<Route>();
            RouteStartingStationNavigations = new HashSet<Route>();
        }

        public string Name { get; set; }

        public virtual ICollection<Route> RouteFinalStationNavigations { get; set; }
        public virtual ICollection<Route> RouteStartingStationNavigations { get; set; }
    }
}