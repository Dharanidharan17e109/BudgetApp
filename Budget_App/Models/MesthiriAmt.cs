using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Budget_App.Models
{
    public class MesthiriAmt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; }

        [Required]
        [BsonElement("Expense Title")]
        public string Title { get; set; }

        [Required]
        [BsonElement("Expense Description")]
        public string Description { get; set; }

        [Required]
        [BsonElement("Expense Amt")]
        public double ExpenseAmt { get; set; }

        [BsonElement("Spent Date")]
        [DataType(DataType.Date)]
        public DateTime SpentDate { get; set; } = DateTime.Now;
    }
}
