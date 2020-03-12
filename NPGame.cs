using System;
using System.Collections.Generic;
using System.Drawing;

namespace NewPogodi
{
    class NPGame
    {
        public Random Random = new Random();

        /// <summary>
        /// Текущие очки
        /// </summary>
        public int CurrentScore
        {
            get { return currentScore; }
            set
            {
                int oldScore = currentScore;
                currentScore = Math.Max(LastCheckpoint, value);
                FallRate += (currentScore - oldScore) * FallRateAcceleration * ExtraFallRateFactor;
            }
        }

        private int currentScore = 0;

        /// <summary>
        /// Последний достигнутый чекпоинт (см. <see cref="Checkpoints"/>)
        /// </summary>
        public int LastCheckpoint = 0;

        /// <summary>
        /// Время текущей игры в соответствии с кол-вом исполненных тиков<br/>
        /// Если все обновления (тики) произошли вовремя без отставаний, то этот параметр численно равен разности таймстампа "сейчас" и таймстампа начала игры.
        /// </summary>
        public double SecondsElapsed = 0;

        /// <summary>
        /// NPCatcher, ассоциированный с этой игрой
        /// </summary>
        public NPCatcher Catcher;

        /// <summary>
        /// Список текущих NPCatchable
        /// </summary>
        public List<NPCatchable> Catchables = new List<NPCatchable>();

        /// <summary>
        /// Суммарная commonness всех фабрик NPCatchable
        /// </summary>
        private double TotalCatchableFactoryCommonness = 0;
        
        /// <summary>
        /// Список фабрик NPCatchable
        /// </summary>
        private List<NPCatchableFactory> CatchableFactories = new List<NPCatchableFactory>();

        /// <summary>
        /// Ширина игрового поля<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int Width;

        /// <summary>
        /// Высота игрового поля<br/>
        /// Разумно использовать кол-во пикселей, отображаемых на панели. В таком случае, точность вычислений коллизий равна точности видимой пользователю.
        /// </summary>
        public int Height;

        /// <summary>
        /// Скорость падения
        /// </summary>
        public double FallRate = 100;

        /// <summary>
        /// Ускорение падения (единиц системы измерения за 1 очко игрового счёта)
        /// </summary>
        public double FallRateAcceleration = 0;

        /// <summary>
        /// Число обновлений (тиков) игры в секунду.
        /// </summary>
        public int TickRate;

        /// <summary>
        /// Скорость появления NPCatchable (единиц в секунду)
        /// </summary>
        public double CatchableSpawnRate = 0;

        /// <summary>
        /// Список чекпоинтов (в порядке возрастания!), преодолев которые счёт уже не может упасть ниже
        /// </summary>
        public List<Int32> Checkpoints = new List<Int32>();

        /// <summary>
        /// Создает новый инстанс NPGame.
        /// </summary>
        /// <param name="gameWidth">Ширина игрового поля</param>
        /// <param name="gameHeight">Высота игрового поля</param>
        /// <param name="catcherWidth">Ширина NPCatcher</param>
        /// <param name="catcherHeight">Высота NPCatcher</param>
        /// <param name="catcherX">Положение NPCatcher по оси X</param>
        /// <param name="catcherY">Положение NPCatcher по оси Y</param>
        /// <param name="catchableSpawnRate">Скорость появления NPCatchable (единиц в секунду)</param>
        public NPGame() { }

