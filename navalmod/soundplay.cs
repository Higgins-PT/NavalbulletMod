using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
namespace Navalmod
{
    public class soundplay: SingleInstance<soundplay>
    {
        public override string Name => throw new NotImplementedException();
        public AudioSource explosionvoice;
        public Navalhe navalhe;
        public GameObject gameObject;
        public void Start()
        {
            if (gameObject == null)
            {
                gameObject = new GameObject();

                this.explosionvoice = gameObject.AddComponent<AudioSource>();
                this.explosionvoice.spatialBlend = 1f;
                this.explosionvoice.rolloffMode = AudioRolloffMode.Linear;
                this.explosionvoice.playOnAwake = false;
                this.explosionvoice.loop = false;
                navalhe = UnityEngine.Object.FindObjectOfType<Navalhe>();
            }
        }
        public void explosionsound(float tntmass,Vector3 pos)
        {
            
            gameObject.transform.position = pos;
            
            if (tntmass >= 30f)
            {
                AudioSource.PlayClipAtPoint(Soundfiles.explosionbigfar2[UnityEngine.Random.Range(1, 4)], pos, navalhe.volume * tntmass / 60f);
                AudioSource.PlayClipAtPoint(Soundfiles.explosionbigfar[UnityEngine.Random.Range(1, 4)], pos, navalhe.volume * tntmass / 60f);
            }
            if (tntmass >= 1f)
            {
                AudioSource.PlayClipAtPoint(Soundfiles.explosionmid[UnityEngine.Random.Range(1, 4)], pos, navalhe.volume * tntmass / 60f);
                AudioSource.PlayClipAtPoint(Soundfiles.explosionmidfar[UnityEngine.Random.Range(1, 4)], pos, navalhe.volume * tntmass / 60f);

            }
            if (tntmass < 1f)
            {
                AudioSource.PlayClipAtPoint(Soundfiles.shootexplosion[UnityEngine.Random.Range(7, 12)], pos, navalhe.volume * tntmass / 60f);

            }
        }
    }
}
