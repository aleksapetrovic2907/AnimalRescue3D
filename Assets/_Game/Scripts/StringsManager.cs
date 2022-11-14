using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class StringsManager : MonoBehaviour
    {
        /// <summary>Shortens numbers with suffix.</summary>
        public static string ShortNotation(int number)
        {
            if(number >= 1000000)
                return (number / 1000000D).ToString("0.#M");

            if(number >= 1000)
                return (number / 1000D).ToString("0.#K");

            return number.ToString();
        }
    }
}
