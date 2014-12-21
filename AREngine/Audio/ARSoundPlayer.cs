using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using AREngine.Cameras;

namespace AREngine.Audio
{
    public static class ARSoundPlayer
    {
        public static void Play3DSound(SoundEffect e, Vector3 position,IARCamera camera)
        {
            SoundEffectInstance i = e.CreateInstance();
            Play3DSound(i, position,camera);

        }
        public static  void Play3DSound(SoundEffectInstance e, Vector3 position, IARCamera camera)
        {

            AudioListener al = new AudioListener();


            al.Forward = Vector3.Normalize(camera.LookAt - camera.Position);
            al.Up = Vector3.Normalize(camera.Up);
            al.Position = camera.Position;
            AudioEmitter ae = new AudioEmitter();
            ae.Position = (position - camera.Position) + camera.Position;
            //ae.Position = position;

            e.Apply3D(al, ae);
            //if (e.State == SoundState.Stopped)
            //{

            e.Play();
            //}
        }

    }
}
