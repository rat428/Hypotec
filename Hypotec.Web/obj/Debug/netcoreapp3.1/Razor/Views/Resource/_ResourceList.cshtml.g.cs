#pragma checksum "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b8d6f4c6c21323f57f2de5b67b9823b032dd6d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resource__ResourceList), @"mvc.1.0.view", @"/Views/Resource/_ResourceList.cshtml")]
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
#line 1 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\_ViewImports.cshtml"
using Hypotec.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\_ViewImports.cshtml"
using Hypotec.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b8d6f4c6c21323f57f2de5b67b9823b032dd6d4", @"/Views/Resource/_ResourceList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e0cb572af3a04e862bb8624c174daedf36e861f", @"/Views/_ViewImports.cshtml")]
    public class Views_Resource__ResourceList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Hypotec.Web.Models.ResourceModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n\r\n");
#nullable restore
#line 6 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-12 col-md-6 col-xl-4 col-lg-4\" id=\"divNone\">\r\n            <div class=\"reBox\">\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 236, "\"", 257, 1);
#nullable restore
#line 10 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 242, item.ImagePath, 242, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid\"");
            BeginWriteAttribute("alt", " alt=\"", 276, "\"", 282, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                <div class=\"reBoxContent\">\r\n                    <h3>");
#nullable restore
#line 12 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                   Write(Html.Raw(@item.Header));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                    <p>");
#nullable restore
#line 13 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                  Write(Html.Raw(@item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </p>\r\n");
#nullable restore
#line 14 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                     if (item.Id == "1")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 536, "\'", 587, 1);
#nullable restore
#line 16 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 543, Url.Action("BuyersClosingCosts", "Aboutus"), 543, 44, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 17 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }
                    else if (item.Id == "2")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 737, "\'", 785, 1);
#nullable restore
#line 20 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 744, Url.Action("SellingYourHome", "Aboutus"), 744, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 21 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }
                    else if (item.Id == "3")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 935, "\'", 982, 1);
#nullable restore
#line 24 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 942, Url.Action("HowToLandADeal", "Aboutus"), 942, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 25 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }
                    else if (item.Id == "4")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 1132, "\'", 1188, 1);
#nullable restore
#line 28 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 1139, Url.Action("PreApprovedForAMortgage", "Aboutus"), 1139, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 29 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }
                    else if (item.Id == "5")
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 1338, "\'", 1392, 1);
#nullable restore
#line 32 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 1345, Url.Action("RefinanceYourMortgage", "Aboutus"), 1345, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 33 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("href", " href=\'", 1522, "\'", 1584, 1);
#nullable restore
#line 36 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
WriteAttributeValue("", 1529, Url.Action("Article", "Resource",new { id = @item.Id}), 1529, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn\">Read More...</a>\r\n");
#nullable restore
#line 37 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 41 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\_ResourceList.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Hypotec.Web.Models.ResourceModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591