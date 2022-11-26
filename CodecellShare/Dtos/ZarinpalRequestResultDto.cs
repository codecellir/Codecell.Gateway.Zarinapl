namespace CodecellShare.Dtos
{
    public class ZarinpalRequestResultDto
    {
        public bool IsSuccess => StatusCode == 100;
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string FeeType { get; set; }
        public int Fee { get; set; }
        public string CallBackUrl { get; set; }
    }
}
