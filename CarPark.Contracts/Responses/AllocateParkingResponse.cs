namespace CarPark.Contracts.Responses
{
    public class AllocateParkingResponse
    {
        public string VehicleReg { get; set; } = default!;
        public int SpaceNumber { get; set; }
        public DateTime TimeIn { get; set; }
    }
}