﻿using NUnit.Framework;
using Stripe;
using System.Diagnostics;

namespace StripeSandbox
{
    public class StripeTestClient {
        [SetUp]
        public void Setup()
        {

            StripeConfiguration.ApiKey = "sk_test_51K7dC5FexnxosIm9q7TFro3Jvh1ArfpQWsJyfIQ9WrrJNYFZMt4CTdgHNnBxT4WakqBZQpVDghSllu8kz0uVYCGV00sAFIpLir";

        }

        [Test, Description("Test the POST /v1/charges endpoint")]
        public void TestBasicChargeObjectInternationalValidMasterCard()
        {
            ChargeListOptions options = new ChargeListOptions();
            ChargeService service = new ChargeService();
            StripeList<Charge> charges = service.List(options);
            var chargesBefore = charges.Data.Count;

            var optionsCreateNewCharge = new ChargeCreateOptions()
            {
                Amount = 200,
                Currency = "usd",
                Source = "tok_ae_mastercard",
                Description = "My first ever Stripe test"

            };
            Charge charge = service.Create(optionsCreateNewCharge);
            charges = service.List(new ChargeListOptions());
            var chargesAfter = charges.Data.Count;
            Debug.WriteLine("The charge object if you want to see more fields", charge);
            Assert.NotNull(charge);
            Assert.AreEqual(charge.Amount, optionsCreateNewCharge.Amount);
            Assert.AreEqual(charge.AmountCaptured, optionsCreateNewCharge.Amount);
            Assert.AreEqual(charge.Currency, optionsCreateNewCharge.Currency);
            Assert.That(chargesAfter - chargesBefore, Is.EqualTo(1));

        }

        [Test, Description("Test the GET /v1/charges endpoint")]
        public void TestListAllChargesWithoutLimit()
        {
            var options = new ChargeListOptions();
            var service = new ChargeService();
            StripeList<Charge> charges = service.List(options);
            Debug.WriteLine("The charge object if you want to see more fields", charges);
            Assert.NotNull(charges);
            Assert.That(charges.Data.Count, Is.GreaterThan(0));

        }

    }
}