




using Microsoft.EntityFrameworkCore;
using W1.Data;

namespace W1.Models
{
  
    public sealed class SingletonClass
    {
         
        private static SingletonClass instance = null;
        private static readonly object padlock = new object();
 
        


        SingletonClass( )
        {
            
             
        }

        public static SingletonClass Instance
        {
            
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonClass( );
                    }
                    return instance;
                }
            }
        }



         
        public void GetforSale() {

            



        }


    }
}
