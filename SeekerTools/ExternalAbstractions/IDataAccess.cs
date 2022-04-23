using System.Collections.Generic;

namespace SeekerTools.ExternalAbstractions
{
    public interface IDataAccess
    {

        IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters);
    }
}