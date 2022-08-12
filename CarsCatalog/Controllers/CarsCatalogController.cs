using CarsCatalog.Api.Models;
using CarsCatalog.Args;
using CarsCatalog.Managers;
using CarsCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarsCatalogController : ControllerBase
    {
        private readonly ICarsCatalogManager _carsCatalogManager;

        public CarsCatalogController(ICarsCatalogManager carsCatalogManager)
        {
            _carsCatalogManager = carsCatalogManager;
        }

        [HttpGet]
        [Route("GetCars")]
        public async Task<IActionResult> GetCarsAsync([FromQuery]CarSearchArgs? args)
        {
            var carsList = await _carsCatalogManager.GetCarsAsync(args);
            return carsList != null ? Ok(carsList) : BadRequest();
        }

        [HttpPost, Authorize(Roles = "Administrator, PowerUser")]
        [Route("AddCar")]
        public async Task<IActionResult> AddCarAsync([FromQuery] CarDto car)
        {
            await _carsCatalogManager.AddCarAsync(car);
            return Ok();
        }

        [HttpDelete, Authorize(Roles = "Administrator, PowerUser")]
        [Route("RemoveCar")]
        public async Task<IActionResult> RemoveCarAsync([FromQuery] int carId)
        {
            await _carsCatalogManager.RemoveCarAsync(carId);
            return Ok();
        }

        [HttpPut, Authorize(Roles = "Administrator, PowerUser")]
        [Route("UpdateCar")]
        public async Task<IActionResult> UpdateCarAsync([FromQuery] Car car)
        {
            await _carsCatalogManager.UpdateCarAsync(car);
            return Ok();
        }
    }
}
