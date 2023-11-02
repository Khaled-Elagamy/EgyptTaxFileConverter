namespace Converter
{
    public class Address
    {
        public string branchID { get; set; }
        public string country { get; set; }
        public string governate { get; set; }
        public string regionCity { get; set; }
        public string street { get; set; }
        public string buildingNumber { get; set; }
        public string postalCode { get; set; }
        public string floor { get; set; }
        public string room { get; set; }
        public string landmark { get; set; }
        public string additionalInformation { get; set; }
    }

    public class Issuer
    {
        public Address address { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Payment
    {
        public string bankName { get; set; }
        public string bankAddress { get; set; }
        public string bankAccountNo { get; set; }
        public string bankAccountIBAN { get; set; }
        public string swiftCode { get; set; }
        public string terms { get; set; }
    }
    public class Delivery
    {
        public string approach { get; set; }
        public string packaging { get; set; }
        public DateTime dateValidity { get; set; }
        public string exportPort { get; set; }
        public string countryOfOrigin { get; set; }
        public double grossWeight { get; set; }
        public double netWeight { get; set; }
        public string terms { get; set; }
    }


    public class UnitValue
    {
        public string currencySold { get; set; }
        public double amountEGP { get; set; }
        public double amountSold { get; set; }
        public double currencyExchangeRate { get; set; }
    }

    public class Discount
    {
        public int rate { get; set; }
        public double amount { get; set; }
    }

    public class TaxableItem
    {
        public string taxType { get; set; }
        public double amount { get; set; }
        public string subType { get; set; }
        public int rate { get; set; }
    }

    public class InvoiceLine
    {
        public string description { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public string unitType { get; set; }
        public int quantity { get; set; }
        public string internalCode { get; set; }
        public double salesTotal { get; set; }
        public double total { get; set; }
        public double valueDifference { get; set; }
        public double totalTaxableFees { get; set; }
        public double netTotal { get; set; }
        public int itemsDiscount { get; set; }
        public UnitValue unitValue { get; set; }
        public Discount discount { get; set; }
        public List<TaxableItem> taxableItems { get; set; }
    }

    public class TaxTotal
    {
        public string taxType { get; set; }
        public double amount { get; set; }
    }

    public class DocumentModel
    {
        public Issuer issuer { get; set; }
        public Issuer receiver { get; set; }
        public string documentType { get; set; }
        public string documentTypeVersion { get; set; }
        public DateTime dateTimeIssued { get; set; }
        public string taxpayerActivityCode { get; set; }
        public string internalID { get; set; }
        public string purchaseOrderReference { get; set; }
        public string purchaseOrderDescription { get; set; }
        public string salesOrderReference { get; set; }
        public string salesOrderDescription { get; set; }
        public string proformaInvoiceNumber { get; set; }
        public Payment payment { get; set; }
        public Delivery delivery { get; set; }
        public List<InvoiceLine> invoiceLines { get; set; }
        public double totalDiscountAmount { get; set; }
        public double totalSalesAmount { get; set; }
        public double netAmount { get; set; }
        public List<TaxTotal> taxTotals { get; set; }
        public double totalAmount { get; set; }
        public double extraDiscountAmount { get; set; }
        public double totalItemsDiscountAmount { get; set; }
    }



    public class InvoiceModel
    {
        public string uuid { get; set; }
        public string submissionUUID { get; set; }
        public string longId { get; set; }
        public string internalId { get; set; }
        public string typeName { get; set; }
        public string typeVersionName { get; set; }
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string receiverId { get; set; }
        public string receiverName { get; set; }
        public DateTime dateTimeIssued { get; set; }
        public DateTime dateTimeReceived { get; set; }
        public double totalSales { get; set; }
        public double totalDiscount { get; set; }
        public double netAmount { get; set; }
        public double total { get; set; }
        public string status { get; set; }
        public string document { get; set; }
        public object lateSubmissionRequestNumber { get; set; }
        public string transformationStatus { get; set; }
        public ValidationResults validationResults { get; set; }
        public int maxPercision { get; set; }
        public List<InvoiceLineItemCode> invoiceLineItemCodes { get; set; }
        public DocumentModel Documents { get; set; }
    }

    public class ValidationResults
    {
        public string status { get; set; }
        public List<ValidationStep> validationSteps { get; set; }
    }

    public class ValidationStep
    {
        public string name { get; set; }
        public string status { get; set; }
    }

    public class InvoiceLineItemCode
    {
        public int codeTypeId { get; set; }
        public string codeTypeNamePrimaryLang { get; set; }
        public string codeTypeNameSecondaryLang { get; set; }
        public string itemCode { get; set; }
        public string codeNamePrimaryLang { get; set; }
        public string codeNameSecondaryLang { get; set; }
    }
}