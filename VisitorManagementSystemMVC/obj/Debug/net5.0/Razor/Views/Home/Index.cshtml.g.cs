#pragma checksum "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1cc8f080545917610aa3a24208f28e5b8981f340"
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
#line 1 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\_ViewImports.cshtml"
using VisitorManagementSystemMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\_ViewImports.cshtml"
using VisitorManagementSystemMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cc8f080545917610aa3a24208f28e5b8981f340", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cc2d1ca3906ffa63f2c0871228bed88838c6044", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<br />\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Visitor Management for Smarter Office</h1>\r\n    <h4>Get rid of paper visitor logs,improve security,and increase productivity.</h4>\r\n    <hr />\r\n");
#nullable restore
#line 11 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml"
     if (User.IsInRole("User"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1 class=\"display-4\">Welcome User</h1>\r\n");
#nullable restore
#line 14 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml"
     if (User.IsInRole("Admin"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1 class=\"display-4\">Welcome Admin</h1>\r\n");
#nullable restore
#line 18 "C:\Users\hp\.netCore\VisitorManagementSystemMVC\VisitorManagementSystemMVC\Views\Home\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591