namespace CarPark.Contracts.Responses
{
    public class GetCapacityResponse
    {
        public int AvailableSpaces { get; set; }
        public int OccupiedSpaces { get; set; }
    }
}