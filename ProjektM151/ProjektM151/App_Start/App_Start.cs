//// -----------------------------------------------------------------------
//// <copyright file="App_Start.cs" company="Fluent.Infrastructure">
////     Copyright © Fluent.Infrastructure. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------
/// See more at: https://github.com/dn32/Fluent.Infrastructure/wiki
////-----------------------------------------------------------------------

using Fluent.Infrastructure.FluentTools;
using ProjektM151.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProjektM151.App_Start), "PreStart")]

namespace ProjektM151 {
    public static class App_Start {
        public static void PreStart() {
            FluentStartup.Initialize(typeof(ApplicationDbContext));
        }
    }
}