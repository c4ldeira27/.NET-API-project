using Dima.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dima.Core.Requests.Transactions
{
    public class UpdateTransactionRequest: Request
    {
        [Required(ErrorMessage ="Invalid Transaction")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Invalid title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invalid type")]
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

        [Required(ErrorMessage = "Invalid date")]
        public DateTime? PaidOrReceivedAt { get; set; }

        [Required(ErrorMessage = "Invalid amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Invalid category")]
        public long CategoryId { get; set; }
    }
}
