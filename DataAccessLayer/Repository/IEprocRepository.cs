using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IEprocRepository : IDisposable
    {
        IEnumerable<QuotationMaster> GetQuotationMasters(string collectiveNumber);
    }
}
