using AadeshPharmaWeb.Interface;
using AadeshPharmaWeb.JWt;
using AadeshPharmaWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static AadeshPharmaWeb.Model.Userr;
using System.IdentityModel.Tokens.Jwt;

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

        [HttpPost]
        
        [Route("/AddMedicine")]
        public async Task<bool> addMedicine(Medicines medicines)
        {
            try
            {
                return await _aadeshPharma.addMedicine(medicines);
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

        
    }
}
