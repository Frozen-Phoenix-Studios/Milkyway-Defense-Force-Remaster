using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FrozenPhoenixStudiosUtilities
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        [SerializeField] private bool _destroyOnLoad = false;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).ToString()} is null");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;

                SetDestroyOnLoadProperty();
            }
        }

        private void SetDestroyOnLoadProperty()
        {
            if (!_destroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    public static class UIUtilities
    {
        public static IEnumerator ColourFlickerEffectIncreasing(SpriteRenderer objectToFlicker, float duration,
            Color originalColour, Color flickerColour)
        {
            //breaks the time period into 10 separate parts
            var intival = duration / 10;

            //sets a time period to half the alloted time period
            var intivalPeriod1 = Time.time + (intival * 5);

            //sets the colour of the object to the new colour for half the time period allotted
            while (Time.time < intivalPeriod1)
            {
                objectToFlicker.color = flickerColour;
                yield return new WaitForEndOfFrame();
            }

            //sets the second period to one third of the full time period
            var intivalPeriod2 = Time.time + (intival * 2);

            //slowly flickers the objects colour between the original and new colour
            while (Time.time < intivalPeriod2)
            {
                var int2FlickerSpeed = intival / 2;
                var color = objectToFlicker.color;
                color = flickerColour;
                yield return new WaitForSeconds(int2FlickerSpeed);
                color = originalColour;
                objectToFlicker.color = color;
                yield return new WaitForSeconds(int2FlickerSpeed);
            }

            //increases the flicker speed exponentially;
            var intivalPeriod3 = Time.time + (intival * 3);
            var flickerSpeed = intival / 3;
            while (Time.time < intivalPeriod3)
            {
                var color = objectToFlicker.color;
                color = flickerColour;
                yield return new WaitForSeconds(flickerSpeed);
                color = originalColour;
                objectToFlicker.color = color;
                yield return new WaitForSeconds(flickerSpeed);
                flickerSpeed /= 1.25f;
            }

            objectToFlicker.color = originalColour;
        }

        public static IEnumerator ColourRandomFlickerEffectIncreasing(SpriteRenderer objectToFlicker, float duration,
            Color originalColour)
        {
            //breaks the time period into 10 separate parts
            var interval = duration / 10;

            //sets a time period to half the alloted time period
            var intivalPeriod1 = Time.time + (interval * 5);

            //sets the colour of the object to the new colour for half the time period allotted
            while (Time.time < intivalPeriod1)
            {
                var randomColour = Random.ColorHSV();
                objectToFlicker.color = randomColour;
                yield return new WaitForEndOfFrame();
            }

            //sets the second period to one third of the full time period
            var intervalPeriod2 = Time.time + (interval * 2);

            //slowly flickers the objects colour between the original and new colour
            while (Time.time < intervalPeriod2)
            {
                var int2FlickerSpeed = interval / 2;
                var randomColour = Random.ColorHSV();

                var color = objectToFlicker.color;
                color = randomColour;
                yield return new WaitForSeconds(int2FlickerSpeed);
                color = originalColour;
                objectToFlicker.color = color;
                yield return new WaitForSeconds(int2FlickerSpeed);
            }

            //increases the flicker speed exponentially;
            var intivalPeriod3 = Time.time + (interval * 3);
            var flickerSpeed = interval / 3;
            while (Time.time < intivalPeriod3)
            {
                var randomColour = Random.ColorHSV();
                var color = objectToFlicker.color;
                color = randomColour;
                yield return new WaitForSeconds(flickerSpeed);
                color = originalColour;
                objectToFlicker.color = color;
                yield return new WaitForSeconds(flickerSpeed);
                flickerSpeed /= 1.25f;
            }

            objectToFlicker.color = originalColour;
        }

        public static IEnumerator ColourFlickerEffect(SpriteRenderer objectToFlicker, float duration,
            float flickerSpeed, Color originalColour, Color flickerColour)
        {
            bool infiniteLoop = duration == 0;
            duration = Time.time + duration;
            while (Time.time < duration && !infiniteLoop)
            {
                var color = objectToFlicker.color;
                color = flickerColour;
                yield return new WaitForSeconds(flickerSpeed);
                color = originalColour;
                objectToFlicker.color = color;
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.color = originalColour;
        }

        /// <summary>
        /// Changes the colour of the passed in object at the passed in speed.
        /// </summary>
        /// <param name="objectToFlicker"></param>
        /// <param name="duration">Set to 0 for an infinite duration</param>
        /// <param name="flickerSpeed">How fast does the colour change to the next random colour</param>
        /// <param name="originalColour">What is the original colour of the object.This value is optional.</param>
        /// <returns></returns>
        /// 
        public static IEnumerator RandomColourFlickerEffect(SpriteRenderer objectToFlicker, float duration,
            float flickerSpeed, Color originalColour = default)
        {
            bool infiniteLoop = false;

            objectToFlicker.gameObject.SetActive(true);

            if (duration == 0)
            {
                infiniteLoop = true;
            }

            while (Time.time < duration && !infiniteLoop)
            {
                Color randomColour = Random.ColorHSV();
                objectToFlicker.color = randomColour;
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.color = originalColour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToFlicker"></param>
        /// <param name="duration"></param>
        /// <param name="flickerSpeed"></param>
        /// <param name="originalColour"></param>
        /// <returns></returns>
        public static IEnumerator TextRandomColourFlickerEffect(Text objectToFlicker, float duration,
            float flickerSpeed, Color originalColour = default)
        {
            // objectToFlicker.gameObject.SetActive(true);

            if (duration == 0)
            {
                duration = Mathf.Infinity;
            }

            duration += Time.time;
            while (Time.time < duration)
            {
                Color randomColour = Random.ColorHSV();
                objectToFlicker.color = randomColour;
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.color = originalColour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToFlicker"></param>
        /// <param name="duration"></param>
        /// <param name="flickerSpeed"></param>
        /// <param name="originalColour"></param>
        /// <returns></returns>
        public static IEnumerator TextRandomColourFlickerEffect(TMP_Text objectToFlicker, float duration,
            float flickerSpeed, Color originalColour = default)
        {
             objectToFlicker.gameObject.SetActive(true);

            if (duration == 0)
            {
                duration = Mathf.Infinity;
            }

            duration += Time.time;
            while (Time.time < duration)
            {
                Color randomColour = Random.ColorHSV();
                objectToFlicker.color = randomColour;
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.color = originalColour;
        }

        /// <summary>
        /// Turns an object active status true and false at a constant rate.
        /// </summary>
        /// <param name="objectToFlicker"></param>
        /// <param name="duration">Set to 0 for infinite duration</param>
        /// <param name="flickerSpeed"></param>
        /// <param name="objectActiveAtCompletion">This will determine if the object will be active after the routine has finished</param>
        /// <returns></returns>
        public static IEnumerator ObjectFlickerEffectConstant(GameObject objectToFlicker, float duration,
            float flickerSpeed, bool objectActiveAtCompletion = true)
        {
            objectToFlicker.SetActive(true);
            if (duration == 0)
            {
                duration = Mathf.Infinity;
            }

            duration += Time.time;
            while (Time.time < duration)
            {
                objectToFlicker.SetActive(false);
                yield return new WaitForSeconds(flickerSpeed);
                objectToFlicker.SetActive(true);
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.SetActive(objectActiveAtCompletion);
            //   Debug.Log($"{objectToFlicker.transform.name} set to {objectToFlicker.gameObject.activeSelf}");
        }

        public static IEnumerator ObjectFlickerEffectConstant(TMP_Text objectToFlicker, float duration,
            float flickerSpeed, bool objectActiveAtCompletion = true)
        {
            objectToFlicker.gameObject.SetActive(true);
            if (duration == 0)
            {
                duration = Mathf.Infinity;
            }

            duration += Time.time;
            while (Time.time < duration)
            {
                objectToFlicker.gameObject.SetActive(false);
                yield return new WaitForSeconds(flickerSpeed);
                objectToFlicker.gameObject.SetActive(true);
                yield return new WaitForSeconds(flickerSpeed);
            }

            objectToFlicker.gameObject.SetActive(objectActiveAtCompletion);
        }

        /// <summary>
        /// Turns an object active status true and false at a constant rate.
        /// </summary>
        /// <param name="objectToFlicker"></param>
        /// <param name="duration">Set to 0 for infinite duration</param>
        /// <param name="flickerSpeed"></param>
        /// <param name="objectActiveAtCompletion">This will determine if the object will be active after the routine has finished</param>
        /// <returns></returns>
        public static IEnumerator ObjectFlickerEffectIncreasing(GameObject objectToFlicker, float duration,
            bool objectActiveAtCompletion = true)
        {
//        Debug.Log("flickering");
            //breaks the time period into 10 separate parts
            var interval = duration / 10;

            //sets a time period to half the alloted time period
            var intervalPeriod1 = Time.time + (interval * 5);

            //sets the status of the object active status half the time period allotted
            while (Time.time < intervalPeriod1)
            {
                objectToFlicker.SetActive(false);
                yield return new WaitForEndOfFrame();
            }

            //sets the second period to one third of the full time period
            var intervalPeriod2 = Time.time + (interval * 2);

            //slowly flickers the objects active status
            while (Time.time < intervalPeriod2)
            {
                var int2FlickerSpeed = interval / 2;
                objectToFlicker.SetActive(true);
                yield return new WaitForSeconds(int2FlickerSpeed);
                objectToFlicker.SetActive(false);
                yield return new WaitForSeconds(int2FlickerSpeed);
            }

            //increases the flicker speed exponentially;
            var intervalPeriod3 = Time.time + (interval * 3);
            var flickerSpeed = interval / 3;
            while (Time.time < intervalPeriod3)
            {
                objectToFlicker.SetActive(false);
                yield return new WaitForSeconds(flickerSpeed);
                objectToFlicker.SetActive(true);
                yield return new WaitForSeconds(flickerSpeed);
                flickerSpeed /= 1.25f;
            }

            objectToFlicker.SetActive(objectActiveAtCompletion);
        }

        public static IEnumerator TextTypeWriteEffect(Text textObject, string textToType, float typeSpeed)
        {
            textObject.gameObject.SetActive(true);
            for (int i = 0; i < (textToType.Length); i++)
            {
                //Debug.Log($"Text to type: {textToType}. This is {textToType.Length} character long. i is up to {i} which is character {textToType.Substring(i, 1)}");
                textObject.text = textToType.Substring(0, i + 1);
                var currentCharacter = textToType.Substring(i, 1);
                if (currentCharacter != " ")
                {
                    yield return new WaitForSeconds(typeSpeed);
                }
            }
        }

        public static IEnumerator TextTypeWriteEffect(TMP_Text textObject, string textToType, float typeSpeed)
        {
            textObject.gameObject.SetActive(true);
            for (int i = 0; i < textToType.Length; i++)
            {
                textObject.SetText(textToType.Substring(0, i + 1));
                var currentCharacter = textToType.Substring(i, 1);
                if (currentCharacter != " ")
                {
                    yield return new WaitForSeconds(typeSpeed);
                }
            }
        }


        public static IEnumerator CountUpText(TMP_Text textObject, String stringToType, float typingSpeed,
            int numberCountingTo)
        {
            Debug.Log("Counting text");
            textObject.gameObject.SetActive(true);
            for (int i = 0; i < numberCountingTo; i++)
            {
                textObject.SetText($"{stringToType} {i}");
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
    public static class HelperMethods
    {
        public static void FPSLogger(string message, bool isActive)
        {
            if (isActive)
            {
                Debug.Log(message);
            }
        }
        }
    public static class ConversionMethods
    {
        /// <summary>
        /// Convert int array to date time.
        /// Structured as {year, month, day}
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertArrayToDate(int[] data)
        {
            var date = new DateTime(data[0], data[1], data[2]);
            return date;
        }
        
        /// <summary>
        /// Convert date time to an int array
        /// </summary>
        /// <param name="data"></param>
        /// <returns>int array for date structured {year, month, day}</returns>
        public static int[] ConvertDateTimeToArray(DateTime data)
        {
            var intArray = new[]
            {
                data.Year,
                data.Month,
                data.Day
            };
            return intArray;
        }
        
        /// <summary>
        /// Convert UTC (seconds since 12:00 01-01-1601) to a DateTime
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DateTime structured {year, month, day}</returns>
        public static DateTime ConvertDateFromUTC(long data)
        {
            return DateTime.FromFileTimeUtc(data);
        }
        
        /// <summary>
        /// Convert a DateTime to a long - UTC (seconds since 12:00 01-01-1601)
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Long - UTC</returns>
        public static long ConvertDateToUTC(DateTime data)
        {
            return data.ToFileTimeUtc();
        }
    }
    
}

namespace FrozenPhoenixStudiosUtilities.SafeAreaUtilities
{
    public static class SafeAreaResizerHorizontal
    {
    
    }

    public static class SafeAreaResizerVertical
    {
    
    }

}
