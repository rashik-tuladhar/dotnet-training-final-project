using Asp.Versioning;
using Hrs.Api.Business.HotelBusiness;
using Hrs.Api.Repository.HotelRepository;
using Hrs.Api.Shared;
using Hrs.Api.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Hrs.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/hotel")]
public class HotelsController(IHotelBusiness hotelBusiness) : ControllerBase
{
    [HttpGet]
    [Route("get-hotel-list")]
    public async Task<IActionResult> GetList()
    {
        var listDetails = await hotelBusiness.GetHotelsAsync();
        return Ok(listDetails);
    }
    
    [HttpGet]
    [Route("get-hotel-details/{hotelId}")]
    public async Task<IActionResult> GetDetails([FromQuery] int hotelId)
    {
        var itemDetails = await hotelBusiness.GetHotelByIdAsync(hotelId);
        return Ok(itemDetails);
    }
    
    [HttpPost]
    [Route("create-hotel")]
    public async Task<IActionResult> GetDetails([FromBody] CreateHotelDto createHotelDto)
    {
        var createResponse = await hotelBusiness.CreateHotelAsync(createHotelDto);
        return Ok(createResponse);
    }
    
    [HttpPut]
    [Route("update-hotel")]
    public async Task<IActionResult> GetDetails([FromBody] UpdateHotelDto updateHotelDto)
    {
        var updateResponse = await hotelBusiness.UpdateHotelAsync(updateHotelDto);
        return Ok(updateResponse);
    }
}