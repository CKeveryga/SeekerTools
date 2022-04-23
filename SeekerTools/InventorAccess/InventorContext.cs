using SeekerTools.DataModels;
using SeekerTools.InventorModels;
using System.Collections.Generic;

namespace SeekerTools.InventorAccess
{
    public class InventorContext : IInventorContext
    {
        public InventorContext()
        {
            _assemblies = new List<IInvAsmDoc>();
            _parts = new List<IInvPartDoc>();
            _drawings = new List<IDrawingModel>();
        }
        private IList<IInvAsmDoc> _assemblies;
        private IList<IInvPartDoc> _parts;
        private IList<IDrawingModel> _drawings;
        /// <summary>
        /// All drawings exported for an assembly
        /// </summary>
        public IList<IDrawingModel> Drawings => _drawings;
        /// <summary>
        /// All assemblies
        /// </summary>
        public IList<IInvAsmDoc> Assemblies => _assemblies;
        /// <summary>
        /// All parts
        /// </summary>
        public IList<IInvPartDoc> Parts => _parts;

        public void AddAssembly(IInvAsmDoc invAsmDoc) => _assemblies.Add(invAsmDoc);

        public void AddPart(IInvPartDoc invPartDoc) => _parts.Add(invPartDoc);

        public void AddDrawing(IDrawingModel drawing) => _drawings.Add(drawing);
    }
}
