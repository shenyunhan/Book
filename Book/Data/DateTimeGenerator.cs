using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Book.Data
{
    internal sealed class DateTimeGenerator : ValueGenerator<DateTime>
    {
        /// <inheritdoc />
        public override bool GeneratesTemporaryValues => false;

        /// <inheritdoc />
        public override DateTime Next(EntityEntry entry)
        {
            return DateTime.Now;
        }
    }
}
