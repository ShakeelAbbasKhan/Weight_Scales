namespace Weight_Scales.dto.response
{
    public class WeightRecord
    {
        public string? Date { get; set; }
        public string? Vehicle_No { get; set; }
        public string? Reference_No { get; set; }
        public string? STATION_NAME { get; set; }
        public int TOTAL_WEIGHT { get; set; }
        public int TOTAL_WEIGHT_LIMIT { get; set; }
        public int STATION_ID { get; set; }
        public int LANE_ID { get; set; }
        public string? MOTORWAY_NAME { get; set; }
        public string? E_VEH_SORT { get; set; }
    }
}
