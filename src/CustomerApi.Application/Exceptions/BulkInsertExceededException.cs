using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Exceptions
{

    /// <summary>
    /// Exception thrown when too many records are attempted to be inserted in a bulk operation, exceeding the allowed limit.
    /// </summary>
    public class BulkInsertExceededException : Exception
    {
        public BulkInsertExceededException(string exceptionMessage) : base(exceptionMessage)
        {

        }

    }
}
