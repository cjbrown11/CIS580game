using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LilManGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LilManGame
{
    public class BirdkillParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Crimson,
            Color.Aqua,
            Color.Orange,
            Color.Yellow,
            Color.DarkBlue,
            Color.DarkGreen
        };

        Color color;

        public BirdkillParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 5) { }

        protected override void InitializeConstants()
        {
            textureFilename = "birdwing";

            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);

            var lifetime = RandomHelper.NextFloat(1, 1.5f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = 100000;

            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);

            particle.Scale = 0.5f + .255f * normalizedLifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}
