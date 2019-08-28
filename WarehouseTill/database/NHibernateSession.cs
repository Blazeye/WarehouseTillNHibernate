using System.Reflection;
using NHibernate.Cfg;
using NHibernate;
using System;

namespace WarehouseTill.database
{
    class NHibernateSession
    {
        private static ISessionFactory SessionFactory { get; set; } = null;
        public static ISession OpenSession()
        {
            if (SessionFactory == null)
            {
                var configuration = new Configuration();

                //GetExecutingAssembly gets the part of the assembly that contains the .NET code that will
                //will be executing, so not the metadata.
                //CodeBase gets the location of that part of the executing assembly's code.
                string codeBase = Assembly.GetExecutingAssembly().CodeBase; 
                //creates an object representation of a uri based on the location
                //uri 
                UriBuilder uri = new UriBuilder(codeBase);
                //returns the uri path as string without the escape sequences
                //path without unescape: file%3A///localhost/c%24/Windows/foo.txt
                //path with unescape example: file:///localhost/c$/Windows/foo.txt
                string path = Uri.UnescapeDataString(uri.Path);

                configuration.Configure(path + "#/WarehouseTill/WarehouseTill/database/hibernate.cfg.xml");
                
                configuration.AddAssembly("WarehouseTill");
                SessionFactory = configuration.BuildSessionFactory();
            }
            return SessionFactory.OpenSession();
        }
    }
}