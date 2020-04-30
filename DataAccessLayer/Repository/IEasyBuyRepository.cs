using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IEasyBuyRepository
    {
        IEnumerable<CartDetail> GetCartDetails();
    }
}
