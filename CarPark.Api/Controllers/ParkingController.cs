using CarPark.Application.Parking.Commands.AllocateVehicle;
using CarPark.Application.Parking.Commands.ExitVehicle;
using CarPark.Application.Parking.Queries.GetCapacity;
using CarPark.Contracts.Requests;
using CarPark.Contracts.Responses;
using CarPark.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Api.Controllers
{
    [ApiController]
    [Route("parking")]
    public class ParkingController : ControllerBase
    {
        private readonly ISender _mediator;
        public ParkingController(ISender mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<AllocateParkingResponse>> Allocate([FromBody] AllocateParkingRequest req, CancellationToken ct)
        {
            var vehicleType = req.VehicleType switch
            {
                1 => VehicleType.Small,
                2 => VehicleType.Medium,
                3 => VehicleType.Large,
                _ => throw new ArgumentOutOfRangeException(nameof(req.VehicleType), "VehicleType must be 1, 2 or 3.")
            };

            var result = await _mediator.Send(new AllocateVehicleCommand(req.VehicleReg.Trim(), vehicleType), ct);

            return Ok(new AllocateParkingResponse
            {
                VehicleReg = result.VehicleReg,
                SpaceNumber = result.SpaceNumber,
                TimeIn = result.TimeInUtc
            });
        }

        [HttpGet]
        public async Task<ActionResult<GetCapacityResponse>> GetCapacity(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCapacityQuery(), ct);
            return Ok(new GetCapacityResponse
            {
                AvailableSpaces = result.AvailableSpaces,
                OccupiedSpaces = result.OccupiedSpaces
            });
        }

        [HttpPost("exit")]
        public async Task<ActionResult<ExitParkingResponse>> Exit([FromBody] ExitParkingRequest req, CancellationToken ct)
        {
            var result = await _mediator.Send(new ExitVehicleCommand(req.VehicleReg.Trim()), ct);
            return Ok(new ExitParkingResponse
            {
                VehicleReg = result.VehicleReg,
                VehicleCharge = (double)result.VehicleCharge,
                TimeIn = result.TimeInUtc,
                TimeOut = result.TimeOutUtc
            });
        }
    }
}
