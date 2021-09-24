using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace NIBM.Procurement
{
    //
    // Summary:
    //     Exception thrown from System.Data.Entity.DbContext.SaveChanges when validating
    //     entities fails.
    public class DbEntityValidationException : DataException
    {
        //
        // Summary:
        //     Initializes a new instance of DbEntityValidationException.
        public DbEntityValidationException()
        { }

        //
        // Summary:
        //     Initializes a new instance of DbEntityValidationException.
        //
        // Parameters:
        //   message:
        //     The exception message.
        public DbEntityValidationException(string message)
        { }

        //
        // Summary:
        //     Initializes a new instance of DbEntityValidationException.
        //
        // Parameters:
        //   message:
        //     The exception message.
        //
        //   entityValidationResults:
        //     Validation results.
        public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults)
        { }

        //
        // Summary:
        //     Initializes a new instance of DbEntityValidationException.
        //
        // Parameters:
        //   message:
        //     The exception message.
        //
        //   innerException:
        //     The inner exception.
        public DbEntityValidationException(string message, Exception innerException)
        { }

        //
        // Summary:
        //     Initializes a new instance of DbEntityValidationException.
        //
        // Parameters:
        //   message:
        //     The exception message.
        //
        //   entityValidationResults:
        //     Validation results.
        //
        //   innerException:
        //     The inner exception.
        public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults, Exception innerException)
        { }

        //
        // Summary:
        //     Initializes a new instance of the DbEntityValidationException class with the
        //     specified serialization information and context.
        //
        // Parameters:
        //   info:
        //     The data necessary to serialize or deserialize an object.
        //
        //   context:
        //     Description of the source and destination of the specified serialized stream.
        protected DbEntityValidationException(SerializationInfo info, StreamingContext context)
        { }

        //
        // Summary:
        //     Validation results.
        public IEnumerable<DbEntityValidationResult> EntityValidationErrors { get; }

        //
        // Summary:
        //     Sets the System.Runtime.Serialization.SerializationInfo with information about
        //     the exception.
        //
        // Parameters:
        //   info:
        //     The System.Runtime.Serialization.SerializationInfo that holds the serialized
        //     object data about the exception being thrown.
        //
        //   context:
        //     The System.Runtime.Serialization.StreamingContext that contains contextual information
        //     about the source or destination.
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        { }
    }

    //
    // Summary:
    //     Represents validation results for single entity.
    public class DbEntityValidationResult
    {
        //
        // Summary:
        //     Creates an instance of System.Data.Entity.Validation.DbEntityValidationResult
        //     class.
        //
        // Parameters:
        //   entry:
        //     Entity entry the results applies to. Never null.
        //
        //   validationErrors:
        //     List of System.Data.Entity.Validation.DbValidationError instances. Never null.
        //     Can be empty meaning the entity is valid.
        public DbEntityValidationResult(DbEntityEntry entry, IEnumerable<DbValidationError> validationErrors)
        {
            Entry = entry;
            ValidationErrors = validationErrors.ToList();
        }

        //
        // Summary:
        //     Gets an instance of System.Data.Entity.Infrastructure.DbEntityEntry the results
        //     applies to.
        public DbEntityEntry Entry { get; }
        //
        // Summary:
        //     Gets validation errors. Never null.
        public ICollection<DbValidationError> ValidationErrors { get; }
        //
        // Summary:
        //     Gets an indicator if the entity is valid.
        public bool IsValID { get; }
    }

    //
    // Summary:
    //     Validation error. Can be either entity or property level validation error.
    public class DbValidationError
    {
        //
        // Summary:
        //     Creates an instance of System.Data.Entity.Validation.DbValidationError.
        //
        // Parameters:
        //   propertyName:
        //     Name of the invalid property. Can be null.
        //
        //   errorMessage:
        //     Validation error message. Can be null.
        public DbValidationError(string propertyName, string errorMessage)
        { }

        //
        // Summary:
        //     Gets name of the invalid property.
        public string PropertyName { get; }
        //
        // Summary:
        //     Gets validation error message.
        public string ErrorMessage { get; }
    }

    public class DbEntityEntry
    {
    }
}
