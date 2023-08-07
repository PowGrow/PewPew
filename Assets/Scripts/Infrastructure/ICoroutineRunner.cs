using System.Collections;
using UnityEngine;

namespace Pewpew.Infrastructure
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}