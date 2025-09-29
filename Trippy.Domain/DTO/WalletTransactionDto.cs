namespace Trippy.Domain.DTO
{
    public class WalletTransactionDto
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public decimal Coins { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string PaymentGateWaySerialNumber { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PurchaseTransactionDTO
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public decimal Coins { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string PaymentGateWaySerialNumber { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreatedAt { get; set; }

        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
    }

}
