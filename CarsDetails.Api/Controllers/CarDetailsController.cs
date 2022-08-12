using CarsDetails.Api.Managers;
using CarsDetails.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarsDetails.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly ICarsDetailsManager _carsDetailsManager;

        public CarDetailsController(ICarsDetailsManager carsDetailsManager)
        {
            _carsDetailsManager = carsDetailsManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarDetailsAsync(int carId)
        {
            var car = await _carsDetailsManager.GetCarDetailsAsync(carId);
            return car != null ? Ok(car) : BadRequest();
        }

        [HttpPut(), Authorize(Roles = "Administrator, PowerUser")]
        public async Task<IActionResult> UpdateCarDetailsAsync([FromBody] CarDto car)
        {
            await _carsDetailsManager.UpdateCarDetailsAsync(car);
            return Ok();
        }
    }
}
