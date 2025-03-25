namespace LibraryApi.Models
{
    public class BookAvailabilityDto
    {
        public bool IsAvailable { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
