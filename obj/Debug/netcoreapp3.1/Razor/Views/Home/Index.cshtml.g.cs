#pragma checksum "C:\Users\iDixon\source\repos\PIozo\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e7060091b304365eac368efe9c56a186f4a73b9e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\iDixon\source\repos\PIozo\Views\_ViewImports.cshtml"
using OZO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\iDixon\source\repos\PIozo\Views\_ViewImports.cshtml"
using OZO.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7060091b304365eac368efe9c56a186f4a73b9e", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd966739c832a681dfaa1f7279becd3beab00849", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\iDixon\source\repos\PIozo\Views\Home\Index.cshtml"
  
  ViewData["Title"] = "Početna stranica";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<p>
  Primjerom se ilustrira nekoliko karakterističnih primjera korištenja MVC-a i ASP.NET Corea.
</p>
<p>
  Primjer s državama prikazuje podatke iz tablice koja nema stranih ključeva. Prilikom pregleda
  prikazuje se po n podataka po stranici pri čemu je podatak o broju elemenata po stranici
  zapisan u konfiguracijskoj datoteci appsettings.json (novitet iz .NET Corea).
</p>
<p>
  Također se vodi računa o tome da ako krenemo u ažuriranje neke države da se nakon ažuriranja vratimo
  na istu stranicu na kojoj smo bili prije odlaska na ažuriranje.
</p>
<p>
  Omogućeno je dodavanje novih država, ažuriranje i brisanje postojećih, pri čemu se
  ažuriranje obavlja na posebnoj stranici.
</p>
<p>
    U primjeru s mjestima ilustriran je odabir vrijednost stranog ključa (država kojoj mjesta pripada)
    korištenjem padajuće liste. Dodatno, primjer prilikom brisanja retka koristi Ajax
    te se samo uklanja obrisani redak bez osvježavanja cijele stranice.
</p>
<p>
  Primjer s partnerima je primje");
            WriteLiteral(@"r hijerarhije klasa Partner, Tvrtka i Osoba, što je u bazi podataka
  modelirano u obliku TPT (Table per type). Spojeni podaci dohvaćaju se preko pogleda koji je naknadno
  ručno dodan u data model.
</p>
<p>
  Kod dodavanja novog partnera pomoću javascripta se dinamički mijenjaju kontrole za unos ovisno radi se li se
  o tvrtki ili osobi, a odabir mjesta se vrši pomoću padajuće liste
</p>
<p>
  Primjer s dokumentima je primjer master-detail forme kod koje je odabir stranog ključa
  za veliki broj mogućnosti izveden javascriptom pomoću autocompleta uz korištenjem
  Web API controllera.
</p>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
