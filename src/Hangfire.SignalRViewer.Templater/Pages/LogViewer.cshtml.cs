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
WriteLiteral("\"\r\n    }\r\n</template>\r\n\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/styles" +
".css\" />\r\n\r\n<div class=\"row\" id=\"vue-app\">\r\n    <div class=\"col-md-3 col-12\">\r\n " +
"       <div class=\"list-group\">\r\n            <vu-log-category v-for=\"item in ord" +
"eredCategories\"\r\n                             v-bind:item=\"item\"\r\n              " +
"               v-bind:key=\"item.id\"></vu-log-category>\r\n        </div>\r\n    </di" +
"v>\r\n    <div class=\"col-md-9 col-12\">\r\n        <div class=\"table-responsive\">\r\n " +
"           <table class=\"table table-responsive\">\r\n                <thead>\r\n    " +
"                <tr>\r\n                        <th>Id</th>\r\n                     " +
"   <th>Level</th>\r\n                        <th>Message</th>\r\n                   " +
"     <th>Time Ago</th>\r\n                        <th>Time</th>\r\n                 " +
"   </tr>\r\n                </thead>\r\n                <tbody>\r\n                   " +
" <tr v-for=\"item in orderedLogs\" :class=\"classObject\">\r\n                        " +
"<td>{{ item.properties.Id }}</td>\r\n                        <td>{{ item.levelForm" +
"at }}</td>\r\n                        <td>{{ item.renderedMessage }}</td>\r\n       " +
"                 <td>{{ item.timeDifferenceFormat }}</td>\r\n                     " +
"   <td>{{ item.createdFormat }}</td>\r\n                    </tr>\r\n               " +
" </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<script s" +
"rc=\"/libs/vue.js\"></script>\r\n<script src=\"/libs/vuex.js\"></script>\r\n\r\n<script ty" +
"pe=\"application/javascript\" src=\"/libs/signalr.js\"></script>\r\n<script type=\"appl" +
"ication/javascript\" src=\"/js/index.js\"></script>\r\n");


        }
    }
}
#pragma warning restore 1591
