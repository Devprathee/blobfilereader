namespace BlobDataRead.Models
{

    public class Property
    {
        public string? PropertyId { get; set; }
        public string? PropertyName { get; set; }
        public List<string>? Features { get; set; }
        public List<string>? Highlights { get; set; }
        public List<Transportation>? Transportation { get; set; }
        public List<Space>? Spaces { get; set; }
    }

    public class Transportation
    {
        public string? Type { get; set; }
        public string? Line { get; set; }
        public string? Distance { get; set; }
    }

    public class Space
    {
        public string? SpaceId { get; set; }
        public string? SpaceName { get; set; }
        public List<RentRoll>? RentRoll { get; set; }
    }

    public class RentRoll
    {
        public string? Month { get; set; }
        public int? Rent { get; set; }
    }

}

