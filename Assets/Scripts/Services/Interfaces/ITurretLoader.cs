using Turrets;

namespace Services.Interfaces
{
    public interface ITurretLoader
    {
        TurretLoadableData GetTurretByType(TurretType turretType);
    }
}