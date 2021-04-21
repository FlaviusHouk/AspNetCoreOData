// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Abstracts;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Parser;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;

namespace Microsoft.AspNetCore.OData
{
    /// <summary>
    /// Provides extension methods to add OData services.
    /// </summary>
    public static class ODataMvcCoreBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcCoreBuilder AddOData(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            AddODataCore(builder.Services);

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IMvcCoreBuilder AddOData(this IMvcCoreBuilder builder, Action<ODataOptions> setupAction)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            if (setupAction == null)
            {
                throw Error.ArgumentNull(nameof(setupAction));
            }

            AddODataCore(builder.Services);

            builder.Services.Configure(setupAction);

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcCoreBuilder AddODataJson(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            AddODataJsonCore(builder.Services);

            return builder;
        }

        internal static void AddODataJsonCore(IServiceCollection services)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<JsonOptions>, ODataJsonOptionsSetup>());
        }

        internal static void AddODataCore(IServiceCollection services)
        {
            //
            // Options
            //
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<ODataOptions>, ODataOptionsSetup>());

            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, ODataMvcOptionsSetup>());

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IODataQueryRequestParser, DefaultODataQueryRequestParser>());

            services.TryAddSingleton<IAssemblyResolver, DefaultAssemblyResolver>();

            services.TryAddSingleton<IODataTypeMappingProvider, ODataTypeMappingProvider>();

            // Routing
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IApplicationModelProvider, ODataRoutingApplicationModelProvider>());

            services.TryAddEnumerable(ServiceDescriptor.Singleton<MatcherPolicy, ODataRoutingMatcherPolicy>());

            services.TryAddSingleton<IODataTemplateTranslator, DefaultODataTemplateTranslator>();
            services.TryAddSingleton<IODataPathTemplateParser, DefaultODataPathTemplateParser>();
        }
    }
}
