﻿using Newtonsoft.Json;
using Stripe;
using Stripe.Infrastructure;

namespace StripeGateway
{
    public class StripeSubscriptionResponse
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }



        #region Expandable Application

        /// <summary>
        /// (ID of the Application)
        /// ID of the Connect Application that created the subscription.
        /// </summary>
        [JsonIgnore]
        public string ApplicationId { get; set; }

        /// <summary>
        /// (Expanded)
        /// ID of the Connect Application that created the subscription.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public Application Application { get; set; }
        #endregion

        /// <summary>
        /// A non-negative decimal between 0 and 100, with at most two decimal places. This
        /// represents the percentage of the subscription invoice total that will be transferred to
        /// the application owner's Stripe account.
        /// </summary>
        [JsonProperty("application_fee_percent")]
        public decimal? ApplicationFeePercent { get; set; }

        [JsonProperty("automatic_tax")]
        public SubscriptionAutomaticTax AutomaticTax { get; set; }

        /// <summary>
        /// The reference point that aligns future <a
        /// href="https://stripe.com/docs/subscriptions/billing-cycle">billing cycle</a> dates. It
        /// sets the day of week for <c>week</c> intervals, the day of month for <c>month</c> and
        /// <c>year</c> intervals, and the month of year for <c>year</c> intervals. The timestamp is
        /// in UTC format.
        /// </summary>
        [JsonProperty("billing_cycle_anchor")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime BillingCycleAnchor { get; set; }

        /// <summary>
        /// The fixed values used to calculate the <c>billing_cycle_anchor</c>.
        /// </summary>
        [JsonProperty("billing_cycle_anchor_config")]
        public SubscriptionBillingCycleAnchorConfig BillingCycleAnchorConfig { get; set; }

        /// <summary>
        /// Define thresholds at which an invoice will be sent, and the subscription advanced to a
        /// new billing period.
        /// </summary>
        [JsonProperty("billing_thresholds")]
        public SubscriptionBillingThresholds BillingThresholds { get; set; }

        /// <summary>
        /// A date in the future at which the subscription will automatically get canceled.
        /// </summary>
        [JsonProperty("cancel_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CancelAt { get; set; }

        /// <summary>
        /// Whether this subscription will (if <c>status=active</c>) or did (if
        /// <c>status=canceled</c>) cancel at the end of the current billing period.
        /// </summary>
        [JsonProperty("cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; set; }

        /// <summary>
        /// If the subscription has been canceled, the date of that cancellation. If the
        /// subscription was canceled with <c>cancel_at_period_end</c>, <c>canceled_at</c> will
        /// reflect the time of the most recent update request, not the end of the subscription
        /// period when the subscription is automatically moved to a canceled state.
        /// </summary>
        [JsonProperty("canceled_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        /// Details about why this subscription was cancelled.
        /// </summary>
        [JsonProperty("cancellation_details")]
        public SubscriptionCancellationDetails CancellationDetails { get; set; }

        /// <summary>
        /// Either <c>charge_automatically</c>, or <c>send_invoice</c>. When charging automatically,
        /// Stripe will attempt to pay this subscription at the end of the cycle using the default
        /// source attached to the customer. When sending an invoice, Stripe will email your
        /// customer an invoice with payment instructions and mark the subscription as
        /// <c>active</c>.
        /// One of: <c>charge_automatically</c>, or <c>send_invoice</c>.
        /// </summary>
        [JsonProperty("collection_method")]
        public string CollectionMethod { get; set; }

        /// <summary>
        /// Time at which the object was created. Measured in seconds since the Unix epoch.
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Created { get; set; }

        /// <summary>
        /// Three-letter <a href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency
        /// code</a>, in lowercase. Must be a <a href="https://stripe.com/docs/currencies">supported
        /// currency</a>.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// End of the current period that the subscription has been invoiced for. At the end of
        /// this period, a new invoice will be created.
        /// </summary>
        [JsonProperty("current_period_end")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CurrentPeriodEnd { get; set; }

        /// <summary>
        /// Start of the current period that the subscription has been invoiced for.
        /// </summary>
        [JsonProperty("current_period_start")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CurrentPeriodStart { get; set; }

        #region Expandable Customer

        /// <summary>
        /// (ID of the Customer)
        /// ID of the customer who owns the subscription.
        /// </summary>
        [JsonIgnore]
        public string CustomerId { get; set; }

        /// <summary>
        /// (Expanded)
        /// ID of the customer who owns the subscription.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public Customer Customer { get; set; }
        #endregion

        /// <summary>
        /// Number of days a customer has to pay invoices generated by this subscription. This value
        /// will be <c>null</c> for subscriptions where
        /// <c>collection_method=charge_automatically</c>.
        /// </summary>
        [JsonProperty("days_until_due")]
        public long? DaysUntilDue { get; set; }

        #region Expandable DefaultPaymentMethod

        /// <summary>
        /// (ID of the PaymentMethod)
        /// ID of the default payment method for the subscription. It must belong to the customer
        /// associated with the subscription. This takes precedence over <c>default_source</c>. If
        /// neither are set, invoices will use the customer's <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-invoice_settings-default_payment_method">invoice_settings.default_payment_method</a>
        /// or <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-default_source">default_source</a>.
        /// </summary>
        [JsonIgnore]
        public string DefaultPaymentMethodId { get; set; }
        /// <summary>
        /// (Expanded)
        /// ID of the default payment method for the subscription. It must belong to the customer
        /// associated with the subscription. This takes precedence over <c>default_source</c>. If
        /// neither are set, invoices will use the customer's <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-invoice_settings-default_payment_method">invoice_settings.default_payment_method</a>
        /// or <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-default_source">default_source</a>.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public PaymentMethod DefaultPaymentMethod { get; set; }
        #endregion

        #region Expandable DefaultSource

        /// <summary>
        /// (ID of the IPaymentSource)
        /// ID of the default payment source for the subscription. It must belong to the customer
        /// associated with the subscription and be in a chargeable state. If
        /// <c>default_payment_method</c> is also set, <c>default_payment_method</c> will take
        /// precedence. If neither are set, invoices will use the customer's <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-invoice_settings-default_payment_method">invoice_settings.default_payment_method</a>
        /// or <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-default_source">default_source</a>.
        /// </summary>
        [JsonIgnore]
        public string DefaultSourceId { get; set; }

        /// <summary>
        /// (Expanded)
        /// ID of the default payment source for the subscription. It must belong to the customer
        /// associated with the subscription and be in a chargeable state. If
        /// <c>default_payment_method</c> is also set, <c>default_payment_method</c> will take
        /// precedence. If neither are set, invoices will use the customer's <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-invoice_settings-default_payment_method">invoice_settings.default_payment_method</a>
        /// or <a
        /// href="https://stripe.com/docs/api/customers/object#customer_object-default_source">default_source</a>.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public IPaymentSource DefaultSource { get; set; }
        #endregion

        /// <summary>
        /// The tax rates that will apply to any subscription item that does not have
        /// <c>tax_rates</c> set. Invoices created will have their <c>default_tax_rates</c>
        /// populated from the subscription.
        /// </summary>
        [JsonProperty("default_tax_rates")]
        public List<TaxRate> DefaultTaxRates { get; set; }

        /// <summary>
        /// The subscription's description, meant to be displayable to the customer. Use this field
        /// to optionally store an explanation of the subscription for rendering in Stripe surfaces
        /// and certain local payment methods UIs.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Describes the current discount applied to this subscription, if there is one. When
        /// billing, a discount applied to a subscription overrides a discount applied on a
        /// customer-wide basis. This field has been deprecated and will be removed in a future API
        /// version. Use <c>discounts</c> instead.
        /// </summary>
        [JsonProperty("discount")]
        public Discount Discount { get; set; }

        #region Expandable Discounts

        /// <summary>
        /// (IDs of the Discounts)
        /// The discounts applied to the subscription. Subscription item discounts are applied
        /// before subscription discounts. Use <c>expand[]=discounts</c> to expand each discount.
        /// </summary>
        [JsonIgnore]
        public List<string> DiscountIds { get; set; }

        /// <summary>
        /// (Expanded)
        /// The discounts applied to the subscription. Subscription item discounts are applied
        /// before subscription discounts. Use <c>expand[]=discounts</c> to expand each discount.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public List<Discount> Discounts { get; set; }
        #endregion

        /// <summary>
        /// If the subscription has ended, the date the subscription ended.
        /// </summary>
        [JsonProperty("ended_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? EndedAt { get; set; }

        [JsonProperty("invoice_settings")]
        public SubscriptionInvoiceSettings InvoiceSettings { get; set; }

        /// <summary>
        /// List of subscription items, each with an attached price.
        /// </summary>
        [JsonProperty("items")]
        public StripeList<SubscriptionItem> Items { get; set; }

        /// <summary>
        /// Details of the most recent price migration that failed for the subscription.
        /// </summary>
        [JsonProperty("last_price_migration_error")]
        public SubscriptionLastPriceMigrationError LastPriceMigrationError { get; set; }

        #region Expandable LatestInvoice

        /// <summary>
        /// (ID of the Invoice)
        /// The most recent invoice this subscription has generated.
        /// </summary>
        [JsonIgnore]
        public string LatestInvoiceId { get; set; }

        /// <summary>
        /// (Expanded)
        /// The most recent invoice this subscription has generated.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public Invoice LatestInvoice { get; set; }
        #endregion

        /// <summary>
        /// Has the value <c>true</c> if the object exists in live mode or the value <c>false</c> if
        /// the object exists in test mode.
        /// </summary>
        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        /// <summary>
        /// Set of <a href="https://stripe.com/docs/api/metadata">key-value pairs</a> that you can
        /// attach to an object. This can be useful for storing additional information about the
        /// object in a structured format.
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Specifies the approximate timestamp on which any pending invoice items will be billed
        /// according to the schedule provided at <c>pending_invoice_item_interval</c>.
        /// </summary>
        [JsonProperty("next_pending_invoice_item_invoice")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? NextPendingInvoiceItemInvoice { get; set; }

        #region Expandable OnBehalfOf

        /// <summary>
        /// (ID of the Account)
        /// The account (if any) the charge was made on behalf of for charges associated with this
        /// subscription. See the Connect documentation for details.
        /// </summary>
        [JsonIgnore]
        public string OnBehalfOfId { get; set; }

        /// <summary>
        /// (Expanded)
        /// The account (if any) the charge was made on behalf of for charges associated with this
        /// subscription. See the Connect documentation for details.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public Account OnBehalfOf { get; set; }
        #endregion

        /// <summary>
        /// If specified, payment collection for this subscription will be paused. Note that the
        /// subscription status will be unchanged and will not be updated to <c>paused</c>. Learn
        /// more about <a href="https://stripe.com/docs/billing/subscriptions/pause-payment">pausing
        /// collection</a>.
        /// </summary>
        [JsonProperty("pause_collection")]
        public SubscriptionPauseCollection PauseCollection { get; set; }

        /// <summary>
        /// Payment settings passed on to invoices created by the subscription.
        /// </summary>
        [JsonProperty("payment_settings")]
        public SubscriptionPaymentSettings PaymentSettings { get; set; }

        /// <summary>
        /// Specifies an interval for how often to bill for any pending invoice items. It is
        /// analogous to calling <a href="https://stripe.com/docs/api#create_invoice">Create an
        /// invoice</a> for the given subscription at the specified interval.
        /// </summary>
        [JsonProperty("pending_invoice_item_interval")]
        public SubscriptionPendingInvoiceItemInterval PendingInvoiceItemInterval { get; set; }

        #region Expandable PendingSetupIntent

        /// <summary>
        /// (ID of the SetupIntent)
        /// You can use this <a href="https://stripe.com/docs/api/setup_intents">SetupIntent</a> to
        /// collect user authentication when creating a subscription without immediate payment or
        /// updating a subscription's payment method, allowing you to optimize for off-session
        /// payments. Learn more in the <a
        /// href="https://stripe.com/docs/billing/migration/strong-customer-authentication#scenario-2">SCA
        /// Migration Guide</a>.
        /// </summary>
        [JsonIgnore]
        public string PendingSetupIntentId { get; set; }

        /// <summary>
        /// (Expanded)
        /// You can use this <a href="https://stripe.com/docs/api/setup_intents">SetupIntent</a> to
        /// collect user authentication when creating a subscription without immediate payment or
        /// updating a subscription's payment method, allowing you to optimize for off-session
        /// payments. Learn more in the <a
        /// href="https://stripe.com/docs/billing/migration/strong-customer-authentication#scenario-2">SCA
        /// Migration Guide</a>.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public SetupIntent PendingSetupIntent { get; set; }
        #endregion

        /// <summary>
        /// If specified, <a
        /// href="https://stripe.com/docs/billing/subscriptions/pending-updates">pending updates</a>
        /// that will be applied to the subscription once the <c>latest_invoice</c> has been paid.
        /// </summary>
        [JsonProperty("pending_update")]
        public SubscriptionPendingUpdate PendingUpdate { get; set; }

        /// <summary>
        /// Time period and invoice for a Subscription billed in advance.
        /// </summary>
        [JsonProperty("prebilling")]
        public SubscriptionPrebilling Prebilling { get; set; }

        #region Expandable Schedule

        /// <summary>
        /// (ID of the SubscriptionSchedule)
        /// The schedule attached to the subscription.
        /// </summary>
        [JsonIgnore]
        public string ScheduleId { get; set; }

        /// <summary>
        /// (Expanded)
        /// The schedule attached to the subscription.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public SubscriptionSchedule Schedule { get; set; }
        #endregion

        /// <summary>
        /// Date when the subscription was first created. The date might differ from the
        /// <c>created</c> date due to backdating.
        /// </summary>
        [JsonProperty("start_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Possible values are <c>incomplete</c>, <c>incomplete_expired</c>, <c>trialing</c>,
        /// <c>active</c>, <c>past_due</c>, <c>canceled</c>, <c>unpaid</c>, or <c>paused</c>.
        ///
        /// For <c>collection_method=charge_automatically</c> a subscription moves into
        /// <c>incomplete</c> if the initial payment attempt fails. A subscription in this status
        /// can only have metadata and default_source updated. Once the first invoice is paid, the
        /// subscription moves into an <c>active</c> status. If the first invoice is not paid within
        /// 23 hours, the subscription transitions to <c>incomplete_expired</c>. This is a terminal
        /// status, the open invoice will be voided and no further invoices will be generated.
        ///
        /// A subscription that is currently in a trial period is <c>trialing</c> and moves to
        /// <c>active</c> when the trial period is over.
        ///
        /// A subscription can only enter a <c>paused</c> status <a
        /// href="https://stripe.com/docs/billing/subscriptions/trials#create-free-trials-without-payment">when
        /// a trial ends without a payment method</a>. A <c>paused</c> subscription doesn't generate
        /// invoices and can be resumed after your customer adds their payment method. The
        /// <c>paused</c> status is different from <a
        /// href="https://stripe.com/docs/billing/subscriptions/pause-payment">pausing
        /// collection</a>, which still generates invoices and leaves the subscription's status
        /// unchanged.
        ///
        /// If subscription <c>collection_method=charge_automatically</c>, it becomes
        /// <c>past_due</c> when payment is required but cannot be paid (due to failed payment or
        /// awaiting additional user actions). Once Stripe has exhausted all payment retry attempts,
        /// the subscription will become <c>canceled</c> or <c>unpaid</c> (depending on your
        /// subscriptions settings).
        ///
        /// If subscription <c>collection_method=send_invoice</c> it becomes <c>past_due</c> when
        /// its invoice is not paid by the due date, and <c>canceled</c> or <c>unpaid</c> if it is
        /// still not paid by an additional deadline after that. Note that when a subscription has a
        /// status of <c>unpaid</c>, no subsequent invoices will be attempted (invoices will be
        /// created, but then immediately automatically closed). After receiving updated payment
        /// information from a customer, you may choose to reopen and pay their closed invoices.
        /// One of: <c>active</c>, <c>canceled</c>, <c>incomplete</c>, <c>incomplete_expired</c>,
        /// <c>past_due</c>, <c>paused</c>, <c>trialing</c>, or <c>unpaid</c>.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        #region Expandable TestClock

        /// <summary>
        /// (ID of the TestHelpers.TestClock)
        /// ID of the test clock this subscription belongs to.
        /// </summary>
        [JsonIgnore]
        public string TestClockId { get; set; }

        /// <summary>
        /// (Expanded)
        /// ID of the test clock this subscription belongs to.
        ///
        /// For more information, see the <a href="https://stripe.com/docs/expand">expand documentation</a>.
        /// </summary>
        [JsonIgnore]
        public Stripe.TestHelpers.TestClock TestClock { get; set; }
        #endregion

        /// <summary>
        /// The account (if any) the subscription's payments will be attributed to for tax
        /// reporting, and where funds from each payment will be transferred to for each of the
        /// subscription's invoices.
        /// </summary>
        [JsonProperty("transfer_data")]
        public SubscriptionTransferData TransferData { get; set; }

        /// <summary>
        /// If the subscription has a trial, the end of that trial.
        /// </summary>
        [JsonProperty("trial_end")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? TrialEnd { get; set; }

        /// <summary>
        /// Settings related to subscription trials.
        /// </summary>
        [JsonProperty("trial_settings")]
        public SubscriptionTrialSettings TrialSettings { get; set; }

        /// <summary>
        /// If the subscription has a trial, the beginning of that trial.
        /// </summary>
        [JsonProperty("trial_start")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? TrialStart { get; set; }

    }
}
