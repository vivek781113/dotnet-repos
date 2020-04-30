using DataAccessLayer.Repository;
using System.Linq;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : BaseApiController
    {
        
        public TaskController(IEprocRepository eprocRepository, IEasyBuyRepository easyBuyRepository) 
            : base(eprocRepository, easyBuyRepository)
        {
        }

        //*Mark represnt optional parameter
        [HttpGet]
        [Route("getTaskDetails/{param1}/{*param2}")]
        public IHttpActionResult GetTaskDetails(int param1, string param2) => Ok($"TaskDetails + {param1} + {param2}");

        [HttpGet]
        [Route("getRfqStatusByPr/{prNumber}")]
        public IHttpActionResult GetRfqStatusByPr(string prNumber) => Ok(EprocRepo.GetQuotationMasters(prNumber).ToList());

        [HttpGet]
        [Route("getCartDetails")]
        public IHttpActionResult GetCartDetails() => Ok(EasyBuyRepo.GetCartDetails().ToList());

    }
}