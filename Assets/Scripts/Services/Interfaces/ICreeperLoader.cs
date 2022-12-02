using Creeps;

namespace Services.Interfaces
{
    public interface ICreeperLoader
    {
        Creep GetPrefabByType(CreepType creepType);
    }
}