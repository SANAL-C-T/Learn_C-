﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuApp.Model;

namespace MenuApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly MenuDbContext _context;

        public AdminsController(MenuDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            return await _context.Admin.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.Id)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Admins/Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] Admin admin)
        {
            var user = await _context.Admin
                .FirstOrDefaultAsync(u => u.UserName == admin.UserName);

            if (user != null)
            {
                Console.WriteLine($"Received login attempt: Username={user.UserName}, PasswordHash={user.PasswordHash}");
            }

            if (user == null || user.PasswordHash != admin.PasswordHash)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // If login is successful, you could store user information in the session or cookie here.
            // Example: HttpContext.Session.SetString("UserId", user.Id.ToString());

            return Ok("Login successful");
        }

        // POST: api/Admins/Logout
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Clear user session or cookie to log out
            HttpContext.Response.Headers.Remove("Authorization");
            return Ok("Successfully logged out.");
        }






        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.Id == id);
        }
    }
}
