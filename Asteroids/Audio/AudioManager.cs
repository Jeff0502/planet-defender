using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Audio
{
    class AudioManager
    {
        private Dictionary<string, SoundEffect> soundEffects;

        public AudioManager(Dictionary<string, SoundEffect> _soundEffects)
        {
            soundEffects = new Dictionary<string, SoundEffect>(_soundEffects);
        }

        public void PlayAudio(SoundEffect soundEffect)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

            soundEffectInstance.IsLooped = false;

            soundEffectInstance.Play();
        }

        public void PlayAudio(string SoundEffectName)
        {
            SoundEffectInstance soundEffectInstance = soundEffects[SoundEffectName].CreateInstance();

            soundEffectInstance.IsLooped = false;

            soundEffectInstance.Volume = 0.25f;

            soundEffectInstance.Play();
        }
    }
}
