namespace NZWalks.API.Entity_Models.Domain
{
    public class Walk
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public int DifficultyId { get; set; }
        public int RegionId { get; set; }


        //Navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }



    }
}
