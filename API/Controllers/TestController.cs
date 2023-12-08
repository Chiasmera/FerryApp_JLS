using Business_Logic;
using Data_Transfer_Objects.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// ONLY FOR TESTING. Manages test-data in the databse. For Claus' conveniece.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Clears the database, and then adds the default testdata. 
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public void Add()
        {
            FerryBL ferryBL = new FerryBL();
            ferryBL.ResetTestData();
            
        }
    }
}
