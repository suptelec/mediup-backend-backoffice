using MediUp.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Models;
public class AppAssets
{
 
    public virtual string UpgradeCustomerTemplate { get; } = null!;
    public virtual string LogoPath { get; }


    public AppAssets(string contentRootPath)
    {
      
        UpgradeCustomerTemplate = Path.Combine(contentRootPath, "templates", "Mail", "UpgradeCustomerTemplate.html");
        LogoPath = Path.Combine(contentRootPath, "images", "logo.png");

        // Solo validar la existencia de archivos si no estamos en un entorno de pruebas
        if (!AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
         
            Check.FileExists(UpgradeCustomerTemplate, nameof(UpgradeCustomerTemplate));
            Check.FileExists(LogoPath, nameof(LogoPath));
        }
    }

}
