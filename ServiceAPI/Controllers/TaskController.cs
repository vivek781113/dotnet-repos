using DataAccessLayer.Repository;
using System.Linq;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private readonly IEprocRepository _eprocRepository;
        private readonly EasyBuyRepository _easyBuyRepository;

        public TaskController()
        {
            _eprocRepository = _eprocRepository ?? new EprocRepository();
            _easyBuyRepository = _easyBuyRepository ?? new EasyBuyRepository();

        }

        //*Mark represnt optional parameter
        [HttpGet]
        [Route("getTaskDetails/{param1}/{*param2}")]
        public IHttpActionResult GetTaskDetails(int param1, string param2) => Ok($"TaskDetails + {param1} + {param2}");

        [HttpGet]
        [Route("getRfqStatusByPr/{prNumber}")]
        public IHttpActionResult GetRfqStatusByPr(string prNumber) => Ok(_eprocRepository.GetQuotationMasters(prNumber).ToList());

        [HttpGet]
        [Route("getCartDetails")]
        public IHttpActionResult GetCartDetails() => Ok(_easyBuyRepository.GetCartDetails().ToList());

    }
}