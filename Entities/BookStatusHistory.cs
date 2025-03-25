using LibaryApi.Entities.Enum;

namespace LibaryApi.Entities
{
    public class BookStatusHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Status Status { get; set; }
        public DateTime ChangeDate { get; set; } = DateTime.Now;
    }
}
