using Microsoft.AspNetCore.Mvc;
using AtlasAir.Models;
using AtlasAir.Interfaces;

namespace AtlasAir.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await customerRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,IsSpecialCustomer")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerRepository.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,IsSpecialCustomer")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await customerRepository.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerRepository.GetByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                await customerRepository.DeleteAsync(customer);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
