namespace CodecellShare.Dtos.Internal
{
    internal class ZarinpalVerifyResponseDto
    {
        public int Code { get; set; }
        public string CardHash { get; set; }
        public string CardPan { get; set; }
        public long RefId { get; set; }
        public string Message { get; set; }
        public string FeeType { get; set; }
        public int Fee { get; set; }
    }
}
