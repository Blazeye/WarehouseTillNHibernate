using WarehouseTill.products;
using NHibernate.Cfg;
using NHibernate;

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
                configuration.Configure("C:/Users/StJac/source/repos/WarehouseTill2/WarehouseTill_C#/WarehouseTill/WarehouseTill/database/hibernate.cfg.xml");
                configuration.AddAssembly(typeof(Product).Assembly);
                SessionFactory = configuration.BuildSessionFactory();
            }
            return SessionFactory.OpenSession();
        }
    }
}