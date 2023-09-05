using PlayerScripts;

namespace BuildableObjects
{
    public interface IBuildCondition
    {
        public bool IsBuildable(Player player);
    }
}