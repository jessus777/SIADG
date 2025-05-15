using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIADG.Application.Contracts.Interfaces;
public interface IUnitOfWorkFactory
{
    T Create<T>() where T : IUnitOfWork;
}
