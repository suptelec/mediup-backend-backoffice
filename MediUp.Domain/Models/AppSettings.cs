using MediUp.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Models;
public class AppSettings
{
    public string[] BaseDomain { get; set; } = Array.Empty<string>();
    public string? FrontOfficeLoginUrl { get; set; }
    public string ContentBucketCertificateFolderName { get; set; } = string.Empty;
    public string ContentBucketName { get; set; } = string.Empty;
    public string CloudFrontUrl { get; set; } = string.Empty;
    public int PaymentCategoryPercentCondition { get; set; }

    public void CheckSettings()
    {
        Check.NotEmpty(BaseDomain, nameof(BaseDomain));
        Check.NotEmpty(FrontOfficeLoginUrl, nameof(FrontOfficeLoginUrl));

    }
}
