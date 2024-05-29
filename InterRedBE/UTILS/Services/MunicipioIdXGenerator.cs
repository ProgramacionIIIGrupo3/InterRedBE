namespace InterRedBE.UTILS.Services
{
    using InterRedBE.DAL.Context;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class MunicipioIdXGenerator : ValueGenerator<string>
    {
        public override string Next(EntityEntry entry)
        {
            var context = (InterRedContext)entry.Context;
            var lastId = context.Municipio.OrderByDescending(m => m.Id).FirstOrDefault()?.IdX;
            var newId = lastId == null ? 1 : int.Parse(lastId.Substring(1)) + 1;
            return $"M{newId:D6}";
        }

        public override bool GeneratesTemporaryValues => false;
    }

}
