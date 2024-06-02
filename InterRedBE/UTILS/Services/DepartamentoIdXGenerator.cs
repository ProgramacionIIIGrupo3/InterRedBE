using InterRedBE.DAL.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace InterRedBE.UTILS.Services
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class DepartamentoIdXGenerator : ValueGenerator<string>
    {
        public override string Next(EntityEntry entry)
        {
            var context = (InterRedContext)entry.Context;
            var lastId = context.Departamento.OrderByDescending(d => d.Id).FirstOrDefault()?.IdX;
            var newId = lastId == null ? 1 : int.Parse(lastId.Substring(1)) + 1;
            return $"D{newId:D6}";
        }

        public override bool GeneratesTemporaryValues => false;
    }

}
