using AadeshPharmaWeb.Model;

namespace AadeshPharmaWeb.Interface
{
    public interface IAadeshPharma
    {
        bool addMedicine(Medicines medicine);
        Task<Medicines> getMedicineById(string id);
        Task<List<Medicines>> getAllMedicines();
        Task<List<Medicines>> searchMedicines(string name);

        bool upser(Medicines medicines);
        bool update(Medicines medicines);
        bool deleteMedecine(string id);


    }
}
