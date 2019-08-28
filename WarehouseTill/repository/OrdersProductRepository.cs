using System;
using WarehouseTill.database;
using WarehouseTill.model;

namespace WarehouseTill.repository
{
    public class OrdersProductRepository : IOrdersProductRepository
    {
        public void Add(OrdersProduct ordersProduct)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Save(ordersProduct);
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
        public void Remove(OrdersProduct ordersProduct)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Delete(ordersProduct);
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
        public void Update(OrdersProduct ordersProduct)
        {
            using (var session = NHibernateSession.OpenSession())
            {
                //session.Save(product); // zonder transactions
                using (var tx = session.BeginTransaction()) // met transactions
                {
                    try
                    {
                        session.Update(ordersProduct);
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
