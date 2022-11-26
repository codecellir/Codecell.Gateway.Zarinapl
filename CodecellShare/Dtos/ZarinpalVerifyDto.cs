using System.Text.Json.Serialization;

namespace CodecellShare.Dtos
{
    public class ZarinpalVerifyDto
    {
        [JsonPropertyName("merchant_id")]
        public string MerchantId { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        [JsonIgnore]
        public string Status { get; set; }
    }
}
