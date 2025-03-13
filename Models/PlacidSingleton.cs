namespace W1.Models
{

    public interface IScopedService
    {
        void PerformAction();
        void SetVal(bool val);

        bool GetVal( );

        string GetPlacidUser();

        void SetPlacidUser(string CurrentUser);
 
        bool GetPlacid();
         void SetPlacid(bool val);
        

    }

    public class ScopedService : IScopedService
    {
        private bool _b;
        private string _CurrentUser;
        private bool IsAuthenticated;
        public void PerformAction()
        {
            // Code to be executed within the scope
            Console.WriteLine("Scoped service action performed.");
        }

        public bool GetVal( )
        {
            return _b;
        }
 
        public void SetVal(bool b)
        {
            _b = b;
        }

        public string GetPlacidUser()
        {
            return _CurrentUser;
        }
        public void SetPlacidUser(string CurrentUser)
        {
            _CurrentUser = CurrentUser;
        }

        public bool GetPlacid()
        {
            return IsAuthenticated;
        }
        public void SetPlacid(bool val)
        {
            IsAuthenticated = val;
        }

    }



    public interface IPlacidService
    {
        string GetPlacidUser();

        void SetPlacidUser(string CurrentUser);

        bool GetPlacid();

        void SetPlacid(bool val);
        
    }



    public class PlacidSingleton : IScopedService
    {
        private static PlacidSingleton _instance;
        private string _CurrentUser;
        private bool _IsAuthenticated;

        private PlacidSingleton()
        {
        }

        public static PlacidSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlacidSingleton();

                return _instance;
            }
        }

        public void PerformAction()
        {
            // Code to be executed within the scope
            Console.WriteLine("Scoped service action performed.");
        }

        public bool GetVal()
        {
            return _IsAuthenticated;
        }

        public void SetVal(bool b)
        {
            _IsAuthenticated = b;
        }

        public string GetPlacidUser()
        {
            return _CurrentUser;
        }
        public void SetPlacidUser(string CurrentUser)
        {
            _CurrentUser = CurrentUser;
        }

        public bool GetPlacid()
        {
            return _IsAuthenticated;
        }
        public void SetPlacid(bool val)
        {
            _IsAuthenticated = val;
        }
    }
}
