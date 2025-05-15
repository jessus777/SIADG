using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIADG.Domain.Interfaces;
public interface IConsecutiveEntity
    : IEntity
{
    long Consecutivo { get; }
}
