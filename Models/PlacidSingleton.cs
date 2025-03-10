namespace W1.Models
{
    public class PlacidSingleton
    {
        private static readonly Lazy<PlacidSingleton> lazy =
        new Lazy<PlacidSingleton>(() => new PlacidSingleton());

        private bool Placid = false;

        public static PlacidSingleton Instance { get { return lazy.Value; } }

        private PlacidSingleton() { }

        public bool GetPlacid()
        {
            return Placid;
        }
        public void SetPlacid(bool val)
        {
            Placid = val;
        }
    }
}
