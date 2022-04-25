using HospitalAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Repositories
{
    public interface IDoctorRepository
    {

        public Task<IEnumerable<Doctor>> Get();

        public Task<Doctor> Get(int id);

        public Task<Doctor> Create(Doctor doctor);

        public Task Update(Doctor doctor);

        public Task Delete(int id);

    }
}
