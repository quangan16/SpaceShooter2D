using System;
using System.Collections;
using UnityEngine;


    public static class Support 
    {
        
        public static void Invoke(this MonoBehaviour mb, Action action, float delay)
        {
            mb.StartCoroutine(CoroutineDelay(action, delay));
        }

        private static IEnumerator CoroutineDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        public static void ReturnObject(this GameObject go)
        {
            go.gameObject.SetActive(false);
        }

        public static void OutScreenDestroy(this GameObject go)
        {
            if (!CheckOutsideScreen(go))
            {
                ReturnObject(go);
            }
                         
        }

        public static bool CheckOutsideScreen(this GameObject mb)
        {
            float verticalBound = Camera.main.orthographicSize + 1f;
            float horizontalBound = Camera.main.orthographicSize * Camera.main.aspect;
            if(Mathf.Abs(mb.gameObject.transform.position.x) > horizontalBound)
            {
                return false;
            }

            if (Mathf.Abs(mb.gameObject.transform.position.y) > verticalBound)
            {
                return false;
            }

            return true;
        }
            
    }
    
