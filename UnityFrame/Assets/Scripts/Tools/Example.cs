//CodeSuperHero 20150520
//R1    XXX 20150520 
//      xxxxxxxxx
//      xxxxxxxxx
//R2    YYY 20150520
//      yyyyyyyyy
//      yyyyyyyy

using UnityEngine;
using System.Collections;
  
//类说明以顶部为例，尽量简化信息，
//第一行用 //"name"+"Tab"+"time"作为开发者的信息
//后续更改以R+版本号为开头，用tab分隔，加上名字，时间和更改内容。

//类，结构体，接口，枚举，变量，方法的注释均以 "///" 注释。在代码中间的注释可以选择 "//"或者 "/*  */"

namespace CodeSuperHero.UF
{
    /// <summary>
    /// 结构体以大写字母"S"开头。
    /// 之所以特别区分，是因为在CSharp代码中，结构体是值类型，存储在栈上，每次传递和赋值均会被复制。所以用S开头以明显区分结构体和类。
    /// </summary>
    public struct SExample
    {
    }

    /// <summary>
    /// 枚举变量以大写字母"E"开头
    /// </summary>
    public enum EExample
    {
    }

    /// <summary>
    /// 范例接口，接口声明以大写 “I”字母开头。
    /// </summary>
    public interface IExample
    {
        void ExampleFunc(int threeExample);
    }

    /// <summary>
    /// 范例类
    /// </summary>
    public class Example : IExample
    {
        /// <summary>
        /// 常量全部字母大写，以_连接字母
        /// </summary>
        public const int EXAMPLE_ZERO = 0;

        /// <summary>
        /// 静态变量每个单词首字母大写
        /// </summary>
        public static int ExampleZero = 0;

        /// <summary>
        /// 私有变量以“_”开头，单词首字母小写,其余首字母大写
        /// </summary>
        private int _zeroExample = 0;
 
        /// <summary>
        /// 保护变量与私有变量一样
        /// </summary>
        protected int _neExample = 1;
 
        /// <summary>
        /// 公有变量首字母小写，其余单词首字母大写
        /// </summary>
        public int twoExample = 2;

        /// <summary>
        /// 属性同公有变量
        /// </summary>
        public int zeroExample
        {
            get
            {
                return _zeroExample;
            }
            set
            {
                _zeroExample = value;
            }
        }
 
        /// <summary>
        /// 公有方法每个单词均首字母大写
        /// </summary>
        /// <param name="threeExample"></param>
        public void ExampleFunc(int threeExample)
        {
        }

        /// <summary>
        /// 私有方法同公有方法
        /// </summary>
        /// <param name="threeExample"></param>
        /// <param name="fourExample"></param>
        void ExampleFunTwo(int threeExample, int fourExample)
        {
            //在程序中间注释使用"//"在待注释代码上面一行。
            //如果代码中出现有大量参数罗列，或者一行代码必须写很长的时候可以用"/*  */"在程序中直接进行注释，方便阅读。  
            //int[] indexArray = new int[5] { 1, /* 第二个值 */ 2, 3, 4, 5 }; 
        }
    }
}
