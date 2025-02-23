﻿using Ardalis.GuardClauses;
using Cybele.Core;
using Cybele.Extensions;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Kvasir.Extraction {
    /// <summary>
    ///   An <see cref="IFieldExtractor"/> that performs extracting by reading from a property through reflection.
    /// </summary>
    public sealed class ReadPropertyExtractor : IFieldExtractor {
        /// <inheritdoc/>
        public Type ExpectedSource { get; }

        /// <inheritdoc/>
        public Type FieldType { get; }

        /// <summary>
        ///   Constructs a new <see cref="ReadPropertyExtractor"/>.
        /// </summary>
        /// <param name="property">
        ///   The <see cref="PropertyChain"/> describing the property (or chain or properties) from which to read.
        /// </param>
        internal ReadPropertyExtractor(PropertyChain property) {
            Guard.Against.Null(property, nameof(property));
            Debug.Assert(property.ReflectedType is not null);

            property_ = property;
            ExpectedSource = property_.ReflectedType;
            FieldType = property_.PropertyType;
        }

        /// <inheritdoc/>
        public object? Execute(object? source) {
            Debug.Assert(source is null || source.GetType().IsInstanceOf(ExpectedSource));

            if (source is null) {
                return null;
            }
            return property_.GetValue(source);
        }


        private readonly PropertyChain property_;
    }
}
