using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Budget_App.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Expense Name")]
        public string ExpenseName { get; set; }

        [Required]
        [Display(Name = "Expense Description")]
        public string ExpenseDescription { get; set; }

        [Required]
        [Display(Name = "Expense Ammount")]
        public double ExpenseAmt { get; set; }

        [Display(Name = "Spent Date")]
        [DataType(DataType.Date)]
        public DateTime  SpentDate{ get; set; }=DateTime.Now;
    }
}