        public void tick()
        {
            // Завершение маневра NPCatcher "в очереди"
            Catcher.XPosition += Catcher.QueuedMovement.X;
            Catcher.YPosition += Catcher.QueuedMovement.Y;
            Catcher.QueuedMovement = new System.Drawing.Point(0, 0);

            // Появление новых NPCatchable
            if (Random.NextDouble() < CatchableSpawnRate / TickRate)
            {
                double rand = Random.NextDouble() * TotalCatchableFactoryCommonness;
                double i = 0;
                // Взвешанный выбор фабрики по commonness
                foreach (NPCatchableFactory factory in CatchableFactories)
                {
                    i += factory.GetCommonness(this);
                    if (i > rand)
                    {
                        Catchables.Add(factory.Create(this));
                        break;
                    }
                }
            }

            // Падение
            foreach (NPCatchable c in Catchables)
            {
                c.YPosition += (int)(c.FallRate * c.FallRateFactor) / TickRate;
            }

            // Проверка на коллизии
            Rectangle catcherRect = new Rectangle(Catcher.XPosition, Catcher.YPosition, Catcher.Width, Catcher.Height);
            foreach (NPCatchable c in Catchables)
            {
                // Если NPCatchable вышел за пределы игровой зоны
                if (c.YPosition > Height)
                {
                    c.Dead = true;
                    c.Factory.OnMiss(c);
                    CurrentScore -= c.Penalty;

                    if (isExtraActive)
                        DeactivateExtra();
                }

                // Если NPCatchable столкнулся с NPCatcher
                Rectangle catchableRect = new Rectangle(c.XPosition, c.YPosition, c.Width, c.Height);
                if (catchableRect.IntersectsWith(catcherRect))
                {
                    c.Dead = true;
                    c.Factory.OnCatch(c);
                    CurrentScore += c.Reward;

                    if (isExtraActive)
                        CurrentScore += (int)((SecondsElapsed - ExtraActivationTime) * ExtraBonusPointsPerSecond);
                }
            }

            // Проверка на отрицательный счёт (признак завершения игры)
            if (CurrentScore < 0)
            {
                // game over logic
            }
            
            // Проверка счёта на чекпоинты
            for (int i = Checkpoints.Count - 1; i >= 0; i--)
            {
                if (CurrentScore >= Checkpoints[i])
                {
                    LastCheckpoint = Checkpoints[i];
                    break;
                }
            }

            // Удаление мертвых NPCatchable
            Catchables.RemoveAll(c => c.Dead);

            // Счёт времени
            SecondsElapsed += 1.0 / TickRate;

            // handle the rest of tick logic
        }

        /// <summary>
        /// Добавить фабрику NPCatchable
        /// </summary>
        /// <param name="factory"></param>
        public void AddCatchableFactory(NPCatchableFactory factory)
        {
            double commonness = factory.GetCommonness(this);
            TotalCatchableFactoryCommonness += commonness;
            CatchableFactories.Add(factory);
        }

        /// <summary>
        /// Добавить несколько фабрик NPCatchable
        /// </summary>
        /// <param name="factories"></param>
        public void AddCatchableFactories(IEnumerable<NPCatchableFactory> factories)
        {
            foreach (NPCatchableFactory factory in factories)
            {
                AddCatchableFactory(factory);
            }
        }

        /* Настройки относящиеся к так называемому режиму "extra".
         * 
         * Режим активируется, когда NPCatcher ловит особо редкий NPCatchable - активатор этого режима.
         * Одновременно с активацией режима, все текущие NPCatchable удаляются с игрового поля.
         * 
         * Пока режим активирован, увеличивается скорость появления и падения NPCatchable, начисляется больше очков.
         * Чем дольше 
         * 
         * По окончанию действия, все параметры возвращаются к прежним значениям.
         */

        /// <summary>
        /// Момент времени, когда был активирован режим "extra" ("нулём" отсчёта является <see cref="SecondsElapsed"/>)
        /// </summary>
        private double ExtraActivationTime = -1;

        /// <summary>
        /// Статус активности режима "extra"
        /// </summary>
        public bool isExtraActive = false;

        /// <summary>
        /// Доп
        /// </summary>
        public double ExtraBonusPointsPerSecond = 0;

        /// <summary>
        /// Множитель скорости появления NPCatchable в режиме "extra"
        /// </summary>
        public double ExtraSpawnRateFactor = 1;

        /// <summary>
        /// Множитель скорости падения NPCatchable в режиме "extra"
        /// </summary>
        public double ExtraFallRateFactor = 1;

        /// <summary>
        /// Активирует режим "extra"
        /// </summary>
        public void ActivateExtra()
        {
            Catchables.ForEach(c => c.Dead = true);
            CatchableSpawnRate *= ExtraSpawnRateFactor;
            FallRate *= ExtraFallRateFactor;

            isExtraActive = true;
            ExtraActivationTime = SecondsElapsed;
        }

        /// <summary>
        /// Деактивирует режим "extra"
        /// </summary>
        public void DeactivateExtra()
        {
            Catchables.ForEach(c => c.Dead = true);
            CatchableSpawnRate /= ExtraSpawnRateFactor;
            FallRate /= ExtraFallRateFactor;

            isExtraActive = false;
            ExtraActivationTime = -1;
        }
    }
}
