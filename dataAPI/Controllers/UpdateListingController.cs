using dataAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateListingController : ControllerBase
    {
        UpdateServices _updateSercies;
        public UpdateListingController(
            UpdateServices _svs
            )
        {
            _updateSercies = _svs;
        }
        [HttpGet("updateFun")]
        public void UpdateFun()
        {
            _updateSercies.ResidentialSaleUpdate();
        }
    }
}
