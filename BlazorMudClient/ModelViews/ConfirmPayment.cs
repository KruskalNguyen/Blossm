using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BlossmAPI.ModelViews
{
    public class ConfirmPayment
    {
        [Required]
        public int id_purchase_order { get; set; }
        [Required]
        public int id_payment_method { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Payment amount must be a positive integer.")]
        public int payment_amount { get; set; }
        public string? note { get; set; }
    }
}
