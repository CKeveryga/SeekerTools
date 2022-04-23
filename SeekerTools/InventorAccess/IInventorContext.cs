using SeekerTools.DataModels;
using SeekerTools.InventorModels;
using System.Collections.Generic;

namespace SeekerTools.InventorAccess
{
    public interface IInventorContext
    {
        IList<IInvAsmDoc> Assemblies { get; }
        IList<IInvPartDoc> Parts { get; }
        IList<IDrawingModel>Drawings { get; }
        void AddAssembly(IInvAsmDoc invAsmDoc);
        void AddPart(IInvPartDoc invPartDoc);
        void AddDrawing(IDrawingModel drawing);
    }
}