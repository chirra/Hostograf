﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Tester
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	internal class TestProduct_TCP : ITestProduct
	{

        private string _IPAddress;
	    private string _Port;

        public TestProduct_TCP(string IPAddress, string Port)
	    {
	        _IPAddress = IPAddress;
            _Port = Port;

	    }
	  
	    public bool Check()
        {
            Console.WriteLine("Send TCP packets on " + _IPAddress + ":" + _Port);
            return true;

        }

    }
}

