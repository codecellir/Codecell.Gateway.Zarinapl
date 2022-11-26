namespace CodecellShare.Dtos
{
    public class ZarinpalVerifyResultDto
    {
        public bool IsSuccess => StatusCode == 100|| StatusCode==101;
        public int StatusCode { get; set; }
        public string TransactionNumber { get; set; }
        public long RefId { get; set; }
        public string CardPan { get; set; }
        public string CardHash { get; set; }
        public string Message { get; set; }
    }
}
