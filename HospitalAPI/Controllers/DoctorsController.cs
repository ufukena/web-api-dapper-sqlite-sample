using HospitalAPI.Models;
using HospitalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IDoctorRepository _doctorRepository;


        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {

            return await _doctorRepository.Get();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctors(int id)
        {

            return await _doctorRepository.Get(id);
        }


        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor([FromBody] Doctor doctor)
        {

            var newDoctor = await _doctorRepository.Create(doctor);

            return CreatedAtAction(nameof(GetDoctors), new { id = newDoctor.Id }, newDoctor);
        }


        [HttpPut]
        public async Task<ActionResult> PutDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            await _doctorRepository.Update(doctor);

            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var doctorDelete = await _doctorRepository.Get(id);

            if (doctorDelete == null)
            {
                return NotFound();
            }

            await _doctorRepository.Delete(doctorDelete.Id);
            return NoContent();
        }


    }

}
