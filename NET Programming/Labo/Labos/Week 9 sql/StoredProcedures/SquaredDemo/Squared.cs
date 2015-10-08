//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SquaredDemo
{
    public class Squared
    {
        private SqlInt64 _number, _squared;

        public Squared(SqlInt64 number, SqlInt64 squared)
        {
            _number = number;
            _squared = squared;
        }

        [SqlFunction(FillRowMethodName="FillRows", TableDefinition="number bigint, squared bigint")]
        public static IEnumerable SquareNumbers(int maxNumber)
        {
            try
            {
                ArrayList squaredNumbers = new ArrayList();
                for (int i = 1; i < maxNumber; i++)
                    squaredNumbers.Add(new Squared(i, i * i));

                return squaredNumbers;
            } catch(Exception ex)
            {
                return null;
            }
        }

        //fill rows argumenten moeten overeenstemmen met het attribuut!
        public static void FillRows(object squaredObject, out SqlInt64 number, out SqlInt64 squared)
        {
            number = ((Squared)squaredObject)._number;
            squared = ((Squared)squaredObject)._squared;
        }
    }
}
