using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace GiftoftheGivers.Function
{
    public class DonationFunction
    {
        private readonly ILogger<DonationFunction> _logger;

        public DonationFunction(ILogger<DonationFunction> logger)
        {
            _logger = logger;
        }

        [Function("DonationFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post","get")]
            HttpRequestData req)
        {
            _logger.LogInformation("Donation function was called.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var donation = JsonSerializer.Deserialize<DonationRequest>(
                requestBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (donation == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid donation data.");
                return badResponse;
            }

            // Generate the dummy tax certificate number
            var certificateNumber =
                $"GOTG-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpperInvariant()}";

            _logger.LogInformation(
                "Tax certificate generated: {CertificateNumber}",
                certificateNumber);

            var response = req.CreateResponse(HttpStatusCode.OK);

            var result = new
            {
                certificateNumber = certificateNumber,
                message = "Donation processed successfully."
            };

            await response.WriteAsJsonAsync(result);

            return response;
        }
    }

    public class DonationRequest
    {
        public decimal Amount { get; set; }

        public string DonationType { get; set; } = "";

        public string Currency { get; set; } = "";

        public bool IsAnonymous { get; set; }
    }
}
