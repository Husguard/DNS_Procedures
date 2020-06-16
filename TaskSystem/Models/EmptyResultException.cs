using System;
using System.Runtime.Serialization;

namespace TaskSystem.Models.Services
{
    [Serializable]
    internal class EmptyResultException : Exception
    {
        public EmptyResultException()
        {

        }
        public EmptyResultException(string message) : base(message)
        {

        }
    }
}