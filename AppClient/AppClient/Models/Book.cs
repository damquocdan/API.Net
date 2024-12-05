namespace AppClient.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string BookCode { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Summary { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
