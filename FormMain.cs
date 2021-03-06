﻿using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPogodi
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Текущий инстанс NPGame.
        /// </summary>
        NPGame Game = null;

        List<SignatureCatchableFactory> Signatures = new List<SignatureCatchableFactory>();

        public FormMain()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            InitializeComponent();

            gameTimer.Interval = Int32.Parse(Resources.Constants.TICK_INTERVAL);

            double scale = 0.3;
            int reward = Int32.Parse(Resources.Constants.REWARD);
            int penalty = Int32.Parse(Resources.Constants.PENALTY);
            double extraSignatureProbability = Double.Parse(Resources.Constants.EXTRA_SIGNATURE_PROBABILITY);
            double commonSignatureCommonness = (1.0 / extraSignatureProbability - 1) / 1.0;
            double extraSignatureCommonnness = 1.0;
            double extraSignatureFallRateFactor = Double.Parse(Resources.Constants.EXTRA_SIGNATURE_FALL_RATE_FACTOR);
            Signatures.Add(new SignatureCatchableFactory(reward, penalty, 1.0, commonSignatureCommonness, Properties.Resources.RegularEgg, scale));
            Signatures.Add(new SignatureCatchableFactory(0, 0, extraSignatureFallRateFactor, extraSignatureCommonnness, Properties.Resources.GoldenEgg, scale * 2) { ActivatesExtra = true });

            buttonNewGame.BackColor = buttonAI.BackColor = buttonExit.BackColor = Color.FromArgb(200, 255, 255, 255);
            labelHighscore.BackColor = labelHighscoreDesc.BackColor = Color.FromArgb(150, 255, 255, 255);
            labelHighscore.Text = String.Format(labelHighscore.Tag as string, Properties.Settings.Default.Highscore);
        }

        /// <summary>
        /// Ориентация волка (1 = вправо, -1 = влево)
        /// </summary>
        private int orientation = 1;
        /// <summary>
        /// Если true, волк повернется при следующем обновлении, а флаг вернется к исходному значению (false).
        /// </summary>
        private bool flipOrientation = false;

        private void handleKeyDown(object sender, KeyEventArgs e)
        {
            if (Game != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                {
                    Game.Catcher.Move(-Game.Catcher.Width, 0);
                    if (orientation > 0)
                    {
                        orientation = -1;
                        flipOrientation = true;
                    }
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                {
                    Game.Catcher.Move(+Game.Catcher.Width, 0);
                    if (orientation < 0)
                    {
                        orientation = 1;
                        flipOrientation = true;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    panelMainMenu.Visible = true;
                    panelGame.Visible = false;
                    Game = null;
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (Game != null)
            {
                Game.tick();

                pictureCatcher.Location = new Point(Game.Catcher.XPosition, Game.Catcher.YPosition);
                if (flipOrientation)
                {
                    flipOrientation = false;
                    pictureCatcher.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                if (Game.CurrentScore != 0 || !labelScore.Tag.Equals("Untouched"))
                {
                    if (labelScore.Tag as string != "" + Game.CurrentScore)
                    {
                        labelScore.Tag = labelScore.Text;
                        labelScore.Text = "" + Game.CurrentScore;

                        ScoreSizeAnimationDuration = Math.Max(
                            ScoreSizeAnimationDurationFastest,
                            ScoreSizeAnimationDurationStarting / Math.Log(Game.FallRate, StartingFallRate)
                            );
                        StartScoreAnimation();

                        if (Properties.Settings.Default.Highscore <= Game.CurrentScore)
                        {
                            Properties.Settings.Default.Highscore = Game.CurrentScore;
                            Properties.Settings.Default.Save();
                        }
                    }
                    
                }

                // Перерисовываем компоненты
                panelGame.Invalidate();

                Color backColor = Game.isExtraActive ? Color.LightPink : Color.White;
                if (panelGame.BackColor != backColor)
                {
                    if (Game.isExtraActive)
                        ShowTip(Resources.Locale_ru.TIP_EXTRA_START);
                    else
                        ShowTip(Resources.Locale_ru.TIP_EXTRA_END);

                    panelGame.BackColor = backColor;
                }

                // update visuals (according to post-update NPGame parameters)
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            panelMainMenu.Visible = false;
            panelGame.Visible = true;
            Game = new NPGame()
            {
                CatchableSpawnRate = Double.Parse(Resources.Constants.SPAWN_RATE),
                TickRate = 1000 / gameTimer.Interval,
                Width = panelGame.Width,
                Height = panelGame.Height,
                FallRate = StartingFallRate,
                FallRateAcceleration = Double.Parse(Resources.Constants.FALL_RATE_ACCELERATION),
                ExtraFallRateFactor = Double.Parse(Resources.Constants.EXTRA_FALL_RATE_FACTOR),
                ExtraSpawnRateFactor = Double.Parse(Resources.Constants.EXTRA_SPAWN_RATE_FACTOR),
                ExtraBonusPointsPerSecond = Double.Parse(Resources.Constants.EXTRA_BONUS_POINTS_PER_SECOND)
            };

            Game.Catcher = new NPCatcher(Game, pictureCatcher.Width, pictureCatcher.Height)
            {
                XPosition = pictureCatcher.Location.X,
                YPosition = pictureCatcher.Location.Y
            };
            Game.AddCatchableFactories(Signatures);

            string[] checkpoints = Resources.Constants.CHECKPOINTS.Split(',');
            foreach (string s in checkpoints)
            {
                Game.Checkpoints.Add(Int32.Parse(s));
            }
        }

        private void panelGame_Paint(object sender, PaintEventArgs e)
        {
            foreach (NPCatchable c in Game.Catchables)
            {
                Bitmap cbm = (c.Factory as SignatureCatchableFactory).Bitmap;
                ImageAttributes imgAttr = new ImageAttributes();
                if (c.YPosition + c.Height > Game.Catcher.YPosition + Game.Catcher.Height / 2)
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = 1.0f - (c.YPosition + c.Height - (Game.Catcher.YPosition + Game.Catcher.Height / 2)) / (float)(Game.Height - Game.Catcher.YPosition + Game.Catcher.Height / 2) * 2;
                    imgAttr.SetColorMatrix(matrix);
                }
                e.Graphics.DrawImage(cbm, new Rectangle(c.XPosition, c.YPosition, cbm.Width, cbm.Height), 0, 0, cbm.Width, cbm.Height, GraphicsUnit.Pixel, imgAttr);
            }
        }

        private double ScoreSize
        {
            get
            {
                double timeNow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0;
                double eased = easeOut(timeNow - ScoreSizeAnimationStartTime, ScoreSizeMax, ScoreSizeMax - ScoreSizeDefault, ScoreSizeAnimationDuration);
                return eased;
            }
        }

        private double ScoreSizeAnimationStartTime = 0;
        private double ScoreSizeAnimationDurationStarting = Double.Parse(Resources.Constants.SCORE_SIZE_ANIMATION_DURATION);
        private double ScoreSizeAnimationDuration = Double.Parse(Resources.Constants.SCORE_SIZE_ANIMATION_DURATION);
        private double ScoreSizeAnimationDurationFastest = Double.Parse(Resources.Constants.SCORE_SIZE_ANIMATION_DURATION_FASTEST);
        private double ScoreSizeDefault = Double.Parse(Resources.Constants.DEFAULT_SCORE_FONT_SIZE);
        private double ScoreSizeMax = Double.Parse(Resources.Constants.MAX_SCORE_FONT_SIZE);
        private double StartingFallRate = Int32.Parse(Resources.Constants.FALL_RATE_STARTING);

        /// <summary>
        /// Начинает анимацию изменения текущего счёта
        /// </summary>
        public void StartScoreAnimation()
        {
            ScoreSizeAnimationStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0;
        }

        private double TipStartTime = 0;
        private double TipHoldDuration = Double.Parse(Resources.Constants.TIP_DURATION);
        private double TipFadeOutDuration = Double.Parse(Resources.Constants.TIP_FADE_OUT_DURATION);

        /// <summary>
        /// Показать подсказку на экране.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void ShowTip(string format, params string[] args)
        {
            labelTip.Visible = true;
            labelTip.ForeColor = SystemPens.ControlDarkDark.Color;
            labelTip.Text = String.Format(format, format);
            TipStartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0;
        }

        /// <summary>
        /// Интерполяция по времени (убывающая кубическая кривая)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="startValue"></param>
        /// <param name="change"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private double easeOut(double t, double b, double c, double d)
        {
            t = Math.Min(t / d, 1);
            return -c * t * t * t + b;
        }

        /// <summary>
        /// Интерполяция по времени (возрастающая квадратичная кривая)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="startValue"></param>
        /// <param name="change"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private double easeIn(double t, double b, double c, double d)
        {
            t /= d;
            t = Math.Min(t, 1);
            return c * t * t + b;
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            double timeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (timeNow - ScoreSizeAnimationStartTime <= ScoreSizeAnimationDuration)
            {
                labelScore.Font = new Font(labelScore.Font.FontFamily, (float)ScoreSize);
                labelScore.Invalidate();
            }
            if (timeNow - TipStartTime > TipHoldDuration)
            {
                int transparency = (int)easeIn(timeNow - TipStartTime - TipHoldDuration, SystemPens.ControlDarkDark.Color.R, 255 - SystemPens.ControlDarkDark.Color.R, TipFadeOutDuration);
                labelTip.ForeColor = Color.FromArgb(transparency, transparency, transparency);
                if (transparency == 255)
                {
                    labelTip.Visible = false;
                }
                labelScore.Invalidate();
            }
        }

        private void buttonAI_Click(object sender, EventArgs e)
        {
            timerAI.Enabled = true;
            buttonNewGame_Click(sender, e);
        }

        private void timerAI_Tick(object sender, EventArgs e)
        {
            if (Game != null)
            {
                NPCatchable target = null;
                double targetTicks = 0;
                foreach (NPCatchable c in Game.Catchables)
                {
                    if (c.Dead)
                        continue;

                    double fallDistancePerTick = (int)(c.FallRate * c.FallRateFactor) / Game.TickRate;
                    double fallDistanceLeft = Game.Height - Game.Catcher.Height - c.YPosition;
                    double fallTicks = fallDistanceLeft / fallDistancePerTick;
                    double moveTicks = (double)timerAI.Interval / Game.TickRate;

                    if (target == null
                        || (c.Factory is SignatureCatchableFactory && ((SignatureCatchableFactory)c.Factory).ActivatesExtra)
                        || targetTicks > fallTicks + moveTicks)
                    {
                        targetTicks = fallTicks + moveTicks;
                        target = c;
                    }
                }

                if (target != null)
                {
                    if (Game.Catcher.XPosition > target.XPosition)
                    {
                        handleKeyDown(sender, new KeyEventArgs(Keys.Left));
                    }
                    else if (Game.Catcher.XPosition + Game.Catcher.Width < target.XPosition)
                    {
                        handleKeyDown(sender, new KeyEventArgs(Keys.Right));
                    }
                }
            }
        }
    }
}