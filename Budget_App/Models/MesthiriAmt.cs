using System.ComponentModel.DataAnnotations;

namespace Budget_App.Models
{
    public class MesthiriAmt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Expense Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Expense Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Expense Ammount")]
        public double ExpenseAmt { get; set; }

        [Display(Name = "Spent Date")]
        [DataType(DataType.Date)]
        public DateTime SpentDate { get; set; } = DateTime.Now;
    }
}
