namespace SmartPoint.Administrator.ApplicationService.Payment.Requests
{
    public struct CheckoutRequest
    {
        public CheckoutRequest()
        {
            ExternalReference = Guid.NewGuid().ToString();
            StatementDescriptor = "Smart Point";
            Description = "Compra em Smart Point";
            RegistrationDatePayer = DateTime.UtcNow;
        }

        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }

        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }


        public decimal TransactionAmount { get; set; }
        //public int Installments { get; set; }
        //public string? PaymentMethodId { get; set; }
        //public string? IssuerId { get; set; }
        //public string? Token { get; set; }
        public string ExternalReference { get; set; }
        //public string? NotificationUrl { get; set; }

        //// Payer
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public string? Email { get; set; }
        //public string? IdentificationNumber { get; set; }
        //public string? IdentificationType { get; set; }

        public string StatementDescriptor { get; set; }
        public string Description { get; set; }

        //public Guid? IdItem { get; set; }
        //public string? TitleItem { get; set; }
        //public string? DescriptionItem { get; set; }
        //public string? PictureUrlItem { get; set; }
        //public string? CategoryIdItem { get; set; }
        //public int? QuantityItem { get; set; }
        //public decimal? UnitPriceItem { get; set; }

        //// AdditionalInfo - Payer
        //public string? FirstNamePayer { get; set; }
        //public string? LastNamePayer { get; set; }
        public DateTime RegistrationDatePayer { get; set; }
        //public string? AreaCodePhone { get; set; }
        //public string? NumberPhone { get; set; }
        //public string? ZipCodeAddress { get; set; }
        //public string? StreetNameAddress { get; set; }
        //public string? StreetNumberAddress { get; set; }
    }
}
