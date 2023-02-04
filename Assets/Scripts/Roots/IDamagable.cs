using UnityEngine;

namespace Apollo11
{
    public interface IDamagable : IGetPosition, IGetIconPosition
    {
        public void TakeDamage(int dmg);
        public Enums.RootWeapon GetWeapon();
        
    }

    public interface IGetIconPosition
    {
        public Vector2 GetIconPosition();
    }

    public interface IGetPosition
    {
        public Vector2 GetPosition();
    }
}