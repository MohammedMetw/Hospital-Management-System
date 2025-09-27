using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.MedicineInventory.Command;
using Hospital.Application.Features.MedicineInventory.Queries;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]

public class MedicineController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet("GetAll")]
    //[Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetAllMedicines()
    {
        var medicines = await _mediator.Send(new GetAllMedicinesQuery());
        return Ok(medicines);
    }

    
    [HttpGet("Get-By-Id{id}")]
    //[Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetMedicineById(int id)
    {
        var medicine = await _mediator.Send(new GetMedicineByIdQuery { MedicineId = id });
        return Ok(medicine);
    }

    
    [HttpGet("{id}/history")]
    //[Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetMedicineHistory(int id)
    {
        var history = await _mediator.Send(new GetMedicineHistoryQuery { MedicineInventoryId = id });
        return Ok(history);
    }

    
    [HttpGet("search-By-Name")]
   // [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetMedicinesByName([FromQuery] string name)
    {
        var medicines = await _mediator.Send(new GetMedicinesByNameQuery { MedicineName = name });
        return Ok(medicines);
    }

   
    [HttpGet("Get-low-stock")]
   // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLowStockMedicines([FromQuery] int threshold = 20) // Default threshold is 20
    {
        var medicines = await _mediator.Send(new GetLowStockMedicinesQuery { lowStockThreshold = threshold });
        return Ok(medicines);
    }

  
    [HttpGet("Get-Expired")]
   // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetExpiredMedicines()
    {
        var medicines = await _mediator.Send(new GetExpiredMedicinesQuery());
        return Ok(medicines);
    }

    
    [HttpPost("add-stock")]
   // [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> AddMedicineToInventory([FromBody] AddMedicineToInventoryCommand command)
    {
        var resultDto = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMedicineById), new { id = resultDto.MedicineInventoryId }, resultDto);
    }

   
    [HttpPost("dispense")]
  //  [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> DispenseMedicine([FromBody] DispenseMedicineCommand command)
    {
        await _mediator.Send(command);
        return Ok("Medicine dispensed successfully.");
    }

  
    [HttpPut("update-stock/{id}")]
   // [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> UpdateMedicineStock(int id, [FromBody] UpdateMedicineStockCommand command)
    {
        if (id != command.MedicineId)
        {
            return BadRequest("ID mismatch.");
        }
        var resultDto = await _mediator.Send(command);
        return Ok(resultDto);
    }

    
    [HttpDelete("remove-from-stock")]
  //  [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMedicineFromInventory([FromBody] DeleteMedicineFromInventoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent(); 
    }


   

    [HttpGet("{id}/Getqrcode")]
   // [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetMedicineQrCode(int id)
    {
        var qrCodeBytes = await _mediator.Send(new GetMedicineQrCodeQuery { MedicineId = id });

        return File(qrCodeBytes, "image/png");
    }

    [HttpGet("qr-scan")]
   // [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetMedicineByQrCode([FromQuery] string qrData)
    {
        var medicine = await _mediator.Send(new GetMedicineByQrQuery { QrCodeData = qrData });
        return Ok(medicine);
    }
}