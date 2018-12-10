using System;
namespace PyxisInt.AirCloud.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }
    }
}
