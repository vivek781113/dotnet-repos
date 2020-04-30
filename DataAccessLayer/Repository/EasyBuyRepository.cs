using System;
using System.Collections.Generic;
using Dapper;
using DataAccessLayer.Models;
using DataAccessLayer.Queries;

namespace DataAccessLayer.Repository
{
    public class EasyBuyRepository : OracleServerRepository, IEasyBuyRepository
    {
        public IEnumerable<CartDetail> GetCartDetails()
        {
            return Database.Query<CartDetail>(EasyBuyQueries.CART_DETAILS);
        }
    }
}
