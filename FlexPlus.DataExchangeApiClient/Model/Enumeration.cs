// Copyright (c) World-Direct eBusiness solutions GmbH. All rights reserved.

namespace FlexPlus.DataExchangeApiClient.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides the basic functionality of enumerations.
    /// </summary>
    /// <seealso cref="System.IComparable" />
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        protected Enumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets all items of the <see cref="Enumeration"/>.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the <see cref="Enumeration"/>.</typeparam>
        /// <returns>All items of the given <see cref="Enumeration"/> of type <typeparamref name="TEnumeration"/>.</returns>
        /// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
            where TEnumeration : Enumeration
        {
            var fields = typeof(TEnumeration).GetFields(BindingFlags.Public |
                                                        BindingFlags.Static |
                                                        BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }

        /// <summary>
        /// Calculates the absolutes difference between <paramref name="firstValue"/> and <paramref name="secondValue"/>.
        /// </summary>
        /// <param name="firstValue">The first value.</param>
        /// <param name="secondValue">The second value.</param>
        /// <returns>The absolutes difference between <paramref name="firstValue"/> and <paramref name="secondValue"/>.</returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        /// <summary>
        /// Gets the item of type <typeparamref name="TEnumeration"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the <see cref="Enumeration"/>.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The item of type <typeparamref name="TEnumeration"/> with the given <paramref name="value"/>.</returns>
        public static TEnumeration FromValue<TEnumeration>(int value)
            where TEnumeration : Enumeration
        {
            var matchingItem = Parse<TEnumeration, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        /// <summary>
        /// Gets the item of type <typeparamref name="TEnumeration"/> with the given <paramref name="displayName"/>.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the <see cref="Enumeration"/>.</typeparam>
        /// <param name="displayName">The display name.</param>
        /// <returns>The item of type <typeparamref name="TEnumeration"/> with the given <paramref name="displayName"/>.</returns>
        public static TEnumeration FromDisplayName<TEnumeration>(string displayName)
            where TEnumeration : Enumeration
        {
            var matchingItem = Parse<TEnumeration, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        /// <inheritdoc />
        public override int GetHashCode() => this.Id.GetHashCode();

        /// <inheritdoc />
        public int CompareTo(object other) => this.Id.CompareTo(((Enumeration)other).Id);

        /// <inheritdoc />
        public override string ToString() => this.Name;

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        private static TEnumeration Parse<TEnumeration, TValue>(TValue value, string description, Func<TEnumeration, bool> predicate)
            where TEnumeration : Enumeration
        {
            var matchingItem = GetAll<TEnumeration>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(TEnumeration)}");
            }

            return matchingItem;
        }
    }
}
