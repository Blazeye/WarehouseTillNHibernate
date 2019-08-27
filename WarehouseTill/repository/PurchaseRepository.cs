using System;
using WarehouseTill.database;
using WarehouseTill.products;


namespace WarehouseTill.repository
{
    class PurchaseRepository : IPurchaseRepository
    {
        public void Add(Product product)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Save(product);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw ex;
                    }
                }
            }
        }


        public Product GetById(int productId)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                return session.Get<Product>(productId);
            }
        }

        public void Remove(Product product)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Delete(product);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Update(Product product)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Update(product);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
