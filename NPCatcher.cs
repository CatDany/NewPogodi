using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPogodi
{
    class NPCatcher
    {
        /// <summary>
        /// Инстанс NPGame, ассоциированный с этим NPCatcher.
        /// </summary>
        private readonly NPGame Game;

        /// <summary>
        /// Текущая позиция NPCatcher по координате X.
        /// </summary>
        public int XPosition = 0;

        /// <summary>
        /// Текущая позиция NPCatcher по координате Y.
        /// </summary>
        public int YPosition = 0;

        /// <summary>
        /// Ширина NPCatcher.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Высота NPCatcher.<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Вектор запланированного перемещения ("в очереди").<br/>
        /// DO NOT SET! Задается строго одним из методов передвижения и обнуляется в цикле тика (см. <see cref="Move(int)"/>)
        /// </summary>
        public Point QueuedMovement = new Point(0, 0);

        /// <summary>
        /// Создает новый инстанс NPCatcher.
        /// </summary>
        /// <param name="maxPositions">Максимальное кол-во позиций. В оригинальной игре "Ну, Погоди!" этот параметр был бы равен 4.</param>
        public NPCatcher(NPGame game, int width, int height)
        {
            this.Game = game;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Совершить попытку передвижения.<br/>
        /// Возвращает true или false, в зависимости от успешности действия.
        /// </summary>
        /// <param name="deltaX"></param>
        /// <returns></returns>
        public bool Move(int deltaX, int deltaY)
        {
            if (QueuedMovement.X != 0 || QueuedMovement.Y != 0)
                return false;

            int updatedX = XPosition + deltaX;
            if (updatedX < 0 || updatedX + Width > Game.Width)
                return false;
            int updatedY = YPosition + deltaY;
            if (updatedY < 0 || updatedY + Height > Game.Height)
                return false;

            QueuedMovement = new Point(deltaX, deltaY);
            return true;
        }
    }
}
