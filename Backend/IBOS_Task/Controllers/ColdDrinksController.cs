using IBOS_Task.Models;
using IBOS_Task.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IBOS_Task.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ColdDrinksController : ControllerBase
    {
        private readonly IRepository<ColdDrinks> _repo;

        public ColdDrinksController(IRepository<ColdDrinks> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.GetAll();
                return Ok(data);
            }
            return BadRequest();             
        }
        [HttpGet]
        [Route("{id:int:min(0)}")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Get(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound("No data found");
            }
            return BadRequest();
             
                
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromForm]ColdDrinks drinks)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.Insert(drinks);
                if (result != null)
                {
                    return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + drinks.ColdDrinksId, result);
                }
                return NoContent();
            }
            return BadRequest();
            
        }
        [HttpDelete]
        [Route("{id:int:min(0)}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.Get(id);
                if (data != null)
                {
                    await _repo.Delete(id);
                    return NoContent();
                }
                return NotFound("No data found");
            }
            return BadRequest();
            
        }
        [HttpPut]
        [Route("{id:int:min(0)}")]
        public async Task<IActionResult> Update(int id,[FromForm] ColdDrinks drinks)
        {
            if (ModelState.IsValid)
            {
                if (id != drinks.ColdDrinksId)
                {
                    return BadRequest("Id Not Matched");
                }
                var data = await _repo.Get(id);
                if (data != null)
                {
                    data.ColdDrinksName = drinks.ColdDrinksName;
                    data.Quantity = drinks.Quantity;
                    data.UnitPrice = drinks.UnitPrice;
                    var result = await _repo.Update(data);
                    return Ok(result);
                }
                return NotFound("Data Not Found");
            }
            return BadRequest();
            
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete()
        {
            if (ModelState.IsValid)
            {
                var data = await _repo.GetByQuantity(500);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        await _repo.Delete(item.ColdDrinksId);
                    }
                    return NoContent();
                }
                return NotFound("No data found");
            }
            return BadRequest();
            
        }
        [HttpGet]
        [Route("Total-Price")]
        public async Task<IActionResult> TotalPrice()
        {
            if (ModelState.IsValid)
            {
                int sum = 0;
                var data = await _repo.GetAll();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        sum += (item.Quantity * item.UnitPrice);
                    }
                    return Ok(sum);
                }
                return NotFound("No data found");
            }
            return BadRequest();
            
        }

    }
}
