using Stripe.Terminal;

namespace BlossmAPI.Models.Geocode
{
    public class Geometry
    {
        public Location location { get; set; }
        public object boundary { get; set; }
    }
}
