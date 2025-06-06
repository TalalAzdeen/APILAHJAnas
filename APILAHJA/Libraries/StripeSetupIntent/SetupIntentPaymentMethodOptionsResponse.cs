﻿using Newtonsoft.Json;
using Stripe;

namespace StripeGateway
{
    public class SetupIntentPaymentMethodOptionsResponse
    {
        [JsonProperty("acss_debit")]
        public SetupIntentPaymentMethodOptionsAcssDebit AcssDebit { get; set; }

        [JsonProperty("amazon_pay")]
        public SetupIntentPaymentMethodOptionsAmazonPay AmazonPay { get; set; }

        [JsonProperty("bacs_debit")]
        public SetupIntentPaymentMethodOptionsBacsDebit BacsDebit { get; set; }

        [JsonProperty("card")]
        public SetupIntentPaymentMethodOptionsCard Card { get; set; }

        [JsonProperty("card_present")]
        public SetupIntentPaymentMethodOptionsCardPresent CardPresent { get; set; }

        [JsonProperty("link")]
        public SetupIntentPaymentMethodOptionsLink Link { get; set; }

        [JsonProperty("paypal")]
        public SetupIntentPaymentMethodOptionsPaypal Paypal { get; set; }

        [JsonProperty("payto")]
        public SetupIntentPaymentMethodOptionsPayto Payto { get; set; }

        [JsonProperty("sepa_debit")]
        public SetupIntentPaymentMethodOptionsSepaDebit SepaDebit { get; set; }

        [JsonProperty("us_bank_account")]
        public SetupIntentPaymentMethodOptionsUsBankAccount UsBankAccount { get; set; }
    }
}
