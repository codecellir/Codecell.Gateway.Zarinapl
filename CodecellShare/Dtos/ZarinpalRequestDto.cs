using System.Text.Json.Serialization;

namespace CodecellShare.Dtos
{
    public class ZarinpalRequestDto
    {
        [JsonPropertyName("merchant_id")]
        public string MerchantId { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
