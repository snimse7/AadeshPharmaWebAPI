using AadeshPharmaWeb.Model;

namespace AadeshPharmaWeb.Interface
{
    public interface IAadeshPharma
    {
        Task<bool> addMedicine(Medicines medicine);
        Task<Medicines> getMedicineById(string id);
        Task<List<Medicines>> getAllMedicines();
        Task<List<Medicines>> searchMedicines(string name);

        
    }
}
