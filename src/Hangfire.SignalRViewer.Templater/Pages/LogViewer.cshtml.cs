#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hangfire.SignalRViewer.Templater.Pages
{
    using System;
    using System.Collections.Generic;
    
    #line 3 "..\..\Pages\LogViewer.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 4 "..\..\Pages\LogViewer.cshtml"
    using Hangfire.Dashboard.Pages;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Pages\LogViewer.cshtml"
    using Hangfire.SignalRViewer.DependencyInjection;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class LogViewer : Hangfire.Dashboard.RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n\r\n");






            
            #line 7 "..\..\Pages\LogViewer.cshtml"
  
    Layout = new LayoutPage("Hello World");


            
            #line default
            #line hidden
WriteLiteral("\r\n<template id=\"configuration\">\r\n    {\r\n    \"DateFormat\": \"");


            
            #line 13 "..\..\Pages\LogViewer.cshtml"
              Write(GlobalConfigurationExtensions.Options.DateFormat);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n    \"LevelFormat\": \"");


            
            #line 14 "..\..\Pages\LogViewer.cshtml"
               Write(GlobalConfigurationExtensions.Options.LevelFormat);

            
            #line default
            #line hidden
WriteLiteral(@"""
    }
</template>

<link rel=""stylesheet"" type=""text/css"" href=""/css/styles.css"" />

<div class=""row"" id=""vue-app"">
    <div class=""col-md-3 col-12"">
        <div class=""list-group"">
            <vu-log-category v-for=""item in orderedCategories""
                             v-bind:item=""item""
                             v-bind:key=""item.id""></vu-log-category>
        </div>
    </div>
    <div class=""col-md-9 col-12"">
        <div class=""demo"">
            <ul>
                <vu-log-item v-for=""item in logsList""
                             v-bind:item=""item""
                             v-bind:key=""item.id""></vu-log-item>
            </ul>
        </div>
    </div>
</div>

<script src=""https://unpkg.com/vue@next""></script>
<script src=""https://unpkg.com/vuex@4.0.2/dist/vuex.global.js""></script>

<script type=""application/javascript"" src=""/libs/signalr.js""></script>
<script type=""application/javascript"" src=""/js/index.js""></script>
");


        }
    }
}
#pragma warning restore 1591
