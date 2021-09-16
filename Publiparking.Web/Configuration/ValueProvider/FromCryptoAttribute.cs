﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Publiparking.Web.Configuration.ValueProvider
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromCryptoAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => CryptoBindingSource.Crypto;
    }
}
