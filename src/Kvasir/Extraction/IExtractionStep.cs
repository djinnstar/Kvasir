﻿using Kvasir.Schema;
using System;

namespace Kvasir.Extraction {
    /// <summary>
    ///   The interface describing a unit of logic that extracts and potentially decomposes one or more values out of
    ///   a source object in database-ready form.
    /// </summary>
    /// <remarks>
    ///   The <see cref="IExtractionStep"/> interface is a companion to the <see cref="IFieldExtractor"/> interface,
    ///   separating the logic for what to do with a piece of extracted data from how to obtain it. The data produced
    ///   by executing an <see cref="IExtractionStep"/> instance is fully decomposed but has not undergone any
    ///   transforms.
    /// </remarks>
    public interface IExtractionStep {
        /// <summary>
        ///   The <see cref="Type"/> of source object on which this <see cref="IExtractionStep"/> is capable of being
        ///   executed.
        /// </summary>
        Type ExpectedSource { get; }

        /// <summary>
        ///   Execute this <see cref="IExtractionStep"/> on a source object, producing an ordered sequence of values
        ///   that can be stored in a back-end database.
        /// </summary>
        /// <remarks>
        ///   Executing an <see cref="IExtractionStep"/> on a <see langword="null"/> source object will produce a
        ///   sequence of <see cref="DBValue.NULL"/> of the appropriate length. This ensures that a particular
        ///   <see cref="IExtractionStep"/> instance always produces sequences of equal length independent of the
        ///   source object's nullity.
        /// </remarks>
        /// <param name="sourceObject">
        ///   The source object on which to execute this <see cref="IExtractionStep"/>.
        /// </param>
        /// <pre>
        ///   If <paramref name="sourceObject"/> is not <see langword="null"/>, then <see cref="ExpectedSource"/> is
        ///   its dynamic type or is a base class or interface thereof.
        /// </pre>
        /// <returns>
        ///   An immutable, indexable, ordered sequence of <see cref="DBValue">database values</see> extracted from
        ///   <paramref name="sourceObject"/>.
        /// </returns>
        DBData Execute(object? sourceObject);
    }
}
