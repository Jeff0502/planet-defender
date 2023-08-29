using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Asteroids.Audio
{
    class MusicManager
    {
        private SoundEffectInstance currentInstance;

        private event EventHandler OnFinishedPlaying;

        private TimeSpan lastTimeUpdated;

        private TimeSpan trackControl;

        private string currentTrack;

        private int currentTrackNumber = 1;

        public bool onRepeat = true;

        private Dictionary<string, SoundEffect> tracks = new();

        private float volume = 1;

        private int MaxTrack;

        public void LoadTracks(string[] TrackNames, ContentManager Content)
        {
            string currentTrackName;

            SoundEffect track;

            for(int i = 0; i < TrackNames.Count(); i++)
            {
                currentTrackName = TrackNames[i];

                track = Content.Load<SoundEffect>(currentTrackName);
                tracks.Add($"T{i + 1}", track);
            }

            MaxTrack = tracks.Count;

            OnFinishedPlaying += MusicManager_OnFinishedPlaying;

            PlayFirst();
        }

        private void MusicManager_OnFinishedPlaying(object sender, EventArgs e)
        {
            Loop();
        }

        public void Restart()
        {
            PlayFirst();
        }

        public void Update(GameTime gameTime)
        {
            FixedUpdate(gameTime);
        }

        private void FixedUpdate(GameTime gameTime)
        {
            if(gameTime.TotalGameTime - lastTimeUpdated > trackControl)
            {
                lastTimeUpdated = gameTime.TotalGameTime;
                OnFinishedPlaying(this, EventArgs.Empty);
            }
        }

        public void Play(string soundEffect)
        {
            currentInstance = tracks[soundEffect].CreateInstance();

            currentInstance.IsLooped = false;
            currentInstance.Volume = volume;

            currentInstance.Play();

            currentTrack = soundEffect;

            trackControl = tracks[currentTrack].Duration;
        }

        public void PlayFirst()
        {
            Play("T1");
        }

        public void Pause()
        {
            currentInstance.Pause();
        }

        public void Resume()
        {
            currentInstance.Play();
        }

        public void Stop()
        {
            currentInstance.Stop();

            currentInstance.Dispose();
        }

        private void Loop()
        {
            //TODO: Create a loop so that if the music stops it will loop back
            if (currentTrackNumber < MaxTrack)
            {
                Play("T" + (currentTrackNumber + 1));
            }

             else
                Play("T1");
        }
    }
}
