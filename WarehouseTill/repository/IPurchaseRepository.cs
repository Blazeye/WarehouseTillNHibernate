using System;
using WarehouseTill.model;

namespace WarehouseTill.repository
{
    public interface IPurchaseRepository
    {
        void Add(Purchase purchase);
        void Update(Purchase purchase);
        Purchase GetById(int purchaseId);
    }
}
