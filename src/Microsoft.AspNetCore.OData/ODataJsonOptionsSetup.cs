// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query.Wrapper;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.OData
{
    /// <summary>
    /// Sets up default options for <see cref="JsonOptions"/>.
    /// </summary>
    public class ODataJsonOptionsSetup : IConfigureOptions<JsonOptions>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure(JsonOptions options)
        {
            if (options == null)
            {
                throw Error.ArgumentNull(nameof(options));
            }

            options.JsonSerializerOptions.Converters.Add(new SelectExpandWrapperConverter());
            options.JsonSerializerOptions.Converters.Add(new PageResultValueConverter());
            options.JsonSerializerOptions.Converters.Add(new DynamicTypeWrapperConverter());
        }
    }
}
