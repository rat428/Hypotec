#pragma checksum "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5dad25b045cf6ba8ec1acd6f7961653c1ea815c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resource_AllResources), @"mvc.1.0.view", @"/Views/Resource/AllResources.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5dad25b045cf6ba8ec1acd6f7961653c1ea815c0", @"/Views/Resource/AllResources.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e0cb572af3a04e862bb8624c174daedf36e861f", @"/Views/_ViewImports.cshtml")]
    public class Views_Resource_AllResources : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Hypotec.Web.Models.ResourceModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
  
    ViewData["Title"] = "AllResources";
    Layout = "~/Views/Shared/_AdminDashboard.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<section class=\"agentbookingPage\">\r\n    <div class=\"container-fluid\">\r\n");
#nullable restore
#line 9 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
         if (ViewBag.Message != null && ViewBag.Message == true)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-success  alert-dismissible fade show\">\r\n                <strong>Success!</strong> Removed successfully.\r\n                <button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>\r\n            </div>\r\n");
#nullable restore
#line 15 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
         if (ViewBag.Message == false)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger  alert-dismissible fade show\">\r\n                <strong>Failed!</strong> Record not deleted.\r\n                <button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>\r\n            </div>\r\n");
#nullable restore
#line 22 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n          \r\n            <div class=\"col-md-12 my-4\">\r\n                <div class=\"col-md-12\">\r\n");
            WriteLiteral(@"                </div>
                <div class=""table-responsive"">
                    <table class=""table table-striped custom-table custom-table-1"">
                        <thead class=""thead-dark"">
                            <tr>
                                <th>
                                    ");
#nullable restore
#line 34 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                               Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 37 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                               Write(Html.DisplayNameFor(model => model.Tag));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 40 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                               Write(Html.DisplayNameFor(model => model.Header));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 43 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                               Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </th>
                                <th>
                                    Image
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 52 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td class=\"text-center\">\r\n                                        ");
#nullable restore
#line 56 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                   Write(Html.Raw(@item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td class=\"text-center\">\r\n                                        ");
#nullable restore
#line 59 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                   Write(Html.Raw(item.Tag));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td class=\"text-center\">\r\n                                        ");
#nullable restore
#line 62 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                   Write(Html.Raw(@item.Header));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td class=\"text-center\">\r\n                                        ");
#nullable restore
#line 65 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                   Write(Html.Raw(@item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td class=\"text-center roundimg\">\r\n                                        <img");
            BeginWriteAttribute("src", " src=\"", 3125, "\"", 3146, 1);
#nullable restore
#line 68 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
WriteAttributeValue("", 3131, item.ImagePath, 3131, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 3147, "\"", 3155, 0);
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3156, "\"", 3162, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                    </td>\r\n                                    <td class=\"text-center anchor-link\">\r\n                                        <div class=\"d-flex\">\r\n                                            ");
#nullable restore
#line 72 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                       Write(Html.ActionLink("", "EditResources", new { id = item.Id }, new { @class = "glyphicon glyphicon-edit" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            ");
#nullable restore
#line 73 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                                       Write(Html.ActionLink(
                                            "",
                                            "Delete",
                                            new { id = item.Id },
                                            new { @class = "glyphicon glyphicon-trash", onclick = "return confirm('Are you sure you wish to delete this resource?');" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("                                        </div>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 82 "E:\Project\Hypotec-MGLD\Hypotec.Web\Views\Resource\AllResources.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Hypotec.Web.Models.ResourceModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
