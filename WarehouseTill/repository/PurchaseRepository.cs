using System;
using WarehouseTill.database;
using WarehouseTill.products;
using WarehouseTill.model;


namespace WarehouseTill.repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public void Add(Purchase purchase)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Save(purchase);
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
        public Purchase GetById(int purchaseId)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        return session.Get<Purchase>(purchaseId);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw ex;
                    }
                }
            }
            //using (var session = NHibernateSession.OpenSession())
            //{
            //    return session.Get<Purchase>(purchaseId);
            //}
        }
        public void Remove(Purchase purchase)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Delete(purchase);
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
        public void Update(Purchase purchase)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Update(purchase);
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
