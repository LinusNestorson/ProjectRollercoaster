namespace ProjectRollercoaster.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectRollercoaster.Server.Data;
    using ProjectRollercoaster.Shared;

    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        //private readonly DataContext _context;

        //public UnitController(DataContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetUnits()
        //{
        //    var unitTest = await _context.UnitTests.ToListAsync();
        //    return Ok(unitTest);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddUnits(UnitTest unit)
        //{
        //    _context.UnitTests.Add(unit);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.UnitTests.ToListAsync());
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUnits(int id, UnitTest unit)
        //{
        //    var dbunit = await _context.UnitTests.FirstOrDefaultAsync(U => U.Id == id);
        //    if (dbunit == null)
        //    {
        //        return NotFound("unit with the given ID doesn't exist.");
        //    }
        //    dbunit.Id = unit.Id;
        //    dbunit.Title = unit.Title;
        //    dbunit.Hp = unit.Hp;
        //    dbunit.Attack = unit.Attack;

        //    await _context.SaveChangesAsync();

        //    return Ok(dbunit);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUnits(int id)
        //{
        //    var dbunit = await _context.UnitTests.FirstOrDefaultAsync(U => U.Id == id);
        //    if (dbunit == null)
        //    {
        //        return NotFound("unit with the given ID doesn't exist.");
        //    }

        //    _context.UnitTests.Remove(dbunit);

        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.UnitTests.ToListAsync());
        //}
    }
}