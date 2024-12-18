using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using MenuApp.Model;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace MenuApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuDbContext _context;

        public MenuItemsController(MenuDbContext context)
        {
            _context = context;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return menuItem;
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        //{
        //    _context.MenuItems.Add(menuItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        //}

        //[HttpPost]
        //public async Task<ActionResult<MenuItem>> PostMenuItem([FromBody] MenuItem requestBody)
        //{
        //    try
        //    {
        //        // Log the received data for debugging
        //        Console.WriteLine($"Item Name: {requestBody.ItemName}");
        //        Console.WriteLine($"Item Description: {requestBody.ItemDescription}");
        //        Console.WriteLine($"Category: {requestBody.Category.CategoryName}"); // Assuming you have a navigation property Category that includes CategoryName
        //        Console.WriteLine($"Item Price: {requestBody.ItemPrice}");

        //        // Process data (e.g., save to database)
        //        //var newMenuItem = new MenuItem
        //        //{
        //        //    ItemName = requestBody.ItemName,
        //        //    ItemDescription = requestBody.ItemDescription,
        //        //    Category = requestBody.Category, // Assuming Category is already retrieved from the CategoryId
        //        //    ItemPrice = requestBody.ItemPrice
        //        //};

        //        //_context.MenuItems.Add(newMenuItem);
        //        //await _context.SaveChangesAsync();

        //        return Ok(new { success = true, message = "Menu item added successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        Console.WriteLine(ex.Message);
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}

        // POST: api/MenuItems/getfrontdata
        [HttpPost("getfrontdata")]
        public async Task<IActionResult> getfrontdata(string ItemName, string ItemDescription, string CategoryName, decimal ItemPrice)
        {
            try
            {
                // Log the received data for debugging
                Console.WriteLine($"Item Name: {ItemName}");
                Console.WriteLine($"Item Description: {ItemDescription}");
                Console.WriteLine($"Category: {CategoryName}");
                Console.WriteLine($"Item Price: {ItemPrice}");

                // Since we are not interacting with a database in this method,
                // You may want to return a simple message or status code indicating the data was received.
                return Ok(new { Message = "Data received successfully" });
            }
            catch (Exception ex)
            {
                // Implement logging (e.g., using ILogger)
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }





        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
