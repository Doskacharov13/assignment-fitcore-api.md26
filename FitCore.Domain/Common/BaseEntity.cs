using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCore.Domain.Common
{
    /// <summary>
    /// Base entity with common properties.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
