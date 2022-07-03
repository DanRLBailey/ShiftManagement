using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shift_Management.Models;

namespace Shift_Management.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeShiftController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeShiftController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeShift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWorksShiftModel>>> GetEmployee_Works_Shift()
        {
          if (_context.Employee_Works_Shift == null)
          {
              return NotFound();
          }
            return await _context.Employee_Works_Shift.ToListAsync();
        }

        // GET: api/EmployeeShift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWorksShiftModel>> GetEmployeeWorksShiftModel(int id)
        {
          if (_context.Employee_Works_Shift == null)
          {
              return NotFound();
          }
            var employeeWorksShiftModel = await _context.Employee_Works_Shift.FindAsync(id);

            if (employeeWorksShiftModel == null)
            {
                return NotFound();
            }

            return employeeWorksShiftModel;
        }

        // PUT: api/EmployeeShift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeWorksShiftModel(int id, EmployeeWorksShiftModel employeeWorksShiftModel)
        {
            if (id != employeeWorksShiftModel.Employee_ID)
            {
                return BadRequest();
            }

            _context.Entry(employeeWorksShiftModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeWorksShiftModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeShift
        [HttpPost]
        public async Task<ActionResult<EmployeeWorksShiftModel>> PostEmployeeWorksShiftModel(EmployeeWorksShiftModel employeeWorksShiftModel)
        {
          if (_context.Employee_Works_Shift == null)
          {
              return Problem("Entity set 'EmployeeContext.Employee_Works_Shift'  is null.");
          }
            _context.Employee_Works_Shift.Add(employeeWorksShiftModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeWorksShiftModel", new { id = employeeWorksShiftModel.Employee_ID }, employeeWorksShiftModel);
        }

        // DELETE: api/EmployeeShift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeWorksShiftModel(int id)
        {
            if (_context.Employee_Works_Shift == null)
            {
                return NotFound();
            }
            var employeeWorksShiftModel = await _context.Employee_Works_Shift.FindAsync(id);
            if (employeeWorksShiftModel == null)
            {
                return NotFound();
            }

            _context.Employee_Works_Shift.Remove(employeeWorksShiftModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeWorksShiftModelExists(int id)
        {
            return (_context.Employee_Works_Shift?.Any(e => e.Employee_ID == id)).GetValueOrDefault();
        }
    }
}
