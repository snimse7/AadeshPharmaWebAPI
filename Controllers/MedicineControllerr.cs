using AadeshPharmaWeb.Interface;

using AadeshPharmaWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;


using System.IdentityModel.Tokens.Jwt;
using AuthorizeAttribute = WebApi.Helpers.AuthorizeAttribute;

namespace AadeshPharmaWeb.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineControllerr : ControllerBase
    {
       
        
  
        private readonly IAadeshPharma _aadeshPharma;
        public MedicineControllerr(IAadeshPharma aadeshPharma)
        { _aadeshPharma = aadeshPharma;
            

        }

        [Authorize]
        [HttpPost]
        [Route("/AddMedicine")]
        public  bool addMedicine(Medicines medicines)
        {
            try
            {
                return  _aadeshPharma.addMedicine(medicines);
            }
            catch  { throw; }
        }

        [HttpGet]
        [Route("/GetMedicineById")]
        public async Task<Medicines> getMdecineById(string id)
        {
            try
            {
                return await _aadeshPharma.getMedicineById(id);
            }
            catch { throw; }
        }

        [HttpGet]
        [Route("/GetAllMedicines")]
        public async Task<List<Medicines>> getAllMedicines()
        {
            try
            {
                return await _aadeshPharma.getAllMedicines();
            }
            catch { throw; }
        }

        [HttpGet]
        [Route("/SearchMedicines")]
        public async Task<List<Medicines>> searchMedicines(string name)
        {
            try
            {
                return await _aadeshPharma.searchMedicines(name);
            }
            catch { throw; }
        }

        [Authorize]
        [HttpPost]
        [Route("/UpsertMedicine")]
        public IActionResult upsertMedicine(Medicines medicines)
        {
            try
            {
                return Ok(_aadeshPharma.upser(medicines));
            }
            catch { throw; }
        }

        [Authorize]
        [HttpPut]
        [Route("/UpdateMedicine")]
        public IActionResult updateMedicine(Medicines medicines)
        {
            try
            {
                return Ok(_aadeshPharma.update(medicines));
            }
            catch { throw; }
        }

        [Authorize]
        [HttpDelete]
        [Route("/DeleteMedicine")]
        public IActionResult deleteMedicine(string id)
        {
            try
            {
                return Ok(_aadeshPharma.deleteMedecine(id));
            }
            catch { throw; }
        }
    }
}
