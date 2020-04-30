using System.Collections.Generic;
using DataAccessLayer.Models;
using Dapper;

namespace DataAccessLayer.Repository
{
    public class EprocRepository : SqlServerRepository, IEprocRepository
    {

        public IEnumerable<QuotationMaster> GetQuotationMasters(string collectiveNumber)
        {
            
            string command = $"Select QMS_QTN_STATUS as RFQStatus From T_QTNMAS where QMS_COLL_NO = @CollectiveNumber";
            return Database.Query<QuotationMaster>(command, new { CollectiveNumber = collectiveNumber });
        }
    }
}
