﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using BPService.DataObjects;
using BPService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Web.Http.Tracing;

namespace BPService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            options.MinimumTraceLevel = TraceLevel.Debug;

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnableSystemDiagnosticsTracing();

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    //public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            base.Seed(context);
        }
    }
}

