namespace CarPark.Contracts.Responses
{
    public class ExitParkingResponse
    {
        public string VehicleReg { get; set; } = default!;
        public double VehicleCharge { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}