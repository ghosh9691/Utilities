using System;
namespace PyxisInt.AirCloud.Models
{
    public class Result<T> where T: class
    {
        public T Data { get; set; }
        public Error Error { get; set; }
    }
}
