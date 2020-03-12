using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPogodi
{
    class NPCatchable
    {
        /// <summary>
        /// Уникальный идентификатор NPCatchable. Используйте остаток от деления UID для определения случайных свойств падающих тел.
        /// </summary>
        public readonly int UID;

        /// <summary>
        /// Фабрика, которая создала этот NPCatchable
        /// </summary>
        public readonly NPCatchableFactory Factory;

        /// <summary>
        /// Ширина NPCatchable.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int Width;

        /// <summary>
        /// Высота NPCatchable.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int Height;

        /// <summary>
        /// Текущая позиция NPCatchable по координате X.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int XPosition = 0;

        /// <summary>
        /// Текущая позиция NPCatchable по координате Y.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int YPosition = 0;

        /// <summary>
        /// Скорость падения NPCatchable (единиц по оси Y в секунду)<br/>
        /// </summary>
        public int FallRate = 0;

        /// <summary>
        /// Возвращает награду за поимку этого NPCatchable (число очков).
        /// </summary>
        public int Reward = 0;

        /// <summary>
        /// Возвращает санкцию за пропуск этого NPCatchable (число очков).
        /// </summary>
        public int Penalty = 0;

        /// <summary>
        /// Множитель скорости падения
        /// </summary>
        public double FallRateFactor = 1.0;

        /// <summary>
        /// Инстанс NPGame, ассоциированный с этим NPCathable.
        /// </summary>
        public readonly NPGame Game;

        /// <summary>
        /// Определяет мертв ли NPCatchable.<br/>
        /// Мертвые NPCatchable не взаимодействуют, не обновляются и удаляются в конце каждого тика.
        /// </summary>
        public bool Dead = false;

        /// <summary>
        /// Создаёт новый инстанс NPCatchable с заданными координатами.<br/>
        /// По логике игры, для "рождения" новых NPCatchable следует использовать конструктор без координаты Y.
        /// </summary>
        public NPCatchable(NPGame game, NPCatchableFactory factory)
        {
            this.UID = game.Random.Next();
            this.Dead = false;
            this.Game = game;
            this.Factory = factory;
        }
    }
}
