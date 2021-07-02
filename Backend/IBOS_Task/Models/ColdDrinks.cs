using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IBOS_Task.Models
{
    public class ColdDrinks
    {
        [Key]
        public int ColdDrinksId { get; set; }
        [Required(ErrorMessage ="Enter a name ")]
        public string ColdDrinksName { get; set; }
        [Required][Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int UnitPrice { get; set; }
    }
}
