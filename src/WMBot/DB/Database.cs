//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.Collections.Generic;

namespace wmib
{
    public class Database
    {
        public class Bind
        {
            public DataType Type;
            public string Value;
            public string Name;

            public Bind(string name, string value, DataType type)
            {
                this.Value = value;
                this.Type = type;
                this.Name = name;
            }
        }

        /// <summary>
        /// One row in a database
        /// </summary>
        [Serializable]
        public class Row
        {
            /// <summary>
            /// One value in a row
            /// </summary>
            [Serializable]
            public class Value
            {
                /// <summary>
                /// Type
                /// </summary>
                public DataType Type;
                /// <summary>
                /// Data
                /// </summary>
                public string Data;
                public string Column = null;

                public Value()
                {
                    this.Data = "false";
                    this.Type = DataType.Boolean;
                }

                /// <summary>
                /// Creates a new value of type int
                /// </summary>
                /// <param name="number"></param>
                public Value(int number, string column = null)
                {
                    Data = number.ToString();
                    Type = DataType.Integer;
                    Column = column;
                }

                public Value(double number, string column = null)
                {
                    Data = number.ToString();
                    Type = DataType.Double;
                    Column = column;
                }

                /// <summary>
                /// Creates a new value of type date
                /// </summary>
                /// <param name="date"></param>
                public Value(DateTime date, string column = null)
                {
                    Data = date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" 
                        + date.Day.ToString().PadLeft(2, '0') + " " + date.Hour.ToString().PadLeft(2, '0') + ":" 
                        + date.Minute.ToString().PadLeft(2, '0') + ":" + date.Second.ToString().PadLeft(2, '0');
                    Type = DataType.Date;
                    Column = column;
                }

                /// <summary>
                /// Creates a new value of type bool
                /// </summary>
                /// <param name="text"></param>
                public Value(bool text, string column = null)
                {
                    Data = text.ToString();
                    Type = DataType.Boolean;
                    Column = column;
                }

                /// <summary>
                /// Creates a new value of type text
                /// </summary>
                /// <param name="text"></param>
                /// <param name="type"></param>
                public Value(string text, DataType type, string column = null)
                {
                    if (text == null)
                        throw new NullReferenceException("Text can't be null");
                    Data = text;
                    Type = type;
                    Column = column;
                }
            }

            /// <summary>
            /// Values of a row
            /// </summary>
            public List<Value> Values = new List<Value>();
        }

        /// <summary>
        /// This lock should be locked in case you are working with database to prevent other threads from working with it
        /// </summary>
        public object DatabaseLock = new object();

        /// <summary>
        /// If database is connected or not
        /// </summary>
        public virtual bool IsConnected
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Last db error is stored here
        /// </summary>
        public string ErrorBuffer = null;

        public virtual void Connect() { }

        public virtual string EscapeInput(string data) { return data; }

        public virtual void Disconnect() { }

        public virtual void Commit() { }

        public virtual void Rollback() { }

        public virtual int Delete(string table, string query) { return 0; }

        public virtual void ExecuteNonQuery(string sql, List<Bind> bind_var = null) { }

        public virtual int CacheSize()
        {
            return 0;
        }

        public virtual bool InsertRow(string table, Row row)
        {
            ErrorBuffer = "INSERT: function is not implemented";
            return false;
        }

        /// <summary>
        /// Select a data from db
        /// </summary>
        /// <param name="table">name of table</param>
        /// <param name="rows">Rows separated by comma</param>
        /// <param name="query">Conditions</param>
        /// <returns>Matrix of returned data in string format or null on failure</returns>
        public virtual List<List<string>> Select(string table, string rows, string query)
        {
            ErrorBuffer = "SELECT: function is not implemented";
            return null;
        }

        /// <summary>
        /// Select a data from db
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Matrix of returned data in string format or null if there is error</returns>
        public virtual List<List<string>> Select(string sql, List<Bind> bind_var = null) { return null; }

        /// <summary>
        /// Data type
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// Text SQL
            /// </summary>
            Text,
            /// <summary>
            /// Varchar SQL
            /// </summary>
            Varchar,
            /// <summary>
            /// Integer SQL
            /// </summary>
            Integer,
            Double,
            Numeric,
            /// <summary>
            /// Boolean SQL
            /// </summary>
            Boolean,
            /// <summary>
            /// Date SQL
            /// </summary>
            Date,
        }
    }
}
