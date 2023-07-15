using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Budget_App.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [BsonElement("Expense Name")]
        public string ExpenseName { get; set; }

        [Required]
        [BsonElement("Expense Description")]
        public string ExpenseDescription { get; set; }

        [Required]
        [BsonElement("Expense Ammount")]
        public double ExpenseAmt { get; set; }

        [BsonElement("Spent Date")]
        [DataType(DataType.Date)]
        public DateTime  SpentDate{ get; set; }=DateTime.Now;
    }
}
