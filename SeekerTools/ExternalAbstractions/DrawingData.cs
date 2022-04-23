using SeekerTools.DataModels;
using System.Collections.Generic;

namespace SeekerTools.ExternalAbstractions
{
    public class DrawingData
    {

        public DrawingData(IDataAccess dataAccess, string exportLocation)
        {
            _dataAccess = dataAccess;
            _exportLocation = exportLocation;
        }

        private readonly IDataAccess _dataAccess;
        private readonly string _exportLocation;

        public string ExportLocation => _exportLocation;

        public IEnumerable<DrawingModel> GetDrawingData(string inventorId) =>
            _dataAccess.LoadData<DrawingModel, dynamic>("SelectDrawingModel", new { inventorId });
    }
}
