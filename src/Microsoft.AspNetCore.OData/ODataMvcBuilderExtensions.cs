// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.OData
{
    /// <summary>
    /// Provides extension methods to add OData services.
    /// </summary>
    public static class ODataMvcBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddOData(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            ODataMvcCoreBuilderExtensions.AddODataCore(builder.Services);

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IMvcBuilder AddOData(this IMvcBuilder builder, Action<ODataOptions> setupAction)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            if (setupAction == null)
            {
                throw Error.ArgumentNull(nameof(setupAction));
            }

            ODataMvcCoreBuilderExtensions.AddODataCore(builder.Services);

            builder.Services.Configure(setupAction);

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddODataJson(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw Error.ArgumentNull(nameof(builder));
            }

            ODataMvcCoreBuilderExtensions.AddODataJsonCore(builder.Services);

            return builder;
        }
    }
}
