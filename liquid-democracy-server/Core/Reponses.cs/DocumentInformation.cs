public class DocumentInformation
{
    public string documentId { get; set; }
    public List<Signer> signers { get; set; }
    public Status status { get; set; }
    public List<object> links { get; set; }
    public string title { get; set; }
    public string externalId { get; set; }
    public DataToSign dataToSign { get; set; }
    public ContactDetails contactDetails { get; set; }
}

public class Signer
{
    public string id { get; set; }
    public string url { get; set; }
    public DocumentSignature documentSignature { get; set; }
    public List<object> links { get; set; }
    public string externalSignerId { get; set; }
    public SignatureType signatureType { get; set; }
    public Authentication authentication { get; set; }
    public List<object> tags { get; set; }
    public int order { get; set; }
    public bool required { get; set; }
    public bool getSocialSecurityNumber { get; set; }
}

public class DocumentSignature
{
    public string signatureMethod { get; set; }
    public string fullName { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string signedTime { get; set; }
    public string dateOfBirth { get; set; }
    public string signatureMethodUniqueId { get; set; }
    public SocialSecurityNumber socialSecurityNumber { get; set; }
    public string clientIp { get; set; }
    public string mechanism { get; set; }
    public string personalInfoOrigin { get; set; }
    public Dictionary<string, string> attributes { get; set; }
}

public class SocialSecurityNumber
{
    public string value { get; set; }
    public string countryCode { get; set; }
}

public class SignatureType
{
    public List<string> signatureMethods { get; set; }
    public string mechanism { get; set; }
}

public class Authentication
{
    public bool authBeforeSign { get; set; }
    public string mechanism { get; set; }
    public string signatureMethodUniqueId { get; set; }
}

public class Status
{
    public string documentStatus { get; set; }
    public List<string> completedPackages { get; set; }
    public Dictionary<string, object> attachmentPackages { get; set; }
}

public class DataToSign
{
    public string fileName { get; set; }
    public bool convertToPDF { get; set; }
    public Packaging packaging { get; set; }
}

public class Packaging
{
    public List<string> signaturePackageFormats { get; set; }
}

public class ContactDetails
{
    public string email { get; set; }
}