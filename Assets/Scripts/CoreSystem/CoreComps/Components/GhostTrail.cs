using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace MyGame.CoreSystem
{
    public class GhostTrail : CoreComponent
    {
        [SerializeField] private GameObject ghostTrailPrefab;
        public void ShowGhost(SpriteRenderer SR, int ghostNum, float ghostInterval, float fadeTime, Color ghostColor, Color fadeColor)
        {
            Sequence s = DOTween.Sequence();
            for (int i = 0; i < ghostNum; i++)
            {
                GameObject currentGhost = GameObject.Instantiate(ghostTrailPrefab);
                currentGhost.GetComponent<SpriteRenderer>().sprite = SR.sprite;
                s.AppendCallback(() =>
                {
                    currentGhost.transform.position = Core.Entity.transform.position;
                    currentGhost.transform.localScale = Core.Entity.transform.localScale;
                    currentGhost.transform.rotation = Core.Entity.transform.rotation;
                }
                );
                s.Append(currentGhost.GetComponent<SpriteRenderer>().DOColor(ghostColor, 0f));
                s.AppendCallback(() => FadeSprite(currentGhost, fadeTime, fadeColor));
                s.AppendInterval(ghostInterval);
            }
        }
        public GameObject GeneratGhost(Transform parent, Transform transformReference)
        {
            GameObject currentGhost = Instantiate(ghostTrailPrefab, transformReference.position, transformReference.rotation, parent);
            return currentGhost;
        }
        private void FadeSprite(GameObject current, float fadeTime, Color fadeColor)
        {
            current.GetComponent<SpriteRenderer>().DOKill();
            current.GetComponent<SpriteRenderer>().DOColor(fadeColor, fadeTime).onComplete += () => Destroy(current);
        }
    }
}
