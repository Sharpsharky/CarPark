namespace CarPark.Contracts.Requests
{
    public class AllocateParkingRequest
    {
        public string VehicleReg { get; set; } = default!;
        public int VehicleType { get; set; } // 1/2/3
    }
}
