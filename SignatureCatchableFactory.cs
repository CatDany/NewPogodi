using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPogodi
{
    class SignatureCatchableFactory : NPCatchableFactory
    {
        public readonly Bitmap Bitmap;

        public int Width { get { return Bitmap.Width;  } }
        public int Height { get { return Bitmap.Height; } }

        /// <summary>
        /// Возвращает награду за поимку этого NPCatchable (число очков).
        /// </summary>
        public int Reward = 0;

        /// <summary>
        /// Возвращает санкцию за пропуск этого NPCatchable (число очков).
        /// </summary>
        public int Penalty = 0;

        /// <summary>
        /// Относительная частота, с которой для создания нового NPCatchable будет вызываться эта фабрика.
        /// </summary>
        public double Commonness = 1;

        /// <summary>
        /// Множитель скорости падения (например, 1.0 - в таком случае скорость падения равна скорости падения игры)
        /// </summary>
        public double FallRateFactor = 1;

        /// <summary>
        /// Активирует ли этот NPCatchable режим "extra", будучи пойманным
        /// </summary>
        public bool ActivatesExtra = false;

        /// <summary>
        /// Создает новый инстанс фабрики Catchable "подписей".
        /// </summary>
        public SignatureCatchableFactory(int reward, int penalty, double fallRateFactor, double commonness, Bitmap bitmap, double scale)
        {
            this.Bitmap = new Bitmap(bitmap, new Size((int)(bitmap.Width * scale), (int)(bitmap.Height * scale)));
            this.Reward = reward;
            this.Penalty = penalty;
            this.FallRateFactor = fallRateFactor;
            this.Commonness = commonness;
        }

        public NPCatchable Create(NPGame game)
        {
            int spawnX = game.Random.Next(0, game.Width - this.Width);
            NPCatchable spawn = new NPCatchable(game, this)
            {
                Reward = this.Reward,
                Penalty = this.Penalty,
                Width = this.Width,
                Height = this.Height,
                XPosition = spawnX,
                YPosition = -this.Height,
                FallRate = (int) game.FallRate,
                FallRateFactor = this.FallRateFactor
            };
            return spawn;
        }
        public double GetCommonness(NPGame game)
        {
            return Commonness;
        }

        /// <summary>
        /// Вызывается, когда NPCatchable пойман, перед 
        /// </summary>
        /// <param name="catchable"></param>
        public virtual void OnCatch(NPCatchable catchable)
        {
            if (ActivatesExtra && !catchable.Game.isExtraActive)
            {
                catchable.Game.ActivateExtra();
            }
        }

        /// <summary>
        /// Вызывается, когда NPCatchable пропущен
        /// </summary>
        /// <param name="catchable"></param>
        public virtual void OnMiss(NPCatchable catchable) { }
    }
}
